using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxDTOsLibrary.RabbitMQMessages;
using System.Threading;
using MassTransit;


namespace NewsFolderService
{
    public partial class FilesService : ServiceBase, IConsumer<clsMessage>
    {
        // Lists
        public List<clsContactDto> ReceiveContacts = new List<clsContactDto>();
        public List<FileSystemWatcher> FileWatchers = new List<FileSystemWatcher>();
        public List<clsMessage> UnprocessedTxFileMessages = new List<clsMessage>();

        // MassTransit
        private IBusControl busControl;

        public FilesService()
        {
            InitializeComponent();
        }

        // On Start
        protected override async void OnStart(string[] args)
        {
            // Get all Contacts
            ReceiveContacts = await getReceiveContactsFromTheServer();

            // Configure MassTransit with RabbitMQ settings
            configMassTransitBus();

            // Process created files when the service is not running
            processRxFilesBeforeRunningService();

            // Start Listening to Tx File Messages
            await startListeningToTxFileQueueMessages();

            // Add watcher for all receive contacts
            addWatcherSystem(ReceiveContacts);
        }

        // On Stop
        protected async override void OnStop()
        {
            // Stop control bus
            await busControl.StopAsync();

            // Dispose All Watchers
            foreach (var watcher in FileWatchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }

        // _______________________________________________________________________

        private async Task<List<clsContactDto>> getReceiveContactsFromTheServer()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Send an HTTP GET request to the API endpoint
                HttpResponseMessage response = await httpClient.GetAsync(EditBeforeRun.GetReceiveContactsApiUrl);

                // Check if the response is successful (HTTP status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    List<clsContactDto> receiveContacts = JsonConvert.DeserializeObject<List<clsContactDto>>(responseBody);

                    return receiveContacts;
                }
                else
                {
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
        }

        private void processRxFilesBeforeRunningService()
        {
            foreach(var contact in ReceiveContacts)
            {
                // Get Files from the oldest last time date to the recent
                var files = Directory.GetFiles(contact.ReceiveLocation).Select(filePath => new FileInfo(filePath))
                                                                       .OrderBy(fileInfo => fileInfo.LastWriteTime)
                                                                       .Select(fileInfo => fileInfo.FullName).ToList();

                foreach (var file in files)
                {
                    if (Path.GetExtension(file) == ".txt")
                    {
                        bool isNewFile = checkIfNewFile(file, contact);

                        if (isNewFile)
                        {
                            clsMessage rxMessage = new clsMessage()
                            {
                                Id = Guid.NewGuid(),
                                Command = MessagesCommands.RxFile,
                                ActionDate = File.GetLastWriteTime(file)
                            };
                            addFileAndTransmitterToMessage(file, contact, rxMessage);

                            sendRxFileMessage(rxMessage);
                        }
                    }
                }
            }
        }

        private void configMassTransitBus()
        {
            busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(EditBeforeRun.RabbitMQHostAddress), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

        private async Task startListeningToTxFileQueueMessages()
        {
            busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint(EditBeforeRun.TxFileQueue, endpoint =>
                {
                    endpoint.Consumer<FilesService>();
                });
            });

            await busControl.StartAsync();
        }

        private void processTxFileMessage (clsMessage txFileMessage)
        {
            // write txt file
            if(txFileMessage.Command == MessagesCommands.TxFile)
            {
                Guid TxGuid = Guid.NewGuid();

                string txtFileContent = GetTxtFileContent(txFileMessage, TxGuid);
                string filePath = txFileMessage.Contact.TransmitLocation + @"\" + TxGuid + ".txt";

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(txtFileContent);
                }
                fs.Close();

                // Copy Image
                if (txFileMessage.PhotoDto != null)
                {
                    File.Copy(txFileMessage.PhotoDto.Location, txFileMessage.Contact.TransmitLocation + @"\" + TxGuid + Path.GetExtension(txFileMessage.PhotoDto.Location));
                }
            }
        }

        private void addWatcherSystem(List<clsContactDto> receiveContacts)
        {
            foreach (var contact in receiveContacts)
            {
                if ((contact.Direction & (Direction.Receive)) == Direction.Receive)
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(contact.ReceiveLocation);
                    watcher.Created += onFileCreated;
                    watcher.EnableRaisingEvents = true;

                    FileWatchers.Add(watcher);
                }
            }
        }

        private void onFileCreated(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            if (Path.GetExtension(filePath) == ".txt")
            {
                clsContactDto transmitter = (from cntct in ReceiveContacts
                                             where (cntct.ReceiveLocation == Path.GetDirectoryName(filePath))
                                             select cntct).FirstOrDefault();

                bool isNewFile = checkIfNewFile(filePath, transmitter);

                if (isNewFile)
                {
                    clsMessage rxMessage = new clsMessage()
                    {
                        Id = Guid.NewGuid(),
                        Command = MessagesCommands.RxFile,
                        ActionDate = File.GetLastWriteTime(filePath)
                    };
                    addFileAndTransmitterToMessage(filePath, transmitter, rxMessage);

                    sendRxFileMessage(rxMessage);
                }
            }
        }

        private bool checkIfNewFile(string filePath, clsContactDto transmitter)
        {
            DateTime fileLastModification = File.GetLastWriteTime(filePath);

            // Remove fractions
            fileLastModification = fileLastModification.AddTicks(-(fileLastModification.Ticks % TimeSpan.TicksPerSecond));
            transmitter.LastReceiveDate = transmitter.LastReceiveDate.AddTicks(-(transmitter.LastReceiveDate.Ticks % TimeSpan.TicksPerSecond));

            if (fileLastModification > transmitter.LastReceiveDate)
            {
                transmitter.LastReceiveDate = fileLastModification;
                return true;
            }

            else
            {
                return false;
            }
        }

        private void addFileAndTransmitterToMessage(string filePath, clsContactDto transmitter, clsMessage rxFileMessage)
        {
            const int maxRetries = 5;
            const int retryDelayMs = 200; // 0.2 second delay between retries
            bool rety = true;
            int retryCount = 0;

            while (rety && (retryCount < maxRetries))
            {
                try
                {
                    string record;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            record = reader.ReadLine();
                        }
                    }
                    string[] content = record.Split(new string[] { EditBeforeRun.Separator }, StringSplitOptions.None);

                    if (content.Length >= 6)
                    {
                        string format = "M/d/yyyy h:mm:ss tt";
                        // News
                        if (content[0] == $"{(int)Type.News}")
                        {
                            clsNewsDto news = new clsNewsDto()
                            {
                                Id = Guid.NewGuid(),
                                Description = content[1],
                                CreationDate = DateTime.ParseExact(content[2], format, System.Globalization.CultureInfo.InvariantCulture),
                                CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"),
                                CreatorName = "admin",
                                Name = content[3],
                                Body = content[4],
                                Category = content[5],
                                Class = Type.News
                            };

                            rxFileMessage.NewsDto = news;
                        }

                        // Photo
                        else
                        {
                            clsPhotoDto photo = new clsPhotoDto()
                            {
                                Id = Guid.NewGuid(),
                                Description = content[1],
                                CreationDate = DateTime.ParseExact(content[2], format, System.Globalization.CultureInfo.InvariantCulture),
                                CreatorId = new Guid("ffd7c672-aa84-47b1-a9a3-c7875a503708"),
                                CreatorName = "admin",
                                Name = content[3],
                                Body = content[4],
                                Location = content[5],
                                Class = Type.Photo
                            };

                            rxFileMessage.PhotoDto = photo;
                        }
                        rxFileMessage.Contact = transmitter;
                    }

                    else
                    {
                        throw new Exception("Bad File Format");
                    }
                    rety = false;
                }
                catch (IOException)
                {
                    rety=true;
                    // File is in use, wait and then retry
                    Thread.Sleep(retryDelayMs);
                    retryCount++;
                }
            }
        }

        private async void sendRxFileMessage(clsMessage rxMessage)
        {
            // Publishing a message to RxFile Queue 
            var sendEndpoint = await busControl.GetSendEndpoint(new Uri($"{EditBeforeRun.RabbitMQHostAddress}/{EditBeforeRun.RxFileQueue}"));
            await sendEndpoint.Send(rxMessage);
        }

        public Task Consume(ConsumeContext<clsMessage> context)
        {
            var message = context.Message;

            // Handle the received message here
            if (message.Command == MessagesCommands.TxFile)
            {
                processTxFileMessage(message);
                return Task.CompletedTask;
            }

            else
            {
                throw new Exception($"Unsupported command: {message.Command}");
            }
        }

        // _______________________________________________________________________

        public string GetTxtFileContent(clsMessage txFileMessage, Guid TxGuid)
        {
            if(txFileMessage.NewsDto is null)
            {
                return $"{(int)txFileMessage.PhotoDto.Class}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.PhotoDto.Description}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.PhotoDto.CreationDate}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.PhotoDto.Name}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.PhotoDto.Body}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.Contact.TransmitLocation + @"\" + TxGuid + Path.GetExtension(txFileMessage.PhotoDto.Location)}";
            }

            else
            {
                return $"{(int) txFileMessage.NewsDto.Class}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.NewsDto.Description}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.NewsDto.CreationDate}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.NewsDto.Name}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.NewsDto.Body}{EditBeforeRun.Separator}" +
                       $"{txFileMessage.NewsDto.Category}";
            }
        }
    }
}

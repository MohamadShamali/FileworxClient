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
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NewsFolderService
{
    public partial class FilesService : ServiceBase
    {
        public List<clsContactDto> ReceiveContacts = new List<clsContactDto>();
        public List<FileSystemWatcher> FileWatchers = new List<FileSystemWatcher>();
        public List<clsMessage> UnprocessedTxFileMessages = new List<clsMessage>();

        private IConnection connection;
        private IModel channel;

        public FilesService()
        {
            InitializeComponent();
        }

        // On Start
        protected override async void OnStart(string[] args)
        {

            // Get all Contacts
            ReceiveContacts = await getReceiveContactsFromTheServer();

            // Initialize RabbitMQ
            rabbitMQInit();

            // Start Listening to Tx File Messages
            startListeningToTxFileMessages();

            // Get unprocessed messages From DB
            UnprocessedTxFileMessages = await getUnprocessedTxFileMessages();

            // Processed all unprocessed messages
            foreach (var msg in UnprocessedTxFileMessages)
            {
                await processTxFileMessage(msg);
            }

            //// Add watcher for all receive contacts
            //addWatcherSystem(ReceiveContacts);
        }

        // On Stop
        protected override void OnStop()
        {
            // Dispose All Watchers
            foreach (var watcher in FileWatchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }

        // _______________________________________________________________________

        private void rabbitMQInit()
        {
            var factory = new ConnectionFactory { HostName = EditBeforeRun.HostName };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        private void startListeningToTxFileMessages()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += onMessageReceived;

            channel.BasicConsume(queue: EditBeforeRun.TxFileQueue, autoAck: false, consumer: consumer);
        }

        private async void onMessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(body);

            // Deserialize the JSON response
            clsMessage message = JsonConvert.DeserializeObject<clsMessage>(messageString);

            if (message.Command == MessagesCommands.TxFile)
            {
                await processTxFileMessage(message);
            }

            // Acknowledge the message to remove it from the queue
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        }

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

        private async Task<List<clsMessage>> getUnprocessedTxFileMessages()
        {
            clsMessageQuery query = new clsMessageQuery();
            query.QCommandsFilter = new string[] { MessagesCommands.TxFile };

            var m = await query.RunAsync();

            return m;
        }

        private async Task processTxFileMessage (clsMessage txFileMessage)
        {
            if(txFileMessage.Command == MessagesCommands.TxFile)
            {
                // Write txt file
                TransmitFile(txFileMessage);

                // Mark it as processes message in the DB
                txFileMessage.Processed = true;
                await txFileMessage.UpdateAsync();
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

        private async void onFileCreated(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            clsContactDto transmitter = (from cntct in ReceiveContacts
                                     where (cntct.ReceiveLocation == Path.GetDirectoryName(filePath))
                                     select cntct).FirstOrDefault();

            bool isNewFile = checkIfNewFile(filePath, transmitter);

            if (isNewFile)
            {
                clsMessage rxMessage = new clsMessage() { Id=Guid.NewGuid(),
                                                         Command=MessagesCommands.RxFile };

                addFileAndTransmitterToMessage(filePath, transmitter, rxMessage);
                await rxMessage.InsertAsync();

                sendRxFileMessage(rxMessage);
            }
        }

        private bool checkIfNewFile(string filePath, clsContactDto transmitter)
        {
            DateTime fileLastModification = File.GetLastWriteTime(filePath);

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
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string record;
            using (StreamReader reader = new StreamReader(fs))
            {
                record = reader.ReadLine();
            }
            string[] content = record.Split(new string[] { EditBeforeRun.Separator }, StringSplitOptions.None);

            if(content.Length >= 6)
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
                        Category = content[5]
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
                        Location = content[5]
                    };

                    rxFileMessage.PhotoDto = photo;
                }
                rxFileMessage.Contact = transmitter;
            }

            else
            {
                throw new Exception("Bad File Format");
            }
        }

        private void sendRxFileMessage(clsMessage rxMessage)
        {
            channel.QueueDeclare(queue: EditBeforeRun.RxFileQueue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Serialize the rxMessage object to JSON
            var jsonMessage = JsonConvert.SerializeObject(rxMessage);

            // Convert the JSON string to bytes
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // Publish the JSON message
            channel.BasicPublish(exchange: string.Empty,
                                    routingKey: EditBeforeRun.RxFileQueue,
                                    basicProperties: null,
                                    body: body);

        }

        public void TransmitFile(clsMessage txFileMessage)
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
            if(txFileMessage.PhotoDto != null)
            {
                File.Copy(txFileMessage.PhotoDto.Location, txFileMessage.Contact.TransmitLocation + @"\" + TxGuid + Path.GetExtension(txFileMessage.PhotoDto.Location));
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

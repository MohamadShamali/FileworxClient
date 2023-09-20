using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using NewsFolderService.DTOs;


namespace NewsFolderService
{
    public partial class FilesService : ServiceBase
    {
        List<clsContactDto> ReceiveContacts = new List<clsContactDto>();
        public List<FileSystemWatcher> FileWatchers = new List<FileSystemWatcher>();
        public FilesService()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            ReceiveContacts = await getReceiveContactsFromTheServer();
            addWatcherSystem(ReceiveContacts);
        }

        protected override void OnStop()
        {
            foreach(var watcher in FileWatchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }

        private async Task<List<clsContactDto>> getReceiveContactsFromTheServer()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Send an HTTP GET request to the API endpoint
                HttpResponseMessage response = await httpClient.GetAsync(EditBeforeRun.ApiUrl);

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

        private void addWatcherSystem(List<clsContactDto> receiveContacts)
        {
            foreach (var contact in receiveContacts)
            {
                if ((contact.Direction & (ContactDirection.Receive)) == ContactDirection.Receive)
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(contact.ReceiveLocation);
                    watcher.Created += OnFileCreated;
                    watcher.EnableRaisingEvents = true;

                    FileWatchers.Add(watcher);
                }
            }
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            
        }
    }
}

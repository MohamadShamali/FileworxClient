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
using FileworxObjectClassLibrary;


namespace NewsFolderService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private async Task<List<clsContact>> getAllReceiveContactsFromTheServer()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Send an HTTP GET request to the API endpoint
                HttpResponseMessage response = await httpClient.GetAsync(EditBeforRun.ApiUrl);

                // Check if the response is successful (HTTP status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response
                    List<clsContact> receiveContacts = JsonConvert.DeserializeObject<List<clsContact>>(responseBody);

                    return receiveContacts;
                }
                else
                {
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
            
        }

        private async Task addWatcherSystem(List<clsContact> receiveContacts)
        {

        }
    }
}

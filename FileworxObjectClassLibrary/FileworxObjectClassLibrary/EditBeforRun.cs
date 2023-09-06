using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public class EditBeforRun
    {
        // SQL Server DB
        public static string connectionString = @"Server=M-ALShamali;Database=FileworxClientDB;Trusted_Connection=True;";

        // Elasticsearch
        public static string ElasticUri = @"http://localhost:9200";
        public static string ElasticUsersIndex = "users";
        public static string ElasticFilesIndex = "files";
        public static string ElasticContactsIndex = "contacts";
        public static string ElasticBusinessObjectAlias = "businessobjectalias";

        // Local Directories
        public static string PhotosLocation = @"C:\Users\M.AL-Shamali\Desktop\Demo Projects\FileworxClient\WebFileworxClient\wwwroot\Images\StoredImages";
        public static string TransmitFolder = @"C:\Users\M.AL-Shamali\Desktop\Demo Projects\FileworxClient\FileworxObjectClassLibrary\Contacts\Transmit";
        public static string ReceiveFolder = @"C:\Users\M.AL-Shamali\Desktop\Demo Projects\FileworxClient\FileworxObjectClassLibrary\Contacts\Receive";

        // Other
        public static string Separator = "%%$$##";
    }
}
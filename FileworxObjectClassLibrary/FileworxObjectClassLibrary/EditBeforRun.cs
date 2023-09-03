using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public class EditBeforRun
    {
        public static string connectionString = @"Server=M-ALShamali;Database=FileworxClientDB;Trusted_Connection=True;";
        public static string PhotosLocation = @"C:\Users\M.AL-Shamali\Desktop\Demo Projects\FileworxClient\WebFileworxClient\wwwroot\Images\StoredImages";
        public static string ElasticUri = @"http://localhost:9200";

        public static string ElasticUsersIndex = "users";
        public static string ElasticFilesIndex = "files";
        public static string ElasticContactsIndex = "contacts";
        public static string ElasticBusinessObjectAlias = "businessobjectalias";
    }
}
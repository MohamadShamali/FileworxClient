namespace FileworxDTOsLibrary
{
    public static class EditBeforeRun
    {
        // MongoDB
        public static string MessagesDBName = "FileworxRabbitMQ";
        public static string MessagesCollectionName = "Messages";

        // API
        public static string GetReceiveContactsApiUrl = "http://localhost:5000/api/ReceiveContacts";

        // SQL Server DB
        public static string connectionString = @"Server=M-ALShamali;Database=FileworxClientDB;Trusted_Connection=True;";

        // Elasticsearch
        public static string ElasticUri = @"http://localhost:9200";
        public static string ElasticUsersIndex = "users";
        public static string ElasticFilesIndex = "files";
        public static string ElasticContactsIndex = "contacts";
        public static string ElasticBusinessObjectAlias = "businessobjectalias";

        // RabbitMQ
        public static string HostName = "localhost";
        public static string HostAddress = "rabbitmq://localhost";
        public static string RxFileQueue = "RxFile";
        public static string TxFileQueue = "TxFile";

        // Local Directories
        public static string PhotosLocation = @"C:\Users\M.AL-Shamali\Desktop\DemoProjects\FileworxClient\WebFileworxClient\wwwroot\Images\StoredImages";

        // Other
        public static string Separator = "%%$$##";
    }
}
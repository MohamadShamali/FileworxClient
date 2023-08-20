using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Web_Fileworx_Client.Models
{
    public class NewsServices
    {

        public List<clsNews> AllNews = new List<clsNews>();
        public List<clsPhoto> AllPhotos = new List<clsPhoto>();
        public List<clsFile> AllFiles = new List<clsFile>();
        public clsFile? SelectedFile { get; set; }


        public void AddDBFilesToFilesList()
        {
            clsNewsQuery allNewsQuery = new clsNewsQuery();
            AllNews = allNewsQuery.Run();

            clsPhotoQuery allPhotosQuery = new clsPhotoQuery();
            AllPhotos = allPhotosQuery.Run();


            AllFiles = new List<clsFile>();
            AllFiles.AddRange(AllPhotos);
            AllFiles.AddRange(AllNews);
        }

        public void RefreshFilesList()
        {
            AddDBFilesToFilesList();
        }

    }
}

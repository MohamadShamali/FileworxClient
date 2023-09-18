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
        public List<clsFile> CheckedFiles = new List<clsFile>();
        public clsFile? SelectedFile { get; set; }
        public bool SendingMode { get; set; } = false;
        public QuerySource QuerySource { get; set; } = QuerySource.ES;

        public delegate Task RefreshFilesListDelegate();

        public event RefreshFilesListDelegate? RefreshFilesListRequested;

        public async Task RequestRefreshListview()
        {

            // Raise the event to notify subscribers that a refresh is requested
            await RefreshFilesListRequested?.Invoke();
        }

        public async Task AddDBFilesToFilesList()
        {
            clsNewsQuery allNewsQuery = new clsNewsQuery();
            allNewsQuery.Source = QuerySource;
            AllNews = await allNewsQuery.Run();

            clsPhotoQuery allPhotosQuery = new clsPhotoQuery();
            allPhotosQuery.Source = QuerySource;
            AllPhotos = await allPhotosQuery.Run();


            AllFiles = new List<clsFile>();
            AllFiles.AddRange(AllPhotos);
            AllFiles.AddRange(AllNews);
            SelectedFile = AllFiles[0];
        }

        public async Task RefreshFilesList()
        {
            await AddDBFilesToFilesList();
        }

    }
}

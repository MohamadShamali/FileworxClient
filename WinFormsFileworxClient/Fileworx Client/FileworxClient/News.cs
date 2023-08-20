using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public class News
    {
     public string Title { get; set; } //0
     public string Description { get; set; } //1
     public string Body { get; set; } //2
     public DateTime Date { get; set; } //3
     public Guid GUID { get; set; } //4
     public string Category { get; set; } //5
        public string textPath { get; set; } 


        public News(string[] addedNews)
        {
            Title = addedNews[0];
            Description = addedNews[1];
            Body = addedNews[2];
            Date = DateTime.Parse(addedNews[3]);
            GUID = new Guid (addedNews[4]);
            Category = addedNews[5];
            textPath = Path.Combine(EditBeforeRun.newsDirectory, GUID.ToString() + ".txt");
        }
        
    }
}

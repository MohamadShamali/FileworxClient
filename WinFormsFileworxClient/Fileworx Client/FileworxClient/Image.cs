using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileworx_Client
{
    public class Image : News
    {
        public string ImagePath { get; set; } //6
        public Image(string[] addedImage):base(addedImage) 
        { 
         ImagePath = addedImage[6];
        } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public partial class CommonNews : Form
    {
        protected StreamWriter newsWriter;
        public CommonNews()
        {
            InitializeComponent();
        }
        protected string[] getAddedNewsInfo(string tiltleTextBox ,string descriptionTextBox, string bodyTextBox, string date , Guid guid, string Catagory, string imagePath)
        {
            return new string[] { tiltleTextBox, descriptionTextBox, bodyTextBox, DateTime.Now.ToString(), guid.ToString(), Catagory, imagePath };
        }

        protected void writeTxtFile(News news)
        {
            FileStream fs = new FileStream($"{EditBeforeRun.newsDirectory}" + $"\\{news.GUID}.txt", FileMode.Create, FileAccess.Write);
            newsWriter = new StreamWriter(fs);
            if (news is Image)
            {
                Image img = (Image) news;
                newsWriter.AutoFlush = true;
                newsWriter.WriteLine($"{img.Title}%%$$##{img.Description}%%$$##{img.Body}%%$$##{DateTime.Now.ToString()}%%$$##{img.GUID}%%$$##{String.Empty}%%$$##{img.ImagePath}");
            }
            else
            {
                newsWriter.AutoFlush = true;
                newsWriter.WriteLine($"{news.Title}%%$$##{news.Description}%%$$##{news.Body}%%$$##{DateTime.Now.ToString()}%%$$##{news.GUID}%%$$##{news.Category}%%$$##{String.Empty}");
            }

            newsWriter?.Close();
        }
        protected string copyImageToImagesfolder(Guid guid , string imagePath)
        {
            string newImageDirectory = Path.Combine(EditBeforeRun.ImagesDirectory, Path.GetFileName(imagePath));
            File.Copy(imagePath, newImageDirectory);
            string newNameImage = Path.Combine(EditBeforeRun.ImagesDirectory, guid.ToString()) + Path.GetExtension(imagePath);
            File.Move(newImageDirectory, $"{newNameImage}");
            return newNameImage;
        }

        protected void editTxtFile(News news , string prevTextFilePath)
        {
            File.Delete(prevTextFilePath);
            FileStream fs = new FileStream(prevTextFilePath, FileMode.Create, FileAccess.Write);
            newsWriter = new StreamWriter(fs);
            if (news is Image)
            {
                Image img = (Image)news;
                newsWriter.AutoFlush = true;
                newsWriter.WriteLine($"{img.Title}%%$$##{img.Description}%%$$##{img.Body}%%$$##{DateTime.Now.ToString()}%%$$##{img.GUID}%%$$##{String.Empty}%%$$##{img.ImagePath}");
            }
            else
            {
                newsWriter.AutoFlush = true;
                newsWriter.WriteLine($"{news.Title}%%$$##{news.Description}%%$$##{news.Body}%%$$##{DateTime.Now.ToString()}%%$$##{news.GUID}%%$$##{news.Category}%%$$##{String.Empty}");
            }
            newsWriter?.Close();
        }
    }
}

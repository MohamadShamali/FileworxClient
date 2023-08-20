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
    public partial class Edit_Image : CommonNews
    {
        private string prevTextFilePath;
        private string GUID;
        private DateTime date;
        private string prevImgDirectory;
        public Edit_Image(Image editedImage)
        {
            InitializeComponent();
            tiltleTextBox.Text = editedImage.Title;
            descriptionTextBox.Text = editedImage.Description;
            bodyTextBox.Text = editedImage.Body;

            imagePathTextBox.Text = editedImage.ImagePath;
            prevImgDirectory = editedImage.ImagePath;

            GUID = editedImage.GUID.ToString();
            date = editedImage.Date;

            prevTextFilePath = editedImage.textPath;
        }

        private void clearAddNewsTextBoxes()
        {
            tiltleTextBox.Text = String.Empty;
            descriptionTextBox.Text = String.Empty;
            bodyTextBox.Text = String.Empty;

            imagePathTextBox.Text = String.Empty;
            try
            {
                previewBrowsedPictureBox.Image.Dispose();
                previewBrowsedPictureBox.Image = null;
            }
            catch { }
        }

        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browseImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseImageDialog = new OpenFileDialog();
            browseImageDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (browseImageDialog.ShowDialog() == DialogResult.OK)
            {

                imagePathTextBox.Text = browseImageDialog.FileName;
                previewBrowsedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                previewBrowsedPictureBox.Image = new Bitmap(browseImageDialog.FileName);
            }
        }

        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty) && (File.Exists(imagePathTextBox.Text)))
            {

                string[] data = { tiltleTextBox.Text, descriptionTextBox.Text, bodyTextBox.Text, date.ToString(), GUID, String.Empty, imagePathTextBox.Text };
                Image editedImage = new Image(data);

                Guid guid = new Guid(GUID);

                if ((prevImgDirectory != imagePathTextBox.Text) && (File.Exists(imagePathTextBox.Text)))
                {

                    File.Delete(prevImgDirectory);
                    editedImage.ImagePath = copyImageToImagesfolder(guid, imagePathTextBox.Text);

                }
                clearAddNewsTextBoxes();
                editTxtFile(editedImage, prevTextFilePath);
            }
            else
            {
                MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

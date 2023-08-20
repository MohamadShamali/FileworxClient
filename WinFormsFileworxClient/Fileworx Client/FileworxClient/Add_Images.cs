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
    public partial class Add_Images : CommonNews
    {
        public Add_Images()
        {
            InitializeComponent();
        }

        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearAddNewsTextBoxes()
        {
            tiltleTextBox.Text = String.Empty;
            descriptionTextBox.Text = String.Empty;
            bodyTextBox.Text = String.Empty;
            imagePathTextBox.Text = String.Empty;

            previewBrowsedPictureBox.Image.Dispose();
            previewBrowsedPictureBox.Image = null;
        }

        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty) && (File.Exists(imagePathTextBox.Text)))
            {
                Guid guid = Guid.NewGuid();

                string newNameImage;
                if (File.Exists(imagePathTextBox.Text))
                {
                    newNameImage = copyImageToImagesfolder(guid , imagePathTextBox.Text);
                }
                else
                {
                    newNameImage = String.Empty;
                }
                Image NewImage = new Image(getAddedNewsInfo(tiltleTextBox.Text, descriptionTextBox.Text, bodyTextBox.Text, DateTime.Now.ToString(), guid, string.Empty, newNameImage));

                writeTxtFile(NewImage);

                clearAddNewsTextBoxes();
                this.Close();
            }
            else
            {
                MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}

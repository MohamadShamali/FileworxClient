using FileworxObjectClassLibrary;
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
    public partial class AddImageWindow : Form
    {
        clsPhoto photoToEdit = new clsPhoto();
        public AddImageWindow()
        {
            InitializeComponent();
        }

        public AddImageWindow(clsPhoto photoToEdit)
        {
            InitializeComponent();
            tiltleTextBox.Text = photoToEdit.Name;
            descriptionTextBox.Text = photoToEdit.Description;
            bodyTextBox.Text = photoToEdit.Body;
            imagePathTextBox.Text = photoToEdit.Location;

            previewBrowsedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            using (var img = new Bitmap(photoToEdit.Location))
            {
                previewBrowsedPictureBox.Image = new Bitmap(img);
            }
            this.Text = "Edit Image";
            this.photoToEdit = photoToEdit;
        }

        protected void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected virtual void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            // Add Case
            if (!String.IsNullOrEmpty(photoToEdit.Name))
            {
                if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty)
                    && (bodyTextBox.Text != String.Empty) && (File.Exists(imagePathTextBox.Text)))
                {

                    clsPhoto newPhoto = new clsPhoto()
                    {
                        Id = Guid.NewGuid(),
                        Description = descriptionTextBox.Text,
                        CreatorId = Global.LoggedInUser.Id,
                        CreatorName = Global.LoggedInUser.Name,
                        Name = tiltleTextBox.Text,
                        Body = bodyTextBox.Text,
                        Location = imagePathTextBox.Text,
                        Class = clsBusinessObject.Type.Photo
                    };

                    newPhoto.Insert();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            //Edit Case
            else
            {
                if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty)
                    && (bodyTextBox.Text != String.Empty) && (File.Exists(imagePathTextBox.Text)))
                {

                    photoToEdit.Description = descriptionTextBox.Text;
                    photoToEdit.LastModifierId = Global.LoggedInUser.Id;
                    photoToEdit.LastModifierName = Global.LoggedInUser.Name;
                    photoToEdit.Name = tiltleTextBox.Text;
                    photoToEdit.Body = bodyTextBox.Text;
                    photoToEdit.Location = imagePathTextBox.Text;


                    photoToEdit.Update();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }
        }

        protected void browseImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseImageDialog = new OpenFileDialog();
            browseImageDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (browseImageDialog.ShowDialog() == DialogResult.OK)
            {
                imagePathTextBox.Text = browseImageDialog.FileName;
                previewBrowsedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                using (var img = new Bitmap(browseImageDialog.FileName))
                {
                    previewBrowsedPictureBox.Image = new Bitmap(img);
                }
            }
        }
    }
}

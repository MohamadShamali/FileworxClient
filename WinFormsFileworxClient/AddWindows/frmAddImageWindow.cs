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
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxObjectClassLibrary.Models;

namespace Fileworx_Client
{
    public partial class AddImageWindow : Form
    {
        clsPhoto photoToEdit = new clsPhoto();
        public event OnFormCloseHandler OnFormClose;
        public AddImageWindow()
        {
            InitializeComponent();
        }

        public AddImageWindow(clsPhoto photoToEdit)
        {
            InitializeComponent();
            txtTitle.Text = photoToEdit.Name;
            txtDescription.Text = photoToEdit.Description;
            txtBody.Text = photoToEdit.Body;
            imagePathTextBox.Text = photoToEdit.Location;

            previewBrowsedPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            using (var img = new Bitmap(photoToEdit.Location))
            {
                previewBrowsedPictureBox.Image = new Bitmap(img);
            }
            this.Text = "Edit Image";
            this.photoToEdit = photoToEdit;
        }

        private bool validateData()
        {
            if (!String.IsNullOrEmpty(txtTitle.Text) && (!String.IsNullOrEmpty(txtDescription.Text)) && !String.IsNullOrEmpty(txtBody.Text) && File.Exists(imagePathTextBox.Text))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            // Add Case
            if (String.IsNullOrEmpty(photoToEdit.Name))
            {
                if (validateData())
                {

                    clsPhoto newPhoto = new clsPhoto()
                    {
                        Id = Guid.NewGuid(),
                        Description = txtDescription.Text,
                        CreatorId = WinFormsGlobal.LoggedInUser.Id,
                        CreatorName = WinFormsGlobal.LoggedInUser.Name,
                        Name = txtTitle.Text,
                        Body = txtBody.Text,
                        Location = imagePathTextBox.Text,
                        Class = Type.Photo
                    };

                    await newPhoto.InsertAsync();
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
                if (validateData())
                {

                    photoToEdit.Description = txtDescription.Text;
                    photoToEdit.LastModifierId = WinFormsGlobal.LoggedInUser.Id;
                    photoToEdit.LastModifierName = WinFormsGlobal.LoggedInUser.Name;
                    photoToEdit.Name = txtTitle.Text;
                    photoToEdit.Body = txtBody.Text;
                    photoToEdit.Location = imagePathTextBox.Text;


                    await photoToEdit.InsertAsync();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            if (OnFormClose != null)
            {
                await OnFormClose();
            }

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

                using (var img = new Bitmap(browseImageDialog.FileName))
                {
                    previewBrowsedPictureBox.Image = new Bitmap(img);
                }
            }
        }
    }
}

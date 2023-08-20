using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public partial class EditImageWindow : AddImageWindow
    {
        clsPhoto photoToEdit = new clsPhoto();

        public EditImageWindow(clsPhoto photoToEdit)
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

            this.photoToEdit = photoToEdit;
        }

        protected override void saveAddNewsButton_Click(object sender, EventArgs e)
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
}

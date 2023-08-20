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
    public partial class Add_Image : Form
    {
        public Add_Image()
        {
            InitializeComponent();
        }

        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            if ((tiltleTextBox.Text != String.Empty)&&(descriptionTextBox.Text!=String.Empty) && (bodyTextBox.Text != String.Empty))
            {
                Guid guid = Guid.NewGuid();

                string newNameImage;
                if (File.Exists(imagePathTextBox.Text))
                {
                    newNameImage = copyImageToImagesfolder(guid);
                }
                else
                {
                    newNameImage = String.Empty;
                }
                News NewNews = new News(getAddedNewsInfo(guid , newNameImage));

                writeTxtFile(NewNews);

                clearAddNewsTextBoxes();
                this.Close();
            } 

            else
            {
                //MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

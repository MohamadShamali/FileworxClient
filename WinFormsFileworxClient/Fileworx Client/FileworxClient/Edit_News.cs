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
    public partial class Edit_News : CommonNews
    {
        private string prevTextFilePath;
        private string GUID;
        private DateTime date;
        public Edit_News(News editedNews)
        {
            InitializeComponent();
            tiltleTextBox.Text = editedNews.Title;
            descriptionTextBox.Text = editedNews.Description;
            bodyTextBox.Text = editedNews.Body;

            categoryComboBox.Text = editedNews.Category;

            GUID = editedNews.GUID.ToString();
            date = editedNews.Date;

            prevTextFilePath = editedNews.textPath;
        }

        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty))
            {
                string[] data = { tiltleTextBox.Text, descriptionTextBox.Text, bodyTextBox.Text, date.ToString(), GUID, categoryComboBox.Text, String.Empty };
                News editedNews = new News(data);

                Guid guid = new Guid(GUID);


                editTxtFile(editedNews, prevTextFilePath);
            }
            else
            {
                MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

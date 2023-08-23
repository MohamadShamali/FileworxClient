using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public partial class AddNewsWindow : Form
    {
        clsNews newsToEdit = new clsNews();
        public AddNewsWindow()
        {
            InitializeComponent();
            categoryComboBox.SelectedIndex = 0;
        }

        public AddNewsWindow(clsNews newsToEdit)
        {
            InitializeComponent();
            tiltleTextBox.Text = newsToEdit.Name;
            descriptionTextBox.Text = newsToEdit.Description;
            bodyTextBox.Text = newsToEdit.Body;
            categoryComboBox.SelectedText = newsToEdit.Category;

            this.Text = "Edit News";
            this.newsToEdit = newsToEdit;
        }

        protected void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected virtual void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            // ADD Case
            if (!String.IsNullOrEmpty(newsToEdit.Name))
            {
                if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty))
                {
                    clsNews newNews = new clsNews()
                    {
                        Id = Guid.NewGuid(),
                        Description = descriptionTextBox.Text,
                        CreatorId = Global.LoggedInUser.Id,
                        CreatorName = Global.LoggedInUser.Name,
                        Name = tiltleTextBox.Text,
                        Body = bodyTextBox.Text,
                        Category = categoryComboBox.SelectedItem.ToString(),
                        Class = clsBusinessObject.Type.News
                    };

                    newNews.Insert();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            // Edit Case
            else
            {
                if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty))
                {
                    newsToEdit.Description = descriptionTextBox.Text;
                    newsToEdit.LastModifierId = Global.LoggedInUser.Id;
                    newsToEdit.LastModifierName = Global.LoggedInUser.Name;
                    newsToEdit.Name = tiltleTextBox.Text;
                    newsToEdit.Body = bodyTextBox.Text;
                    newsToEdit.Category = categoryComboBox.SelectedItem.ToString();

                    newsToEdit.Update();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

        }
    }
}

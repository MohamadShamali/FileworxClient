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
        public AddNewsWindow()
        {
            InitializeComponent();
            categoryComboBox.SelectedIndex = 0;
        }
        protected void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected virtual void saveAddNewsButton_Click(object sender, EventArgs e)
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
    }
}

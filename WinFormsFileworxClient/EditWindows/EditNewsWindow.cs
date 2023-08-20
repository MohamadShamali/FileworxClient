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
    public partial class EditNewsWindow : AddNewsWindow
    {
        clsNews newsToEdit = new clsNews();
        public EditNewsWindow(clsNews newsToEdit)
        {
            InitializeComponent();
            tiltleTextBox.Text = newsToEdit.Name;
            descriptionTextBox.Text = newsToEdit.Description;
            bodyTextBox.Text = newsToEdit.Body;
            categoryComboBox.SelectedText = newsToEdit.Category;

            this.newsToEdit = newsToEdit;
        }

        protected override void saveAddNewsButton_Click(object sender, EventArgs e)
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

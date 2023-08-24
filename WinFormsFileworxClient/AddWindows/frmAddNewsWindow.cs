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
    public enum FormResult { Save , Cancel };

    public delegate void OnFormCloseHandler();
    public partial class frmAddNewsWindow : Form
    {
        clsNews newsToEdit = new clsNews();

        public event OnFormCloseHandler OnFormClose;
        public frmAddNewsWindow()
        {
            InitializeComponent();
            cboCategory.SelectedIndex = 0;
        }

        public frmAddNewsWindow(clsNews newsToEdit)
        {
            InitializeComponent();
            txtTiltle.Text = newsToEdit.Name;
            txtDescription.Text = newsToEdit.Description;
            txtBody.Text = newsToEdit.Body;
            cboCategory.SelectedItem = newsToEdit.Category;

            this.Text = "Edit News";
            this.newsToEdit = newsToEdit;
        }

        private bool validateData()
        {
            if (!String.IsNullOrEmpty(txtTiltle.Text) && (!String.IsNullOrEmpty(txtDescription.Text)) && !String.IsNullOrEmpty(txtBody.Text))
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
        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            // ADD Case
            if (String.IsNullOrEmpty(newsToEdit.Name))
            {
                if (validateData())
                {
                    clsNews newNews = new clsNews()
                    {
                        Id = Guid.NewGuid(),
                        Description = txtDescription.Text,
                        CreatorId = Global.LoggedInUser.Id,
                        CreatorName = Global.LoggedInUser.Name,
                        Name = txtTiltle.Text,
                        Body = txtBody.Text,
                        Category = cboCategory.SelectedItem.ToString(),
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
                if (validateData())
                {
                    newsToEdit.Description = txtDescription.Text;
                    newsToEdit.LastModifierId = Global.LoggedInUser.Id;
                    newsToEdit.LastModifierName = Global.LoggedInUser.Name;
                    newsToEdit.Name = txtTiltle.Text;
                    newsToEdit.Body = txtBody.Text;
                    newsToEdit.Category = cboCategory.SelectedItem.ToString();

                    newsToEdit.Update();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            if (OnFormClose != null)
            {
                OnFormClose();
            }

            this.Close();
        }
    }
}

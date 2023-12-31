﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxObjectClassLibrary.Models;

namespace Fileworx_Client
{
    public enum FormResult { Save , Cancel };

    public delegate Task OnFormCloseHandler();
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
        private async void saveAddNewsButton_Click(object sender, EventArgs e)
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
                        CreatorId = WinFormsGlobal.LoggedInUser.Id,
                        CreatorName = WinFormsGlobal.LoggedInUser.Name,
                        Name = txtTiltle.Text,
                        Body = txtBody.Text,
                        Category = cboCategory.SelectedItem.ToString(),
                        Class = Type.News
                    };

                    await newNews.InsertAsync();
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
                    newsToEdit.LastModifierId = WinFormsGlobal.LoggedInUser.Id;
                    newsToEdit.LastModifierName = WinFormsGlobal.LoggedInUser.Name;
                    newsToEdit.Name = txtTiltle.Text;
                    newsToEdit.Body = txtBody.Text;
                    newsToEdit.Category = cboCategory.SelectedItem.ToString();

                    await newsToEdit.UpdateAsync();
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
    }
}

﻿using Fileworx_Client.MainForms;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using FileworxDTOsLibrary.RabbitMQMessages;
using FileworxObjectClassLibrary;
using FileworxObjectClassLibrary.Models;
using FileworxObjectClassLibrary.Queries;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Fileworx_Client.frmFileworx;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Type = FileworxDTOsLibrary.DTOs.Type;

namespace Fileworx_Client
{
    public partial class frmFileworx : Form
    {
        // Lists
        private List<clsFile> allFiles { get; set; } = new List<clsFile>();
        private List<clsNews> allNews { get; set; } = new List<clsNews>();
        private List<clsPhoto> allPhotos { get; set; } = new List<clsPhoto>();
        public List<clsMessage> UnprocessedRxFileMessages { get; set; } = new List<clsMessage>();

        // RabbitMQ
        private MessagesHandling messagesHandling;

        // Other
        private bool formStarted { get; set; } = false;
        private QuerySource querySource { get; set; } = QuerySource.ES;
        private TabPage hiddenTabPage { get; set; }
        private enum SortBy { RecentDate, OldestDate, Alphabetically };

        public frmFileworx()
        {
            InitializeComponent();
        }

        public static async Task<frmFileworx> Create()
        {
            var fileworx = new frmFileworx();

            // UI 
            int desiredHeight = (int)((fileworx.Height * 2 / 3));
            if (fileworx.splitContainer1.Panel1MinSize <= desiredHeight && desiredHeight <= fileworx.splitContainer1.Height - fileworx.splitContainer1.Panel2MinSize)
            {
                fileworx.splitContainer1.SplitterDistance = desiredHeight;
            }
            fileworx.lblName.Text = WinFormsGlobal.LoggedInUser.Name;
            fileworx.WindowState = FormWindowState.Maximized;
            fileworx.cboDataStoreSource.SelectedIndex = 1;
            fileworx.btnSendTo.Enabled = false;

            // Hide and save hidden Tab
            fileworx.hiddenTabPage = fileworx.tclPreview.TabPages[1];
            fileworx.tclPreview.TabPages.RemoveAt(1);

            //Admin access
            if (WinFormsGlobal.LoggedInUser.IsAdmin) fileworx.msiUsersList.Enabled = true;
            else fileworx.msiUsersList.Enabled = false;

            // Initialize Messages Handeling
            fileworx.messagesHandling = new MessagesHandling();

            // Configure UI action when a message is received
            Global.UIAction += fileworx.onMessageReceived;

            // Start listening to RX file Messages
            fileworx.messagesHandling.StartListeningToRxFileMessages();

            // Add DB files to files list
            await fileworx.addDBFilesToFilesList();

            // Add files to listView
            fileworx.sortFilesList(SortBy.RecentDate);
            fileworx.addFilesListItemsToListView();

            // Set the form started
            fileworx.formStarted = true;

            return fileworx;
        }

        private async Task onMessageReceived()
        {
            await refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();
        }

        private async Task addDBFilesToFilesList()
        {
            clsNewsQuery allNewsQuery = new clsNewsQuery();
            allNewsQuery.Source = querySource;
            allNews = await allNewsQuery.RunAsync();

            clsPhotoQuery allPhotosQuery = new clsPhotoQuery();
            allPhotosQuery.Source = querySource;
            allPhotos = await allPhotosQuery.RunAsync();


            allFiles = new List<clsFile>();
            allFiles.AddRange(allPhotos);
            allFiles.AddRange(allNews);
        }

        private void addFilesListItemsToListView()
        {
            if (lvwFiles.InvokeRequired)
            {
                // Use Invoke to marshal the operation to the UI thread
                lvwFiles.Invoke(new Action(() => addFilesListItemsToListView()));
            }

            else
            {
                if (lvwFiles.Items.Count > 0)
                {
                    lvwFiles.Items.Clear();
                }

                foreach (clsFile file in allFiles)
                {
                    var listViewNews = new ListViewItem($"{file.Name}");
                    listViewNews.SubItems.Add($"{file.CreationDate}");
                    listViewNews.SubItems.Add($"{file.Description}");
                    lvwFiles.Items.Add(listViewNews);
                }
            }
        }

        private async Task refreshFilesList()
        {
            allFiles.Clear();
            allNews.Clear();
            allPhotos.Clear(); 
            await addDBFilesToFilesList();
        }

        private void sortFilesList(SortBy sortBy)
        {
            if (sortBy == SortBy.RecentDate)
            {
                var sortedList = (from file in allFiles
                                  orderby file.CreationDate descending
                                  select file).ToList();

                allFiles = sortedList;
            }

            if (sortBy == SortBy.OldestDate)
            {
                var sortedList = (from file in allFiles
                                  orderby file.CreationDate ascending
                                  select file).ToList();

                allFiles = sortedList;
            }

            if (sortBy == SortBy.Alphabetically)
            {
                var sortedList = (from file in allFiles
                                  orderby file.Name ascending
                                  select file).ToList();

                allFiles = sortedList;
            }
        }

        private void autoSortFilesList()
        {
            if (msiSortByRecent.Checked)
            {
                sortFilesList(SortBy.RecentDate);
            }

            if (msiSortByOldest.Checked)
            {
                sortFilesList(SortBy.OldestDate);
            }

            if (msiSortByAlphabetically.Checked)
            {
                sortFilesList(SortBy.Alphabetically);
            }
        }

        private void clearAllDisplayLabels()
        {
            lblTitle.Text = String.Empty;
            lblDate.Text = String.Empty;
            lblCategory.Text = String.Empty;
            txtBody.Text = String.Empty;
        }

        private clsFile findSelectedFile()
        {
            if(lvwFiles.SelectedItems.Count > 0)
            {
                clsFile selectedFile =
                                    (from file in allFiles
                                     where (file.CreationDate.ToString() == (lvwFiles.SelectedItems[0].SubItems[1].Text))
                                     select file).FirstOrDefault();

                return selectedFile;
            }

            else { return null; }
        }

        private List<clsFile> findAllCheckedFiles()
        {
            List<clsFile> selectedFiles = new List<clsFile>();

            if (lvwFiles.CheckedItems.Count > 0)
            {
                selectedFiles =
                     (from file in allFiles
                      where lvwFiles.CheckedItems.Cast<ListViewItem>()
                            .Any(checkedItem => file.CreationDate.ToString() == checkedItem.SubItems[1].Text)
                      select file).ToList();
            }

            return selectedFiles;
        }

        private void displaySelectedFile (clsFile selectedFile)
        {
            if(selectedFile != null)
            {
                lblTitle.Text = selectedFile.Name;
                lblDate.Text = selectedFile.CreationDate.ToString();
                txtBody.Text = selectedFile.Body;
                picImagePreview.SizeMode = PictureBoxSizeMode.Zoom;

                if (selectedFile is clsPhoto)
                {
                    clsPhoto selectedPhoto = (clsPhoto)selectedFile;

                    lblCategoryTitle.Text = String.Empty;
                    lblCategory.Text = String.Empty;

                    if (File.Exists(selectedPhoto.Location))
                    {
                        if (tclPreview.TabPages.Count == 1)
                        {
                            tclPreview.TabPages.Add(hiddenTabPage);
                        }

                        using (var img = new Bitmap(selectedPhoto.Location))
                        {
                            picImagePreview.Image = new Bitmap(img);
                        }
                    }
                }

                else
                {
                    clsNews selectedNews = (clsNews)selectedFile;

                    lblCategoryTitle.Text = "Category:";
                    lblCategory.Text = selectedNews.Category;

                    try
                    {
                        if (tclPreview.TabPages.Count == 2)
                        {
                            hiddenTabPage = tclPreview.TabPages[1];
                            tclPreview.TabPages.RemoveAt(1);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private async Task deleteFile (clsFile selectedFile)
        {
            if (selectedFile is clsPhoto)
            {
                clsPhoto selectedPhoto = (clsPhoto) selectedFile;

                if (picImagePreview.Image != null)
                {
                    picImagePreview.Image.Dispose();
                    picImagePreview.Image = null;
                }

                await selectedPhoto.DeleteAsync();
            }

            else
            {
                clsNews selectedNews = (clsNews)selectedFile;
                await selectedNews.DeleteAsync();
            }
        }

        private void uncheckAllSortByItems()
        {
            msiSortByRecent.Checked = false;
            msiSortByOldest.Checked = false;
            msiSortByAlphabetically.Checked = false;
        }

        private async Task onAddFormClose()
         {
            await refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();
        }

        private async Task onEditFormClose()
        {
            int selectedIndex = lvwFiles.SelectedItems[0].Index;
            await refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();

            lvwFiles.SelectedIndices.Clear();
            lvwFiles.SelectedIndices.Add(selectedIndex); 

            clsFile selectedFile = findSelectedFile();
            displaySelectedFile(selectedFile);
        }

        private void disableSendingMode()
        {
            lvwFiles.CheckBoxes = false;
            lvwFiles.MultiSelect = false;
            pnlDataSourceSelection.Enabled = true;
            pnlDataSourceSelection.Visible = true;
            panel3.Enabled = false;
            panel3.Visible = false;

            mnuMenuStrip.Enabled = true;
        }

        private async Task afterAddingContact()
        {

        }

        //------------------------ Event Handlers ------------------------//

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newsListView_MouseClick(object sender, MouseEventArgs e)
        {
            clsFile selectedFile = findSelectedFile();

            if (e.Button == MouseButtons.Right)
            {
                if (selectedFile != null)
                {
                    cmsFiles.Show(lvwFiles, new Point(e.X, e.Y));
                }
            }

            displaySelectedFile(selectedFile);
        }

        private void FileWorx_Resize(object sender, EventArgs e)
        {
            int desiredHeight = (int)((this.Height * 2 / 3));
            if (splitContainer1.Panel1MinSize <= desiredHeight && desiredHeight <= splitContainer1.Height - splitContainer1.Panel2MinSize)
            {
                splitContainer1.SplitterDistance = desiredHeight;
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddImageWindow add_Image = new AddImageWindow();
            add_Image.OnFormClose += onAddFormClose;
            add_Image.Show();
        }

        private void addNewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewsWindow add_News = new frmAddNewsWindow();
            add_News.OnFormClose += onAddFormClose;
            add_News.Show();
        }

        private void recentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortFilesList(SortBy.RecentDate);
            addFilesListItemsToListView();
            uncheckAllSortByItems();
            msiSortByRecent.Checked = true;
        }

        private void oldestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortFilesList(SortBy.OldestDate);
            addFilesListItemsToListView();
            uncheckAllSortByItems();
            msiSortByOldest.Checked = true;
        }

        private void alphabeticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortFilesList(SortBy.Alphabetically);
            addFilesListItemsToListView();
            uncheckAllSortByItems();
            msiSortByAlphabetically.Checked = true;
        }

        private async void usersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList usersList = await frmUsersList.Create();
            usersList.Show();
        }

        private void FileWorx_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Open LogIn form in a new thread
            LogIn logIn = new LogIn();
            var logInThread = new Thread(() => Application.Run(logIn));
            logInThread.Start();
        }

        private void editFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsFile fileToEdit = findSelectedFile();
            if (fileToEdit is clsPhoto)
            {

                clsPhoto photoToEdit = (clsPhoto)fileToEdit;
                AddImageWindow editImage = new AddImageWindow(photoToEdit);
                editImage.OnFormClose += onEditFormClose;
                editImage.Show();
            }
            else
            {
                clsNews photoToEdit = (clsNews)fileToEdit;
                frmAddNewsWindow editNews = new frmAddNewsWindow(photoToEdit);
                editNews.OnFormClose += onEditFormClose;
                editNews.Show();
            }
        }

        private async void removeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsFile selectedFile = findSelectedFile();
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {selectedFile.Name}?",
                                       "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lvwFiles.SelectedItems.Clear();
                clearAllDisplayLabels();

                await deleteFile(selectedFile);

            
                await refreshFilesList();
                autoSortFilesList();
                addFilesListItemsToListView();

                if (tclPreview.TabPages.Count == 2)
                {
                    hiddenTabPage = tclPreview.TabPages[1];
                    tclPreview.TabPages.RemoveAt(1);
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if(cboDataStoreSource.SelectedIndex == 0)
            {
                querySource = QuerySource.DB;
            }
            if (cboDataStoreSource.SelectedIndex == 1)
            {
                querySource = QuerySource.ES;
            }

            await refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();
        }

        private void sendFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwFiles.CheckBoxes = true;
            lvwFiles.MultiSelect = true;
            pnlDataSourceSelection.Enabled = false;
            pnlDataSourceSelection.Visible = false;
            panel3.Enabled = true;
            panel3.Visible = true;

            mnuMenuStrip.Enabled = false;
        }

        private void btnCancelSending_Click(object sender, EventArgs e)
        {
            disableSendingMode();
        }

        private async void contactsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var contactsList = await frmContactsList.Create();
            contactsList.AfterAddingContact += afterAddingContact;
            contactsList.Show();
        }

        private async void btnSendTo_Click(object sender, EventArgs e)
        {
            var contactsList = await frmContactsList.Create(findAllCheckedFiles());
            contactsList.OnCloseAfterSend += disableSendingMode;
            contactsList.AfterAddingContact += afterAddingContact;
            contactsList.ShowDialog();
        }

        private void lvwFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvwFiles.CheckedItems.Count > 0)
            {
                btnSendTo.Enabled = true;
            }
            else
            {
                btnSendTo.Enabled = false;
            }
        }

        private void btnEnableOrDisableService_Click(object sender, EventArgs e)
        {
            try
            {
                // Replace "YourServiceName" with the actual service name
                serviceController.ServiceName = "NewsFolderService";

                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    // The service is currently running, so ask for confirmation to stop it
                    DialogResult result = MessageBox.Show("The Service is currently running. Do you want to stop it?",
                        "Service Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        progressBar.Value = 0;
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                        progressBar.Value = 100;
                        MessageBox.Show("Service stopped successfully.", "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    // The service is currently stopped, so ask for confirmation to start it
                    DialogResult result = MessageBox.Show("The Service is currently stopped. Do you want to start it?",
                        "Service Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        progressBar.Value = 0;
                        serviceController.Start();
                        progressBar.Value = 100;
                        MessageBox.Show("Service started successfully.", "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Service is in an unknown state.", "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Service Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

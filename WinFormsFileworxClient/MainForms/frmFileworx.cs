using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Fileworx_Client.frmFileworx;

namespace Fileworx_Client
{
    public partial class frmFileworx : Form
    {
        // Properties
        private static List<clsFile> allFiles { get; set; }
        private static List<clsNews> allNews { get; set; }
        private static List<clsPhoto> allPhotos { get; set; }
        private TabPage hiddenTabPage;
        private enum SortBy { RecentDate, OldestDate, Alphabetically };

        public frmFileworx()
        {
            InitializeComponent();

            // UI 
            int desiredHeight = (int)((this.Height * 2 / 3) );
            if (splitContainer1.Panel1MinSize <= desiredHeight && desiredHeight <= splitContainer1.Height - splitContainer1.Panel2MinSize)
            {
                splitContainer1.SplitterDistance = desiredHeight;
            }
            lblName.Text = Global.LoggedInUser.Name;
            this.WindowState = FormWindowState.Maximized;

            // Hide and save hidden Tab
            hiddenTabPage = tclPreview.TabPages[1];
            tclPreview.TabPages.RemoveAt(1);

            //Admin access
            if (Global.LoggedInUser.IsAdmin) msiUsersList.Enabled = true;
            else msiUsersList.Enabled = false;

            // Add files to listView
            addDBFilesToFilesList();
            sortFilesList(SortBy.RecentDate);
            addFilesListItemsToListView();
        }

        private async void addDBFilesToFilesList()
        {
            clsNewsQuery allNewsQuery = new clsNewsQuery();
            allNewsQuery.Source = QuerySource.DB;
            allNews = await allNewsQuery.Run();

            clsPhotoQuery allPhotosQuery = new clsPhotoQuery();
            allPhotosQuery.Source = QuerySource.DB;
            allPhotos = await allPhotosQuery.Run();


            allFiles = new List<clsFile>();
            allFiles.AddRange(allPhotos);
            allFiles.AddRange(allNews);
        }

        private void addFilesListItemsToListView()
        {
            lvwFiles.Items.Clear();
            foreach (clsFile file in allFiles)
            {
                var listViewNews = new ListViewItem($"{file.Name}");
                listViewNews.SubItems.Add($"{file.CreationDate}");
                listViewNews.SubItems.Add($"{file.Description}");
                lvwFiles.Items.Add(listViewNews);
            }
        }

        private void refreshFilesList()
        {
            allFiles.Clear();
            allNews.Clear();
            allPhotos.Clear(); 
            addDBFilesToFilesList();
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
            clsFile selectedFile =
                (from file in allFiles
                 where ((file.Name == lvwFiles.SelectedItems[0].Text) && (file.CreationDate == DateTime.Parse(lvwFiles.SelectedItems[0].SubItems[1].Text)))
                 select file).FirstOrDefault();

            return selectedFile;
        }

        private void displaySelectedFile (clsFile selectedFile)
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

        private void deleteFile (clsFile selectedFile)
        {
            if (selectedFile is clsPhoto)
            {
                clsPhoto selectedPhoto = (clsPhoto) selectedFile;

                if (picImagePreview.Image != null)
                {
                    picImagePreview.Image.Dispose();
                    picImagePreview.Image = null;
                }

                selectedPhoto.Delete();
            }

            else
            {
                clsNews selectedNews = (clsNews)selectedFile;
                selectedNews.Delete();
            }
        }

        private void uncheckAllSortByItems()
        {
            msiSortByRecent.Checked = false;
            msiSortByOldest.Checked = false;
            msiSortByAlphabetically.Checked = false;
        }

        private void onAddFormClose()
        {
            refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();
        }

        private void onEditFormClose()
        {
            int selectedIndex = lvwFiles.SelectedItems[0].Index;
            refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();

            lvwFiles.SelectedIndices.Clear();
            lvwFiles.SelectedIndices.Add(selectedIndex); 

            clsFile selectedFile = findSelectedFile();
            displaySelectedFile(selectedFile);
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
                if(selectedFile != null)
                {
                    cmsFiles.Show(lvwFiles,new Point(e.X,e.Y));
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

        private void usersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersList usersList = new frmUsersList();
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

        private void removeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsFile selectedFile = findSelectedFile();
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {selectedFile.Name}?",
                                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lvwFiles.SelectedItems.Clear();
                clearAllDisplayLabels();

                deleteFile(selectedFile);

                refreshFilesList();
                autoSortFilesList();
                addFilesListItemsToListView();

                if (tclPreview.TabPages.Count == 2)
                {
                    hiddenTabPage = tclPreview.TabPages[1];
                    tclPreview.TabPages.RemoveAt(1);
                }
            }
        }
    }
}

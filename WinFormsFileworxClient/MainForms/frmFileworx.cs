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
            label7.Text = Global.LoggedInUser.Name;
            this.WindowState = FormWindowState.Maximized;

            // Hide and save hidden Tab
            hiddenTabPage = tabControl1.TabPages[1];
            tabControl1.TabPages.RemoveAt(1);

            //Admin access
            if (Global.LoggedInUser.IsAdmin) usersListToolStripMenuItem.Enabled = true;
            else usersListToolStripMenuItem.Enabled = false;

            // Add files to listView
            addDBFilesToFilesList();
            sortFilesList(SortBy.RecentDate);
            addFilesListItemsToListView();
        }

        private void addDBFilesToFilesList()
        {
            clsNewsQuery allNewsQuery = new clsNewsQuery();
            allNews = allNewsQuery.Run();

            clsPhotoQuery allPhotosQuery = new clsPhotoQuery();
            allPhotos = allPhotosQuery.Run();


            allFiles = new List<clsFile>();
            allFiles.AddRange(allPhotos);
            allFiles.AddRange(allNews);
        }

        private void addFilesListItemsToListView()
        {
            newsListView.Items.Clear();
            foreach (clsFile file in allFiles)
            {
                var listViewNews = new ListViewItem($"{file.Name}");
                listViewNews.SubItems.Add($"{file.CreationDate}");
                listViewNews.SubItems.Add($"{file.Description}");
                newsListView.Items.Add(listViewNews);
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
            if (recentToolStripMenuItem.Checked)
            {
                sortFilesList(SortBy.RecentDate);
            }

            if (oldestToolStripMenuItem.Checked)
            {
                sortFilesList(SortBy.OldestDate);
            }

            if (alphabeticallyToolStripMenuItem.Checked)
            {
                sortFilesList(SortBy.Alphabetically);
            }
        }

        private void clearAllDisplayLabels()
        {
            titleLabel.Text = String.Empty;
            dateLabel.Text = String.Empty;
            categoryLabel.Text = String.Empty;
            bodyRichTextBox.Text = String.Empty;
        }

        private clsFile findSelectedFile()
        {
            clsFile selectedFile =
                (from file in allFiles
                 where ((file.Name == newsListView.SelectedItems[0].Text) && (file.CreationDate == DateTime.Parse(newsListView.SelectedItems[0].SubItems[1].Text)))
                 select file).FirstOrDefault();

            return selectedFile;
        }

        private void displaySelectedFile (clsFile selectedFile)
        {
            titleLabel.Text = selectedFile.Name;
            dateLabel.Text = selectedFile.CreationDate.ToString();
            bodyRichTextBox.Text = selectedFile.Body;
            previewImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            if (selectedFile is clsPhoto)
            {
                clsPhoto selectedPhoto = (clsPhoto)selectedFile;

                label3.Text = String.Empty;
                categoryLabel.Text = String.Empty;

                if (File.Exists(selectedPhoto.Location))
                {
                    if (tabControl1.TabPages.Count == 1)
                    {
                        tabControl1.TabPages.Add(hiddenTabPage);
                    }

                    using (var img = new Bitmap(selectedPhoto.Location))
                    {
                        previewImagePictureBox.Image = new Bitmap(img);
                    }
                }
            }

            else
            {
                clsNews selectedNews = (clsNews)selectedFile;

                label3.Text = "Category:";
                categoryLabel.Text = selectedNews.Category;

                try
                {
                    if (tabControl1.TabPages.Count == 2)
                    {
                        hiddenTabPage = tabControl1.TabPages[1];
                        tabControl1.TabPages.RemoveAt(1);
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

                if (previewImagePictureBox.Image != null)
                {
                    previewImagePictureBox.Image.Dispose();
                    previewImagePictureBox.Image = null;
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
            recentToolStripMenuItem.Checked = false;
            oldestToolStripMenuItem.Checked = false;
            alphabeticallyToolStripMenuItem.Checked = false;
        }

        private void onAddFormClose()
        {
            refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();
        }

        private void onEditFormClose()
        {
            int selectedIndex = newsListView.SelectedItems[0].Index;
            refreshFilesList();
            autoSortFilesList();
            addFilesListItemsToListView();

            newsListView.SelectedIndices.Clear();
            newsListView.SelectedIndices.Add(selectedIndex); 

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
                    contextMenuStrip1.Show(newsListView,new Point(e.X,e.Y));
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
            recentToolStripMenuItem.Checked = true;
        }

        private void oldestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortFilesList(SortBy.OldestDate);
            addFilesListItemsToListView();
            uncheckAllSortByItems();
            oldestToolStripMenuItem.Checked = true;
        }

        private void alphabeticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortFilesList(SortBy.Alphabetically);
            addFilesListItemsToListView();
            uncheckAllSortByItems();
            alphabeticallyToolStripMenuItem.Checked = true;
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
                newsListView.SelectedItems.Clear();
                clearAllDisplayLabels();

                deleteFile(selectedFile);

                refreshFilesList();
                autoSortFilesList();
                addFilesListItemsToListView();

                if (tabControl1.TabPages.Count == 2)
                {
                    hiddenTabPage = tabControl1.TabPages[1];
                    tabControl1.TabPages.RemoveAt(1);
                }
            }
        }
    }
}

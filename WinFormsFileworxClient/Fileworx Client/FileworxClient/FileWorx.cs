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
using static Fileworx_Client.FileWorx;

namespace Fileworx_Client
{
    public partial class FileWorx : Form
    {
        private StreamReader newsReader;
        private List<News> allNews = new List<News>();
        private TabPage hiddenTabPage;
        private User LoggedInUser;
        public enum SortBy { RecentDate, OldestDate, Alphabetically };

        public FileWorx(User loggedInUser)
        {
            InitializeComponent();

            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            label7.Text = loggedInUser.Name;

            // Hide and save hidden Tab
            hiddenTabPage = tabControl1.TabPages[1];
            tabControl1.TabPages.RemoveAt(1);

            addAllSavedNews();
            SortNews(SortBy.RecentDate);
            LoggedInUser = loggedInUser;
            if (loggedInUser.IsAdmin)
            {
                usersListToolStripMenuItem.Enabled = true;
            }
            else
            {
                usersListToolStripMenuItem.Enabled = false;
            }
        }

        public void addAllSavedNews()
        {
            allNews.Clear();
            string[] newsDirectorys = Directory.GetFiles(EditBeforeRun.newsDirectory);

            foreach (string news in newsDirectorys)
            {
                FileStream fs = new FileStream(news, FileMode.Open, FileAccess.Read);
                newsReader = new StreamReader(fs);
                string newsRecord = newsReader.ReadToEnd();
                if (newsRecord != null)
                {
                    string[] thisNews = newsRecord.Split(EditBeforeRun.Seperator, StringSplitOptions.None);

                    if (File.Exists(thisNews[6]))
                    {
                        Image newImage = new Image(thisNews);
                        News newIMG = newImage;
                        allNews.Add(newIMG);
                        addThisNewsToListView(newIMG);
                    }
                    else
                    {
                        News newNews = new News(thisNews);
                        allNews.Add(newNews);
                        addThisNewsToListView(newNews);
                    }
                }
                newsReader?.Close();
            }
        }

        private void addThisNewsToListView(News news)
        {
            var listViewNews = new ListViewItem($"{news.Title}");
            listViewNews.SubItems.Add($"{news.Date}");
            listViewNews.SubItems.Add($"{news.Body}");
            newsListView.Items.Add(listViewNews);
        }

        public void RefreshNews()
        {
            allNews.Clear();
            newsListView.Items.Clear();
            addAllSavedNews();
        }

        public void SortNews(SortBy sortby)
        {
            if (sortby == SortBy.RecentDate)
            {
                var sortedList = (from news in allNews
                                  orderby news.Date descending
                                  select news).ToList();
                allNews = sortedList;

                newsListView.Items.Clear();
                foreach (News news in allNews)
                {
                    addThisNewsToListView(news);
                }
            }

            if (sortby == SortBy.OldestDate)
            {
                var sortedList = (from news in allNews
                                  orderby news.Date ascending
                                  select news).ToList();
                allNews = sortedList;

                newsListView.Items.Clear();
                foreach (News news in allNews)
                {
                    addThisNewsToListView(news);
                }
            }
            if (sortby == SortBy.Alphabetically)
            {
                var sortedList = (from news in allNews
                                  orderby news.Title ascending
                                  select news).ToList();
                allNews = sortedList;

                newsListView.Items.Clear();
                foreach (News news in allNews)
                {
                    addThisNewsToListView(news);
                }
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Images add_Image = new Add_Images();
            DialogResult result = add_Image.ShowDialog();

            if (result == DialogResult.OK)
            {
                RefreshNews();
            }
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void clearEverylabels()
        {
            titleLabel.Text = String.Empty;
            dateLabel.Text = String.Empty;
            categoryLabel.Text = String.Empty;

            bodyRichTextBox.Text = String.Empty;


        }

        private News findSelectedNews()
        {
            News foundNews =
                (from News in allNews
                 where News.Title == (newsListView.SelectedItems[0].Text)
                 select News).FirstOrDefault();
            return foundNews;
        }

        private void recentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortNews(SortBy.RecentDate);
            uncheckAllSortByItems();
            recentToolStripMenuItem.Checked = true;
        }

        private void oldestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortNews(SortBy.OldestDate);
            uncheckAllSortByItems();
            oldestToolStripMenuItem.Checked = true;
        }

        private void alphabeticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortNews(SortBy.Alphabetically);
            uncheckAllSortByItems();
            alphabeticallyToolStripMenuItem.Checked = true;
        }

        private void uncheckAllSortByItems()
        {
            recentToolStripMenuItem.Checked = false;
            oldestToolStripMenuItem.Checked = false;
            alphabeticallyToolStripMenuItem.Checked = false;
        }

        private void newsListView_MouseClick(object sender, MouseEventArgs e)
        {


            if (e.Button == MouseButtons.Right)
            {
                News foundNews = findSelectedNews();


                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (foundNews is Image)
                    {
                        Image foundImage = (Image)foundNews;
                        if (File.Exists(foundImage.ImagePath))
                        {
                            previewImagePictureBox.Image.Dispose();
                            previewImagePictureBox.Image = null;
                        }

                        clearEverylabels();

                        if (File.Exists(foundImage.ImagePath))
                        {
                            File.Delete(foundImage.ImagePath);
                        }
                    }

                    File.Delete(foundNews.textPath);

                    newsListView.SelectedItems[0].Remove();
                    clearEverylabels();
                }

            } 

                clearEverylabels();

            if(e.Button == MouseButtons.Left) 
            {
                News foundNews = findSelectedNews();

                label3.Text = "Category:";
                titleLabel.Text = foundNews.Title;
                dateLabel.Text = foundNews.Date.ToString();
                categoryLabel.Text = foundNews.Category;
                bodyRichTextBox.Text = foundNews.Body;
                previewImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                if (foundNews is Image)
                {
                    label3.Text ="";
                    Image foundImage = (Image)foundNews;

                    if (File.Exists(foundImage.ImagePath))
                    {
                        if (tabControl1.TabPages.Count == 1)
                        {
                            tabControl1.TabPages.Add(hiddenTabPage);
                        }
                        showImage(foundImage.ImagePath);
                    }
                }

                else
                {
                    try
                    {
                        hiddenTabPage = tabControl1.TabPages[1];
                        tabControl1.TabPages.RemoveAt(1);
                        previewImagePictureBox.Image.Dispose();
                        previewImagePictureBox.Image = null;
                    }
                    catch { }
                }




                if (newsListView.SelectedItems.Count == 0)
                {
                    clearEverylabels();
                }
            }
        
            }
        

        private void newsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            News foundNews = findSelectedNews();
            if (foundNews is Image)
            {
                Image foundImage = (Image)foundNews;
                Edit_Image editImage = new Edit_Image(foundImage);

                DialogResult result = editImage.ShowDialog();
                if (result == DialogResult.OK)
                {
                    newsListView.Items.Clear();
                    addAllSavedNews();
                    clearEverylabels();
                }
                try
                {
                    hiddenTabPage = tabControl1.TabPages[1];
                    tabControl1.TabPages.RemoveAt(1);
                }
                catch { }
            }
            else
            {
                Edit_News editNews = new Edit_News(foundNews);

                DialogResult result = editNews.ShowDialog();
                if (result == DialogResult.OK)
                {
                    newsListView.Items.Clear();
                    addAllSavedNews();
                    clearEverylabels();
                }
            }
        }

        private void usersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersList usersList = new UsersList(LoggedInUser);
            usersList.ShowDialog();
        }

        private void FileWorx_Resize(object sender, EventArgs e)
        {
            tabControl1.Height = this.Height /3 ;
            newsListView.Height = splitContainer2.Panel1.Height - 10 ;
        }

        private void addNewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_News add_News = new Add_News();
            DialogResult result = add_News.ShowDialog();

            if (result == DialogResult.OK)
            {
                RefreshNews();
            }
        }
        private void showImage(string ImagePath)
        {
            if(previewImagePictureBox.Image!=null)
            {
                previewImagePictureBox.Image.Dispose();
            }


            string showedImagesPath = EditBeforeRun.ImagesDirectory + @"\ShowedImage\FileWorx";
            foreach (string file in Directory.GetFiles(showedImagesPath))
            {
                File.Delete(file);
            }
            string s = ImagePath.Substring(0, ImagePath.Length - 2); 
            string name = Path.GetFileName(s);
            string newPath = Path.Combine(showedImagesPath,name );
            File.Copy(ImagePath, newPath);

            string showedImagePath = Directory.GetFiles(showedImagesPath).FirstOrDefault();
            previewImagePictureBox.Image = new Bitmap(showedImagePath);
        }
    }
}

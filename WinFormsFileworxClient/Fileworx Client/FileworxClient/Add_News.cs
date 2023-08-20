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
    public partial class Add_News : CommonNews
    {
        public Add_News()
        {
            InitializeComponent();
            categoryComboBox.SelectedIndex = 0;
        }
        private void cancelAddNewsbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void clearAddNewsTextBoxes()
        {
            tiltleTextBox.Text = String.Empty;
            descriptionTextBox.Text = String.Empty;
            categoryComboBox.SelectedIndex = 0;
            bodyTextBox.Text = String.Empty;
        }
        private void saveAddNewsButton_Click(object sender, EventArgs e)
        {
            if ((tiltleTextBox.Text != String.Empty) && (descriptionTextBox.Text != String.Empty) && (bodyTextBox.Text != String.Empty))
            {
                Guid guid = Guid.NewGuid();

                News NewNews = new News(getAddedNewsInfo(tiltleTextBox.Text, descriptionTextBox.Text, bodyTextBox.Text, DateTime.Now.ToString(), guid, categoryComboBox.Text, String.Empty));

                writeTxtFile(NewNews);

                clearAddNewsTextBoxes();
                this.Close();
            }
            else
            {
                MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using Fileworx_Client.AddWindows;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using FileworxDTOsLibrary.RabbitMQMessages;
using FileworxObjectClassLibrary;
using FileworxObjectClassLibrary.Models;
using FileworxObjectClassLibrary.Queries;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client.MainForms
{
    public partial class frmContactsList : Form
    {
        private static List<clsContact> allContacts { get; set; }
        private bool enableEventHandlers = true;

        List<clsFile> filesToSend = new List<clsFile>();
        List<clsContact> selectedContacts = new List<clsContact>();
        private QuerySource querySource { get; set; } = QuerySource.ES;
        public event Action OnCloseAfterSend;
        public event OnFormCloseHandler AfterAddingContact;

        // RabbitMQ
        private MessagesHandling messagesHandling;

        public frmContactsList()
        {
            InitializeComponent();
        }

        // Contact List
        public static async Task<frmContactsList> Create()
        {
            var contactList = new frmContactsList();

            // UI
            contactList.cboDataStoreSource.SelectedIndex = 1;


            // Adding Contacts
            await contactList.addAllDBContactsToContactsList();
            contactList.addContactsListItemsToListView();

            return contactList;
        } 

        // Send Files
        public static async Task<frmContactsList> Create(List<clsFile> files)
        {
            var contactList = new frmContactsList();

            contactList.filesToSend = files;

            // Initialize Messages Handeling
            contactList.messagesHandling = new MessagesHandling();

            // UI
            contactList.cboDataStoreSource.SelectedIndex = 1;
            contactList.enableEventHandlers = false;
            contactList.lvwContacts.MultiSelect = true;
            contactList.lvwContacts.CheckBoxes = true;
            contactList.pnlButtons.Enabled = true;
            contactList.pnlButtons.Visible = true;
            contactList.msiAddContact.Enabled = false;
            contactList.mnuMenuStrip.Enabled = false;
            contactList.btnSend.Enabled = false;


            // Adding Contacts
            await contactList.addTransmitDBContactsToContactsList();
            contactList.addContactsListItemsToListView();

            return contactList;
        }


        private async Task addAllDBContactsToContactsList()
        {
            var contactsQuery = new clsContactQuery();
            contactsQuery.Source = querySource;
            allContacts = await contactsQuery.RunAsync();
        }

        private async Task addTransmitDBContactsToContactsList()
        {
            var contactsQuery = new clsContactQuery();
            contactsQuery.Source = querySource;
            contactsQuery.QDirection = new ContactDirection[] { ContactDirection.Transmit, (ContactDirection.Transmit|ContactDirection.Receive) };
            allContacts = await contactsQuery.RunAsync();
        }

        private void addContactsListItemsToListView()
        {
            if (lvwContacts.InvokeRequired)
            {
                // Use Invoke to marshal the operation to the UI thread
                lvwContacts.Invoke(new Action(() => addContactsListItemsToListView()));
            }

            else
            {
                if (lvwContacts.Items.Count > 0)
                {
                    lvwContacts.Items.Clear();
                }

                foreach (clsContact contact in allContacts)
                {
                    var listViewNews = new ListViewItem($"{contact.Name}");
                    if (contact.Direction == (ContactDirection.Transmit | ContactDirection.Receive)) listViewNews.SubItems.Add($"Transmit and Receive");
                    else listViewNews.SubItems.Add($"{contact.Direction.ToString()}");
                    if (!contact.Enabled)
                    {
                        listViewNews.ForeColor = Color.Gray;
                    } 
                    listViewNews.SubItems.Add($"{contact.CreationDate}");
                    lvwContacts.Items.Add(listViewNews);
                }
            }
        }

        private async Task refreshContactsList()
        {
            allContacts.Clear();
            await addAllDBContactsToContactsList();
        }

        private async Task<clsContact> findSelectedContact()
        {
            if (lvwContacts.SelectedItems.Count > 0)
            {
                clsContact selectedContact =
                                    (from file in allContacts
                                     where (file.CreationDate.ToString() == (lvwContacts.SelectedItems[0].SubItems[2].Text))
                                     select file).FirstOrDefault();
                await selectedContact.ReadAsync();
                return selectedContact;
            }

            else { return null; }
        }

        private void assignSelectedContacts()
        {
            if (lvwContacts.CheckedItems.Count > 0)
            {
               selectedContacts =
                    (from contact in allContacts
                     where lvwContacts.CheckedItems.Cast<ListViewItem>()
                           .Any(checkedItem => contact.CreationDate.ToString() == checkedItem.SubItems[2].Text)
                     select contact).ToList();
            }
        }

        private async Task onFormClose()
        {
            await refreshContactsList();
            addContactsListItemsToListView();

            if(AfterAddingContact != null)
            {
                await AfterAddingContact();
            }
        }


        //------------------------ Event Handlers ------------------------//

        private void msiAddContact_Click(object sender, EventArgs e)
        {
            var addContactWindow = new frmAddContactWindow();

            addContactWindow.OnFormClose += onFormClose;

            addContactWindow.Show();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cboDataStoreSource.SelectedIndex == 0)
            {
                querySource = QuerySource.DB;
            }
            if (cboDataStoreSource.SelectedIndex == 1)
            {
                querySource = QuerySource.ES;
            }

            await refreshContactsList();
            addContactsListItemsToListView();
        }

        private async void lvwContacts_MouseClick(object sender, MouseEventArgs e)
        {
            if (enableEventHandlers)
            {
                clsContact selectedContact = await findSelectedContact();

                if (e.Button == MouseButtons.Right)
                {
                    if (selectedContact != null)
                    {
                        cmsUsersList.Show(lvwContacts, new Point(e.X, e.Y));

                        if (!selectedContact.Enabled)
                        {
                            cmsUsersList.Items[2].Text = "Enable Contact";
                        }

                        else
                        {
                            cmsUsersList.Items[2].Text = "Disable Contact";
                        }
                    }
                }
            }
        }

        private async void cmiRemoveContact_Click(object sender, EventArgs e)
        {
            clsContact contactToRemove = await findSelectedContact();
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {contactToRemove.Name}?",
                                       "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lvwContacts.SelectedItems.Clear();

                await contactToRemove.DeleteAsync();

                await refreshContactsList();
                addContactsListItemsToListView();
            }
        }

        private async void cmiEditContact_Click(object sender, EventArgs e)
        {
            clsContact contactToEdit = await findSelectedContact();

            var editContactWindow = new frmAddContactWindow(contactToEdit);

            editContactWindow.OnFormClose += onFormClose;

            editContactWindow.Show();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                assignSelectedContacts();
                foreach (var contact in selectedContacts)
                {
                    foreach (var file in filesToSend)
                    {
                        clsMessage txMessage = new clsMessage()
                        {
                            Id = Guid.NewGuid(),
                            Command = MessagesCommands.TxFile,
                            Contact = WinFormsGlobal.MapContactToContactDto(contact),
                            ActionDate = DateTime.Now,
                        };

                        if(file is clsNews)
                        {
                            txMessage.NewsDto = WinFormsGlobal.MapNewsToNewsDto((clsNews) file);
                        }

                        else
                        {
                            txMessage.PhotoDto = WinFormsGlobal.MapPhotoToPhotoDto((clsPhoto)file);
                        }
                        await messagesHandling.SendTxFileMessage(txMessage);
                    }
                }

                MessageBox.Show("Files Sent successfully");

                if (OnCloseAfterSend != null)
                {
                    OnCloseAfterSend();
                }

                this.Close();
            }

            catch
            {
                MessageBox.Show("An Error Occurred While Sending Files");
            }
        }

        private void lvwContacts_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(lvwContacts.CheckedItems.Count > 0)
            {
                btnSend.Enabled = true;
            }
            else 
            {
                btnSend.Enabled = false;
            }
            ListViewItem checkedItem = e.Item;
            
            if (checkedItem.ForeColor == Color.Gray)
            {
                e.Item.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private async void disableContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedContact = await findSelectedContact();

            if (cmsUsersList.Items[2].Text == "Enable Contact")
            {
                selectedContact.Enabled = true;
                await selectedContact.UpdateAsync();
                await refreshContactsList();
                addContactsListItemsToListView();
            }

            else
            {
                selectedContact.Enabled = false;
                await selectedContact.UpdateAsync();
                await refreshContactsList();
                addContactsListItemsToListView();
            }
        }

    }
}

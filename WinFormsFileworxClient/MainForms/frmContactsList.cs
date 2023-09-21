using Fileworx_Client.AddWindows;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.DTOs;
using FileworxDTOsLibrary.RabbitMQMessages;
using FileworxObjectClassLibrary;
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

        private IConnection connection;
        private IModel channel;

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

            // Initialize RabbitMQ
            contactList.rabbitMQInit();

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

        private void rabbitMQInit()
        {
            var factory = new ConnectionFactory { HostName = EditBeforeRun.HostName };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
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
                            Contact = mapContactToContactDto(contact)
                        };

                        if(file is clsNews)
                        {
                            txMessage.NewsDto = mapNewsToNewsDto((clsNews) file);
                        }

                        else
                        {
                            txMessage.PhotoDto = mapPhotoToPhotoDto((clsPhoto)file);
                        }
                        await txMessage.InsertAsync();
                        sendTxFileMessage(txMessage);
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

        private void sendTxFileMessage(clsMessage txMessage)
        {
            channel.QueueDeclare(queue: EditBeforeRun.TxFileQueue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Serialize the rxMessage object to JSON
            var jsonMessage = JsonConvert.SerializeObject(txMessage);

            // Convert the JSON string to bytes
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // Publish the JSON message
            channel.BasicPublish(exchange: string.Empty,
                                    routingKey: EditBeforeRun.TxFileQueue,
                                    basicProperties: null,
                                    body: body);

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

        //------------------------ Mapping ------------------------//

        private FileworxDTOsLibrary.DTOs.clsContactDto mapContactToContactDto(clsContact contact)
        {
            var contactDto = new FileworxDTOsLibrary.DTOs.clsContactDto()
            {
                Id = contact.Id,
                Description = contact.Description,
                CreationDate = contact.CreationDate,
                ModificationDate = contact.ModificationDate,
                CreatorId = contact.CreatorId,
                CreatorName = contact.CreatorName,
                LastModifierId = contact.LastModifierId,
                Name = contact.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)contact.Class,
                TransmitLocation = contact.TransmitLocation,
                ReceiveLocation = contact.ReceiveLocation,
                Direction = (FileworxDTOsLibrary.DTOs.Direction)(int)contact.Direction,
                LastReceiveDate = contact.LastReceiveDate,
                Enabled = contact.Enabled,
            };

            return contactDto;
        }

        private clsPhotoDto mapPhotoToPhotoDto(clsPhoto photo)
        {
            var photoDto = new clsPhotoDto()
            {
                Id = photo.Id,
                Description = photo.Description,
                CreationDate = photo.CreationDate,
                ModificationDate = photo.ModificationDate,
                CreatorId = photo.CreatorId,
                CreatorName = photo.CreatorName,
                LastModifierId = photo.LastModifierId,
                Name = photo.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)photo.Class,
                Body = photo.Body,
                Location = photo.Location,
            };

            return photoDto;
        }

        private clsNewsDto mapNewsToNewsDto(clsNews news)
        {
            var newsDto = new clsNewsDto()
            {
                Id = news.Id,
                Description = news.Description,
                CreationDate = news.CreationDate,
                ModificationDate = news.ModificationDate,
                CreatorId = news.CreatorId,
                CreatorName = news.CreatorName,
                LastModifierId = news.LastModifierId,
                Name = news.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)news.Class,
                Body = news.Body,
                Category = news.Category,
            };

            return newsDto;
        }
    }
}

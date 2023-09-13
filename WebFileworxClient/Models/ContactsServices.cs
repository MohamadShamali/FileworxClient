using Elastic.Clients.Elasticsearch.Mapping;
using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Web_Fileworx_Client.Models
{
    public class ContactsServices
    {
        public List<clsContact> AllContacts = new List<clsContact>();
        public clsContact? SelectedContact { get; set; }
        public QuerySource QuerySource { get; set; } = QuerySource.ES;

        public async Task AddDBContactsToContactsList()
        {

            clsContactQuery query = new clsContactQuery();
            query.Source = QuerySource.ES;
            AllContacts = await query.Run();
        }

        public async Task RefreshContactsList()
        {
            await AddDBContactsToContactsList();
        }

    }
}

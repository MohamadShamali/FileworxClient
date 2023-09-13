using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxObjectClassLibrary
{
    public class clsContactDto : clsBusinessObject 
    {
        // Properties
        public string TransmitLocation { get; set; } = String.Empty;
        public string ReceiveLocation { get; set; } = String.Empty;
        public int Direction { get; set; }
        public DateTime LastReceiveDate { get; set; }
        public bool Enabled { get; set; } = true;


        // For Reading from Elastic
        public clsContactDto()
        {
             
        }

        // For writing to Elastic
        public clsContactDto(clsContact contact)
        {
            Id = contact.Id;
            Description = contact.Description;
            CreationDate = contact.CreationDate;
            ModificationDate = contact.ModificationDate;
            CreatorId = contact.CreatorId;
            CreatorName = contact.CreatorName;
            LastModifierId = contact.LastModifierId;
            LastModifierName = contact.LastModifierName;
            Name = contact.Name;
            Class = contact.Class;
            TransmitLocation= contact.TransmitLocation;
            ReceiveLocation= contact.ReceiveLocation;
            Direction = (int) contact.Direction;
            LastReceiveDate = contact.LastReceiveDate;
            Enabled = contact.Enabled;
        }


    }
}

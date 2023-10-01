using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileworxObjectClassLibrary.Models;

namespace FileworxObjectClassLibrary.Others
{
    public class clsContactElasticDto : clsBusinessObject
    {
        // Properties
        public string TransmitLocation { get; set; } = string.Empty;
        public string ReceiveLocation { get; set; } = string.Empty;
        public int Direction { get; set; }
        public DateTime LastReceiveDate { get; set; }
        public bool Enabled { get; set; } = true;


        // For Reading from Elastic
        public clsContactElasticDto()
        {

        }

        // For writing to Elastic
        public clsContactElasticDto(clsContact contact)
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
            TransmitLocation = contact.TransmitLocation;
            ReceiveLocation = contact.ReceiveLocation;
            Direction = (int)contact.Direction;
            LastReceiveDate = contact.LastReceiveDate;
            Enabled = contact.Enabled;
        }


    }
}

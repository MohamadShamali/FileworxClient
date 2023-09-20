using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFolderService.DTOs
{
    public enum Type { User = 1, News = 2, Photo = 3, Contact = 4 }
    public enum ContactDirection { Transmit = 1, Receive = 2}
    internal class clsContactDto
    {
        // Properties
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; } 
        public Guid? LastModifierId { get; set; } = null;
        public string LastModifierName { get; set; } 
        public string Name { get; set; }
        public Type Class { get; set; }
        public string TransmitLocation { get; set; } 
        public string ReceiveLocation { get; set; } 
        public ContactDirection Direction { get; set; } 
        public DateTime LastReceiveDate { get; set; }
        public bool Enabled { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxDTOsLibrary.DTOs
{
    public enum Type { User = 1, News = 2, Photo = 3, Contact = 4 }
    public class clsBusinessObjectDto
    {
        // Properties
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; } = String.Empty;
        public Guid? LastModifierId { get; set; } = null;
        public string LastModifierName { get; set; } = String.Empty;
        public string Name { get; set; }
        public Type Class { get; set; }
    }
}

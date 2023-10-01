using FileworxDTOsLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxDTOsLibrary.RabbitMQMessages
{
    public class clsMessage
    {
        // Properties
        public Guid Id { get; set; }
        public string Command { get; set; }
        public clsNewsDto NewsDto { get; set; }
        public clsPhotoDto PhotoDto { get; set; }
        public bool Processed { get; set; } = false;
        public clsContactDto Contact { get; set; }
        public DateTime ActionDate { get; set; }

    }
}

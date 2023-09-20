using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileworxDTOsLibrary.DTOs
{
    public enum Direction
    {
        Transmit = 1,
        Receive = 2
    };
    public class clsContactDto : clsBusinessObjectDto
    {
        // Properties
        public string TransmitLocation { get; set; } = String.Empty;
        public string ReceiveLocation { get; set; } = String.Empty;
        public Direction Direction { get; set; } = (Direction.Transmit | Direction.Receive);
        public DateTime LastReceiveDate { get; set; }
        public bool Enabled { get; set; } = true;
    }
}

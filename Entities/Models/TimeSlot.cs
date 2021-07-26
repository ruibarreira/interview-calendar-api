using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }
        public DateTime Registration { get; set; }
        public DateTime LastUpdateRegistration { get; set; }
        public DateTime TimeSlotStart { get; set; }
        public DateTime TimeSlotEnd { get; set; }
    }
}

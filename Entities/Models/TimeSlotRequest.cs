using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TimeSlotRequest
    {
        [Key]
        public int TimeSlotRequestId { get; set; }
        public DateTime Registration { get; set; }
        public DateTime LastUpdateRegistration { get; set; }
        [ForeignKey(nameof(TimeSlot))]
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        [ForeignKey(nameof(Candidate))]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}

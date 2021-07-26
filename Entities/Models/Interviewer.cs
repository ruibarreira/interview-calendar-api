using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Interviewer
    {
        [Key]
        public int InterviewerId { get; set; }
        public DateTime Registration { get; set; }
        public DateTime LastUpdateRegistration { get; set; }
        [Required(ErrorMessage = "Interviewer name is a required field.")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

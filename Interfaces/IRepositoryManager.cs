using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepositoryManager
    {
        ICandidateRepository Candidate { get; }
        IInterviewerRepository Interviewer { get; }
        ITimeSlotRepository TimeSlot { get; }
        ITimeSlotAvailabilityRepository TimeSlotAvailability { get; }
        ITimeSlotRequestRepository TimeSlotRequest { get; }
        Task SaveAsync();
    }
}

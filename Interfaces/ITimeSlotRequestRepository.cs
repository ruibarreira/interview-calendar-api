using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Interfaces
{
    public interface ITimeSlotRequestRepository
    {
        Task<TimeSlotRequest> GetTimeSlotRequestAsync(int timeSlotId, int candidateId, bool trackChanges);
        void CreateTimeSlotRequest(TimeSlotRequest timeSlotRequest);
    }
}

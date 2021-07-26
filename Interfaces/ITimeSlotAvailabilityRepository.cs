using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Interfaces
{
    public interface ITimeSlotAvailabilityRepository
    {
        Task<TimeSlotAvailability> GetTimeSlotAvailabilityAsync(int timeSlotId, int interviewerId, bool trackChanges);
        void CreateTimeSlotAvailability(TimeSlotAvailability timeSlotAvailability);
    }
}

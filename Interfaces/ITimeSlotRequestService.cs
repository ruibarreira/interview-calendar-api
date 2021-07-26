using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITimeSlotRequestService
    {
        Task ReferenceTimeSlotsAsync(int candidateId, List<TimeSlot> timeSlots);
    }
}

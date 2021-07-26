using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlot> GetTimeSlotAsync(DateTime timeSlotStart, DateTime timeSlotEnd, bool trackChanges);
        void CreateTimeSlot(TimeSlot timeSlot);
        Task<List<TimeSlot>> GetTimeSlotsAsync(int candidateId, List<int> interviewerIds);
    }
}

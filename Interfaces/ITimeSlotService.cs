using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Interfaces
{
    public interface ITimeSlotService
    {
        Task<List<TimeSlot>> CreateTimeSlotsAsync(List<TimeSlotDto> timeSlots);
    }
}

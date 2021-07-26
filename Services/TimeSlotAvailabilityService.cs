using Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TimeSlotAvailabilityService : ITimeSlotAvailabilityService
    {
        private readonly IRepositoryManager _repository;

        public TimeSlotAvailabilityService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task ReferenceTimeSlotsAsync(int interviewerId, List<TimeSlot> timeSlots) 
        {
            // Check if the interviewer already has reference to each time slot and add it if not
            foreach (var timeSlot in timeSlots)
            {
                var timeSlotAvailability = await _repository.TimeSlotAvailability.GetTimeSlotAvailabilityAsync(
                    timeSlot.TimeSlotId, interviewerId, false
                );
                if (timeSlotAvailability == null)
                {
                    _repository.TimeSlotAvailability.CreateTimeSlotAvailability(
                        new TimeSlotAvailability
                        {
                            Registration = DateTime.UtcNow,
                            LastUpdateRegistration = DateTime.UtcNow,
                            TimeSlotId = timeSlot.TimeSlotId,
                            InterviewerId = interviewerId
                        }
                    );
                    await _repository.SaveAsync();
                }
            }
        }
    }
}

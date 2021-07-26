using Entities.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TimeSlotRequestService : ITimeSlotRequestService
    {
        private readonly IRepositoryManager _repository;

        public TimeSlotRequestService(IRepositoryManager repository) 
        {
            _repository = repository;
        }

        public async Task ReferenceTimeSlotsAsync(int candidateId, List<TimeSlot> timeSlots) 
        {
            // Check if the candidate already has reference to each time slot and add it if not
            foreach (var timeSlot in timeSlots)
            {
                TimeSlotRequest timeSlotRequest = await _repository.TimeSlotRequest.GetTimeSlotRequestAsync(timeSlot.TimeSlotId, candidateId, false);
                if (timeSlotRequest == null)
                {
                    _repository.TimeSlotRequest.CreateTimeSlotRequest(
                        new TimeSlotRequest
                        {
                            Registration = DateTime.UtcNow,
                            LastUpdateRegistration = DateTime.UtcNow,
                            TimeSlotId = timeSlot.TimeSlotId,
                            CandidateId = candidateId
                        }
                    );
                    await _repository.SaveAsync();
                }
            }
        }
    }
}

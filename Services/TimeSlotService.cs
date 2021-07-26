using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Interfaces;

namespace Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public TimeSlotService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TimeSlot>> CreateTimeSlotsAsync(List<TimeSlotDto> TimeSlotDtos)
        {
            // Create new time slots not yet created and get a list with all of them
            List<TimeSlot> timeSlots = new();
            var timeSlotEntities = _mapper.Map<IEnumerable<TimeSlot>>(TimeSlotDtos);
            foreach (var timeSlotEntity in timeSlotEntities)
            {
                DateTime start = timeSlotEntity.TimeSlotStart;
                DateTime nextHour = start.AddHours(1);
                do
                {
                    var dbTimeSlot = await _repository.TimeSlot.GetTimeSlotAsync(start, nextHour, false);
                    if (dbTimeSlot == null)
                    {
                        _repository.TimeSlot.CreateTimeSlot(
                            new TimeSlot
                            {
                                Registration = DateTime.UtcNow,
                                LastUpdateRegistration = DateTime.UtcNow,
                                TimeSlotStart = start,
                                TimeSlotEnd = nextHour
                            }
                        );
                        await _repository.SaveAsync();
                    }
                    timeSlots.Add(await _repository.TimeSlot.GetTimeSlotAsync(start, nextHour, false));

                    start = nextHour;
                    nextHour = nextHour.AddHours(1);
                } while (nextHour <= timeSlotEntity.TimeSlotEnd);
            }
            return timeSlots;
        }
    }
}

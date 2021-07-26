using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewCalendarAPI.Controllers
{
    [Route("api/interviewers")]
    [ApiController]
    public class InterviewersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeSlotAvailabilityService _timeSlotAvailabilityService;

        public InterviewersController(IRepositoryManager repository, IMapper mapper, 
            ITimeSlotService timeSlotService, ITimeSlotAvailabilityService timeSlotAvailabilityService)
        {
            _repository = repository;
            _mapper = mapper;
            _timeSlotService = timeSlotService;
            _timeSlotAvailabilityService = timeSlotAvailabilityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInterviewers()
        {
            var interviewers = await _repository.Interviewer.GetAllInterviewersAsync(trackChanges: false);
            var interviewersDto = _mapper.Map<IEnumerable<InterviewerDto>>(interviewers);
            return Ok(new DataTransferObject { data = interviewersDto });
        }

        [HttpGet("{interviewerId}")]
        public async Task<IActionResult> GetInterviewer(int interviewerId)
        {
            var interviewer = await _repository.Interviewer.GetInterviewerAsync(interviewerId, trackChanges: false);
            if (interviewer == null)
                return NotFound(new DataTransferObject { message = $"Interviewer with id {interviewerId} doesn't exist." });
            else
            {
                var interviewerDto = _mapper.Map<InterviewerDto>(interviewer);
                return Ok(new DataTransferObject { data = interviewerDto });
            }
        }

        [HttpPut("{interviewerId}")]
        public async Task<IActionResult> SetAvailabilityTimeSlot(int interviewerId, [FromBody] TimeSlotListDto timeSlotInput) 
        {
            if (timeSlotInput.TimeSlots == null || timeSlotInput.TimeSlots.Count == 0)
                return BadRequest(new DataTransferObject { message = "There are no time slot inputs" });

            var interviewer = await _repository.Interviewer.GetInterviewerAsync(interviewerId, trackChanges: false);
            if (interviewer == null)
                return NotFound(new DataTransferObject { message = $"Interviewer with id {interviewerId} doesn't exist." });

            // Time Slot Validations
            if (timeSlotInput.TimeSlots.Exists(ts => ts.TimeSlotStart < DateTime.UtcNow
            || ts.TimeSlotEnd < DateTime.UtcNow
            || ts.TimeSlotStart >= ts.TimeSlotEnd))
            {
                return BadRequest(new DataTransferObject
                {
                    message = "Time slots must be in the future and the start date lower than the end date"
                });
            }

            if (timeSlotInput.TimeSlots.Exists(ts => ts.TimeSlotStart.Minute > 0 || ts.TimeSlotStart.Second > 0
            || ts.TimeSlotEnd.Minute > 0 || ts.TimeSlotEnd.Second > 0
            || (ts.TimeSlotEnd - ts.TimeSlotStart).Hours < 1))
            {
                return BadRequest(new DataTransferObject
                {
                    message = "Time slots must be at least 1-hour period of time that spreads from the beginning of any hour until the beginning of the next hour"
                });
            }

            var availableTimeSlots = await _timeSlotService.CreateTimeSlotsAsync(timeSlotInput.TimeSlots);
            await _timeSlotAvailabilityService.ReferenceTimeSlotsAsync(interviewerId, availableTimeSlots);
            return Ok();
        }
    }
}

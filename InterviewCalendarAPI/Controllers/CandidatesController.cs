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
    [Route("api/candidates")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeSlotRequestService _timeSlotRequestService;

        public CandidatesController(IRepositoryManager repository, IMapper mapper, 
            ITimeSlotService timeSlotService, ITimeSlotRequestService timeSlotRequestService)
        {
            _repository = repository;
            _mapper = mapper;
            _timeSlotService = timeSlotService;
            _timeSlotRequestService = timeSlotRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCandidates()
        {
            var candidates = await _repository.Candidate.GetAllCandidatesAsync(trackChanges: false);
            var candidatesDto = _mapper.Map<IEnumerable<CandidateDto>>(candidates);
            return Ok(new DataTransferObject { data = candidatesDto });
        }

        [HttpGet("{candidateId}")]
        public async Task<IActionResult> GetCandidate(int candidateId)
        {
            var candidate = await _repository.Candidate.GetCandidateAsync(candidateId, trackChanges: false);
            if (candidate == null)
                return NotFound(new DataTransferObject { message = $"Candidate with id {candidateId} doesn't exist." });
            else
            {
                var candidateDto = _mapper.Map<CandidateDto>(candidate);
                return Ok(new DataTransferObject { data = candidateDto });
            }
        }

        [HttpPut("{candidateId}")]
        public async Task<IActionResult> RequestTimeSlot(int candidateId, [FromBody] TimeSlotListDto timeSlotInput)
        {
            if (timeSlotInput.TimeSlots == null || timeSlotInput.TimeSlots.Count == 0)
                return BadRequest(new DataTransferObject { message = "There are no time slot inputs" });

            var candidate = await _repository.Candidate.GetCandidateAsync(candidateId, trackChanges: false);
            if (candidate == null)
                return NotFound(new DataTransferObject { message = $"Candidate with id {candidateId} doesn't exist." });

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

            var requestedTimeSlots = await _timeSlotService.CreateTimeSlotsAsync(timeSlotInput.TimeSlots);
            await _timeSlotRequestService.ReferenceTimeSlotsAsync(candidateId, requestedTimeSlots);
            return Ok();
        }
    }
}

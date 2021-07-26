using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Interfaces;
using InterviewCalendarAPI.ModelBinders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewCalendarAPI.Controllers
{
    [Route("api/timeslots")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public TimeSlotsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{candidateId}")]
        public async Task<IActionResult> GetTimeSlots(int candidateId, [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> interviewerIds)
        {
            var candidate = await _repository.Candidate.GetCandidateAsync(candidateId, trackChanges: false);
            if (candidate == null)
                return NotFound(new DataTransferObject { message = $"Candidate with id {candidateId} doesn't exist." });

            foreach (var interviewerId in interviewerIds)
            {
                var interviewer = await _repository.Interviewer.GetInterviewerAsync(interviewerId, trackChanges: false);
                if (interviewer == null)
                    return NotFound(new DataTransferObject { message = $"Interviewer with id {interviewerId} doesn't exist." });
            }

            List<TimeSlot> timeSlots = await _repository.TimeSlot.GetTimeSlotsAsync(candidateId, interviewerIds.ToList());
            var timeSlotsDto = _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
            return Ok(timeSlotsDto);
        }
    }
}

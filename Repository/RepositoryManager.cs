using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private ICandidateRepository _candidateRepository;
        private IInterviewerRepository _interviewerRepository;
        private ITimeSlotRepository _timeSlotRepository;
        private ITimeSlotAvailabilityRepository _timeSlotAvailabilityRepository;
        private ITimeSlotRequestRepository _timeSlotRequestRepository;
        private IConfiguration _configuration;

        public RepositoryManager(RepositoryContext repositoryContext, IConfiguration configuration)
        {
            _repositoryContext = repositoryContext;
            _configuration = configuration;
        }

        public ICandidateRepository Candidate
        {
            get
            {
                if (_candidateRepository == null)
                    _candidateRepository = new CandidateRepository(_repositoryContext);
                return _candidateRepository;
            }
        }

        public IInterviewerRepository Interviewer
        {
            get
            {
                if (_interviewerRepository == null)
                    _interviewerRepository = new InterviewerRepository(_repositoryContext);
                return _interviewerRepository;
            }
        }

        public ITimeSlotRepository TimeSlot
        {
            get
            {
                if (_timeSlotRepository == null)
                    _timeSlotRepository = new TimeSlotRepository(_repositoryContext, _configuration);
                return _timeSlotRepository;
            }
        }

        public ITimeSlotAvailabilityRepository TimeSlotAvailability
        {
            get
            {
                if (_timeSlotAvailabilityRepository == null)
                    _timeSlotAvailabilityRepository = new TimeSlotAvailabilityRepository(_repositoryContext);
                return _timeSlotAvailabilityRepository;
            }
        }

        public ITimeSlotRequestRepository TimeSlotRequest
        {
            get
            {
                if (_timeSlotRequestRepository == null)
                    _timeSlotRequestRepository = new TimeSlotRequestRepository(_repositoryContext);
                return _timeSlotRequestRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TimeSlotRequestRepository : RepositoryBase<TimeSlotRequest>, ITimeSlotRequestRepository
    {
        public TimeSlotRequestRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<TimeSlotRequest> GetTimeSlotRequestAsync(int timeSlotId, int candidateId, bool trackChanges) =>
            await FindByCondition(tsr => tsr.TimeSlotId.Equals(timeSlotId) && tsr.CandidateId.Equals(candidateId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateTimeSlotRequest(TimeSlotRequest timeSlotRequest) => Create(timeSlotRequest);
    }
}

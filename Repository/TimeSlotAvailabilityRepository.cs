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
    public class TimeSlotAvailabilityRepository : RepositoryBase<TimeSlotAvailability>, ITimeSlotAvailabilityRepository
    {
        public TimeSlotAvailabilityRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<TimeSlotAvailability> GetTimeSlotAvailabilityAsync(int timeSlotId, int interviewerId, bool trackChanges) =>
            await FindByCondition(tsa => tsa.TimeSlotId.Equals(timeSlotId) && tsa.InterviewerId.Equals(interviewerId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateTimeSlotAvailability(TimeSlotAvailability timeSlotAvailability) => Create(timeSlotAvailability);
    }
}

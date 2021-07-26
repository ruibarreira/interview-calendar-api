using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Entities;
using Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
    {
        private readonly IConfiguration _configuration;
        public TimeSlotRepository(RepositoryContext repositoryContext, IConfiguration configuration)
        : base(repositoryContext)
        {
            _configuration = configuration;
        }

        public async Task<TimeSlot> GetTimeSlotAsync(DateTime timeSlotStart, DateTime timeSlotEnd, bool trackChanges) =>
            await FindByCondition(t => t.TimeSlotStart.Equals(timeSlotStart) && t.TimeSlotEnd.Equals(timeSlotEnd), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateTimeSlot(TimeSlot timeSlot) => Create(timeSlot);

        public async Task<List<TimeSlot>> GetTimeSlotsAsync(int candidateId, List<int> interviewerIds) 
        {
            List<TimeSlot> timeSlots;
            using (var db = new SqlConnection(_configuration.GetConnectionString("sqlConnection")))
            {
                string query = @"select TimeSlotStart,TimeSlotEnd from dbo.TimeSlots where TimeSlotId in (
	                             select tsr.TimeSlotId from TimeSlotRequests tsr where tsr.CandidateId = @candidateId";
                foreach (var interviewerId in interviewerIds)
                {
                    query += string.Format(" intersect select tsa.TimeSlotId from TimeSlotAvailabilities tsa where tsa.InterviewerId = {0}", interviewerId);
                }
                query += ")";
                var result = await db.QueryAsync<TimeSlot>(query, new { candidateId }, commandType: CommandType.Text).ConfigureAwait(false);
                timeSlots = result.ToList();
            }
            return timeSlots;
        }
    }
}

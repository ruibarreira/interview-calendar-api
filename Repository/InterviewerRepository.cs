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
    public class InterviewerRepository : RepositoryBase<Interviewer>, IInterviewerRepository
    {
        public InterviewerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Interviewer>> GetAllInterviewersAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(i => i.Name)
            .ToListAsync();

        public async Task<Interviewer> GetInterviewerAsync(int interviewerId, bool trackChanges) =>
            await FindByCondition(i => i.InterviewerId.Equals(interviewerId), trackChanges)
            .SingleOrDefaultAsync();
    }

}

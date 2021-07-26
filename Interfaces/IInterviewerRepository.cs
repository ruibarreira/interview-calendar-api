using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IInterviewerRepository
    {
        Task<IEnumerable<Interviewer>> GetAllInterviewersAsync(bool trackChanges);
        Task<Interviewer> GetInterviewerAsync(int interviewerId, bool trackChanges);
    }
}

using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync(bool trackChanges);
        Task<Candidate> GetCandidateAsync(int candidateId, bool trackChanges);
    }
}

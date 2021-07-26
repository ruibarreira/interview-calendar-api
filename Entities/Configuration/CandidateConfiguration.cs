using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder) 
        {
            builder.HasData
            (
                new Candidate 
                {
                    CandidateId = 1,
                    Registration = DateTime.UtcNow,
                    LastUpdateRegistration = DateTime.UtcNow,
                    Name = "John"
                }
            );
        }
    }
}

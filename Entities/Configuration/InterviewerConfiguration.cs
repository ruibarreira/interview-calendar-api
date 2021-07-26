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
    public class InterviewerConfiguration : IEntityTypeConfiguration<Interviewer>
    {
        public void Configure(EntityTypeBuilder<Interviewer> builder)
        {
            builder.HasData
            (
                new Interviewer
                {
                    InterviewerId = 1,
                    Registration = DateTime.UtcNow,
                    LastUpdateRegistration = DateTime.UtcNow,
                    Name = "Mary"
                },
                new Interviewer 
                {
                    InterviewerId = 2,
                    Registration = DateTime.UtcNow,
                    LastUpdateRegistration = DateTime.UtcNow,
                    Name = "Diana"
                }
            );
        }
    }
}

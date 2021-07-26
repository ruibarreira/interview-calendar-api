using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSlot>()
                .HasIndex(ts => new { ts.TimeSlotStart, ts.TimeSlotEnd })
                .IsUnique(true);

            modelBuilder.ApplyConfiguration(new CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new InterviewerConfiguration());
        }
        
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<TimeSlotAvailability> TimeSlotAvailabilities { get; set; }
        public DbSet<TimeSlotRequest> TimeSlotRequests { get; set; }
    }
}

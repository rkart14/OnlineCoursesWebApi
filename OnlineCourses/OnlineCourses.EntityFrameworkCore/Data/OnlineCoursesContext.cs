using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.SeedWork;
using OnlineCourses.EntityFrameworkCore.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.EntityFrameworkCore.Data
{
    public class OnlineCoursesContext : DbContext, IUnitOfWork, IDomainEventContext
    {
        public const string DefaultSchema = "OnlineCoursesDb";


        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lecturer> Lecturers{ get; set; }

        public OnlineCoursesContext(DbContextOptions<OnlineCoursesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new LecturerConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _ = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public IEnumerable<Event> GetDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<IAggregatRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents);
        }
    }
}

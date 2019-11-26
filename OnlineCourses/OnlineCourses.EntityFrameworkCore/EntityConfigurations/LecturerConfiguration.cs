using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCourses.Domain.Entities;
using OnlineCourses.EntityFrameworkCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.EntityFrameworkCore.EntityConfigurations
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.ToTable("Lecturers", OnlineCoursesContext.DefaultSchema);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name);
            
            builder.Ignore(b => b.DomainEvents);

        }
    }
}

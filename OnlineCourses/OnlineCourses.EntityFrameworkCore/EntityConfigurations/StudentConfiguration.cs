using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCourses.EntityFrameworkCore.Data;

namespace OnlineCourses.EntityFrameworkCore.EntityConfigurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students", OnlineCoursesContext.DefaultSchema);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name);
            builder.Property(p => p.Age);
            builder.Property(p => p.Email);

            builder.Ignore(b => b.DomainEvents);
        }
    }
}

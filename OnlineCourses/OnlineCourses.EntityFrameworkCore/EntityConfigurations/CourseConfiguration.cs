using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCourses.Domain.Entities;
using OnlineCourses.EntityFrameworkCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.EntityFrameworkCore.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses", OnlineCoursesContext.DefaultSchema);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AverageStudentAge);
            builder.Property(p => p.EnrolledStudentsCount);
            builder.Property(p => p.Name);
            builder.Property(p => p.MaximumStudentAge);
            builder.Property(p => p.MinimumStudentAge);
            builder.Property(p => p.IsAvailableToEnroll);
            builder.Property(p => p.LecturerName);
            builder.Property(p => p.LecturerId);

            builder.HasMany(c=> c.EnrolledStudents)
                .WithOne(s=> s.Course)
                .HasForeignKey("CourseId");

            builder.Property<byte[]>("RowVersion")
             .IsRowVersion();
        }
    }
}

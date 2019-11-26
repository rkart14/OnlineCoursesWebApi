using OnlineCourses.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Domain.DomainEvents
{
    public class CourseMaximumStudentAgeIncreasedDomainEvent : Event
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
    }
}

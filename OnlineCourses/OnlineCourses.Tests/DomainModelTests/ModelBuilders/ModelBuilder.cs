using OnlineCourses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Tests.DomainModelTests.ModelBuilders
{
    public static class ModelBuilder
    {
        public static Course BuildCourse(int totalCapacity)
        {
            return new Course("testCourse", totalCapacity, "lec", new Guid());
        }
        public static Student BuildStudent(int age)
        {
            return new Student("testname", "testEmail", age);
        }
    }
}

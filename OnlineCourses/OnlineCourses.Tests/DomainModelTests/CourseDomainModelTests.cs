using OnlineCourses.Domain;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Shared;
using OnlineCourses.Shared.ApplicationExceptions;
using OnlineCourses.Tests.DomainModelTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnlineCourses.Tests.DomainModelTests
{
    public class CourseDomainModelTests
    {
        [Fact]
        public void Should_Throw_ConflictOccuredException_For_InApproprate_Maximum_Students_Capacity()
        {
            Exception ex = Assert.Throws<ConflictOccuredException>(() => { ModelBuilder.BuildCourse(-1); });
            Assert.Equal(typeof(ConflictOccuredException), ex.GetType());
        }

        [Fact]
        public void CourseStatistics_Changed_After_Enroll()
        {
            int student1Age = Consts.MinimalStudentAge;
            int student2Age = student1Age + 2;
            Course course = ModelBuilder.BuildCourse(5);
            Student student1 = ModelBuilder.BuildStudent(student1Age);
            Student student2 = ModelBuilder.BuildStudent(student2Age);
            
            course.EnrollStudent(student1);
            
            Assert.Equal(student1Age, course.MinimumStudentAge);
            Assert.Equal(student1Age, course.MaximumStudentAge);
            Assert.Equal(student1Age, course.AverageStudentAge);
            Assert.Equal(1, course.EnrolledStudentsCount);

            course.EnrollStudent(student2);
            
            Assert.Equal(student1Age, course.MinimumStudentAge);
            Assert.Equal(student2Age, course.MaximumStudentAge);
            Assert.Equal(student1Age + 1, course.AverageStudentAge);
            Assert.Equal(2, course.EnrolledStudentsCount);
        }

        [Fact]
        public void Should_Throw_Course_Cant_Take_Mode_Students_ConflictOccured_Exception()
        {
            int studentNormalAge = Consts.MinimalStudentAge + 3;
            Course course = ModelBuilder.BuildCourse(1);
            Student student1 = ModelBuilder.BuildStudent(studentNormalAge);
            Student student2 = ModelBuilder.BuildStudent(studentNormalAge);
            course.EnrollStudent(student1);
            Exception ex = Assert.Throws<ConflictOccuredException>(() => { course.EnrollStudent(student2);});
            Assert.Equal(typeof(ConflictOccuredException), ex.GetType());
        }
    }
}

using OnlineCourses.Domain;
using OnlineCourses.Shared;
using OnlineCourses.Shared.ApplicationExceptions;
using OnlineCourses.Tests.DomainModelTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnlineCourses.Tests.DomainModelTests
{
    public class StudentDomainModelTests
    {
        [Fact]
        public void Should_Throw_ConflictOccuredException_For_InApproprate_Age()
        {
            int studentAge = Consts.MinimalStudentAge;
            studentAge--;
            Exception ex = Assert.Throws<ConflictOccuredException>(() => { ModelBuilder.BuildStudent(studentAge); });
            Assert.Equal(typeof(ConflictOccuredException), ex.GetType());
        }
    }
}

using MediatR;
using OnlineCourses.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OnlineCourses.Application.Commands
{
    [DataContract]
    public class EnrollStudentToCourseCommand : IRequest
    {
        public Guid CourseId { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string StudentEmail { get; set; }

        [DataMember]
        public int StudentAge { get; set; }
    }
}

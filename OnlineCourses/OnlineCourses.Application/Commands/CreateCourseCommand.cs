using MediatR;
using OnlineCourses.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.Commands
{
    public class CreateCourseCommand : IRequest<CreateCourseResultViewModel>
    {
        public Guid LecturerId { get; set; }
        
        public string CourseName { get; set; }

        public int CourseMaximumStudentsCount { get; set; }
    }
}

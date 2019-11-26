using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.ProcessorEvents
{
    public class EnrollStudentToCourseProcessingCommand
    {
        public string StudentName { get; set; }

        public string StudentEmail { get; set; }

        public Guid CourseId { get; set; }

        public int StudentAge { get; set; }
    }
}

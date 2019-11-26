using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.ViewModels
{
    public class CourseStatisticsViewModel
    {
        public Guid CourseId { get; set; }

        public int MinimumAgeOfStudents { get; set; }

        public int MaximumAgeOfStudents { get; set; }

        public int AverageAgeOfStudents { get; set; }

        public int TotalCapacity { get; set; }
          
        public int CurrentNumberOfStudents { get; set; }
    }

    public class CourseDetailedInfoViewModel 
    {
        public CourseStatisticsViewModel Statistics { get; set; }

        public LecturerInfoViewModel Lecturer { get; set; }

        public IReadOnlyList<StudentInfoViewModel> Students { get; set; }
    }
}

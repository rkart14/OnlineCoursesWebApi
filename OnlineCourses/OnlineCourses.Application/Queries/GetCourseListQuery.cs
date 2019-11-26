using MediatR;
using OnlineCourses.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.Queries
{
    public class GetCourseListQuery : IRequest<IReadOnlyList<CourseStatisticsViewModel>>
    {
    }
}

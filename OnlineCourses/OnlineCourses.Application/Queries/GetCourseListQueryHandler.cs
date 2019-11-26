using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Application.ViewModels;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Shared.OptionSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.Application.Queries
{
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, IReadOnlyList<CourseStatisticsViewModel>>
    {
        private readonly IOptions<DatabaseStringsConfigurationManager> _options;
        public GetCourseListQueryHandler(IOptions<DatabaseStringsConfigurationManager> options)
        {
            _options = options;
        }
        public async Task<IReadOnlyList<CourseStatisticsViewModel>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            string query = "SELECT Id AS 'CourseId'," +
                              "		MinimumStudentAge AS 'MinimumAgeOfStudents'," +
                              "		MaximumStudentAge AS 'MaximumAgeOfStudents'," +
                              "		AverageStudentAge AS 'AverageAgeOfStudents'," +
                              "		MaximumStudentsCount AS 'TotalCapacity'," +
                              "		EnrolledStudentsCount AS 'CurrentNumberOfStudents'" +
                              "FROM dbo.Courses";
            using (IDbConnection db = new SqlConnection(_options.Value.ConnectionString))
            {
                return (await db.QueryAsync<CourseStatisticsViewModel>(query))
                           .ToList()
                           .AsReadOnly();
            }
        }
    }
}

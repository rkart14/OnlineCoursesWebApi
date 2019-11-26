using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using OnlineCourses.Application.ViewModels;
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
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDetailedInfoViewModel>
    {
        private readonly IOptions<DatabaseStringsConfigurationManager> _options;
        public GetCourseQueryHandler(IOptions<DatabaseStringsConfigurationManager> options)
        {
            _options = options;
        }
        async Task<CourseDetailedInfoViewModel> IRequestHandler<GetCourseQuery, CourseDetailedInfoViewModel>.Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            string query = "";
            using (IDbConnection db = new SqlConnection(_options.Value.ConnectionString))
            {
                return (await db.QueryAsync<CourseDetailedInfoViewModel>(query + "WHERE Id = @Id", new { request.CourseId })).SingleOrDefault();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Application.Commands;

namespace OnlineCourses.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public StudentController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("{id}/students")]
        public async Task<IActionResult> Create(Guid id, [FromBody]EnrollStudentToCourseCommand command)
        {
            await _mediatr.Send(new EnrollStudentToCourseCommand
            {
                CourseId = id,
                StudentAge = command.StudentAge,
                StudentName = command.StudentName,
                StudentEmail = command.StudentEmail
            }); 
            return Ok();
        }
    }
}
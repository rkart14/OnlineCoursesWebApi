using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Application.Commands;
using OnlineCourses.Application.Queries;

namespace OnlineCourses.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public CourseController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCourseCommand command)
        {
            await _mediatr.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, GetCourseQuery query)
        {
            var result = await _mediatr.Send(new GetCourseQuery
            {
                CourseId = id
            });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetCourseListQuery query)
        {
            var result = await _mediatr.Send(query);
            return Ok(result);
        }
    }
}
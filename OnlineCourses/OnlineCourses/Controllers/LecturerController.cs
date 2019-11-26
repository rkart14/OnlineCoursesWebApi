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
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public LecturerController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateLecturerCommand command)
        {
            await _mediatr.Publish(command);
            return Ok();
        }
    }
}
using MediatR;
using OnlineCourses.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.Commands
{
    public class CreateLecturerCommand : IRequest<CreateLecturerResultViewModel>
    {
        public string Name { get; set; }
    }
}

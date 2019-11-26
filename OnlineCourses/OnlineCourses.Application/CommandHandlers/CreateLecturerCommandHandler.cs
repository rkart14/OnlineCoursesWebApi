using MediatR;
using OnlineCourses.Application.Commands;
using OnlineCourses.Application.ViewModels;
using OnlineCourses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.Application.CommandHandlers
{
    public class CreateLecturerCommandHandler : IRequestHandler<CreateLecturerCommand, CreateLecturerResultViewModel>
    {
        Task<CreateLecturerResultViewModel> IRequestHandler<CreateLecturerCommand, CreateLecturerResultViewModel>.Handle(CreateLecturerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

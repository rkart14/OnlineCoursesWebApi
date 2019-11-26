using MediatR;
using MessageBus.MassTransit;
using OnlineCourses.Application.Commands;
using OnlineCourses.Application.ProcessorEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.Application.CommandHandlers
{
    public class EnrollStudentToCurseCommandHandler : IRequestHandler<EnrollStudentToCourseCommand>
    {
        //Concurency issue solved with rowversioning on Course entity
        public async Task<Unit> Handle(EnrollStudentToCourseCommand request, CancellationToken cancellationToken)
        {
            var bus = BusConfigurator.ConfigureBus();

            var enrollStudentUri = new Uri(RabbitMqConstants.RabbitMqUri + RabbitMqConstants.EnrollStudentServiceQueue);
            var endPoint = await bus.GetSendEndpoint(enrollStudentUri);

            await endPoint.Send(new EnrollStudentToCourseProcessingCommand
            {
                CourseId = request.CourseId,
                StudentEmail = request.StudentEmail,
                StudentAge = request.StudentAge,
                StudentName = request.StudentName
            });
            return await Task.FromResult(Unit.Value);
        }
    }
}

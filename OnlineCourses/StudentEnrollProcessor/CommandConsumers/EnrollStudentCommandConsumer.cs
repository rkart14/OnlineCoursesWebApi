using MassTransit;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Application.ProcessorEvents;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Shared.ApplicationExceptions;
using StudentEnrollProcessor.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollProcessor.CommandConsumers
{
    public class EnrollStudentCommandConsumer : IConsumer<EnrollStudentToCourseProcessingCommand>
    {
        private readonly IRepository<Course> _courseRepository;
        public EnrollStudentCommandConsumer(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public EnrollStudentCommandConsumer()
        {

        }
        public async Task Consume(ConsumeContext<EnrollStudentToCourseProcessingCommand> context)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(context.Message.CourseId);
                if (course == null)
                {
                    throw new NotFoundException($"Couldn't find course with id {context.Message.CourseId}");
                }
                var student = new Student(context.Message.StudentName, context.Message.StudentEmail, context.Message.StudentAge);
                course.EnrollStudent(student);
                await _courseRepository.UnitOfWork.SaveChangesAsync();
                await context.Publish(new StudentEnrolledResult
                {
                    EnrollSucceed = true,
                    StudentEmail = context.Message.StudentEmail
                });
            }
            catch (Exception)
            { 
                var studentEnrolledResult = new StudentEnrolledResult
                {
                    EnrollSucceed = false,
                    StudentEmail = context.Message.StudentEmail
                };
                //publish event
                await context.Publish(studentEnrolledResult);
            }
        }
    }
}

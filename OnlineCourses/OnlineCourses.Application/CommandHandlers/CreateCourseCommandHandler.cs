using MediatR;
using OnlineCourses.Application.Commands;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Application.ViewModels;
using OnlineCourses.Domain.Entities;
using OnlineCourses.Shared.ApplicationExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineCourses.Application.CommandHandlers
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseResultViewModel>
    {
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly IRepository<Course> _courseRepository;
        public CreateCourseCommandHandler(IRepository<Course> courseRepository, IRepository<Lecturer> lecturerRepository)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(IRepository<Course>));
            _lecturerRepository = lecturerRepository ?? throw new ArgumentNullException(nameof(IRepository<Lecturer>));
        }
        async Task<CreateCourseResultViewModel> IRequestHandler<CreateCourseCommand, CreateCourseResultViewModel>.Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var lecturer = await _lecturerRepository.GetByIdAsync(request.LecturerId);
            if (lecturer == null)
            {
                throw new NotFoundException($"Couldn't find lecturer with id {request.LecturerId}");
            }
            var course = new Course(request.CourseName, request.CourseMaximumStudentsCount, lecturer.Name, request.LecturerId);
            var insertedCourse = await _courseRepository.AddAsync(course);
            await _courseRepository.UnitOfWork.SaveChangesAsync();
            return new CreateCourseResultViewModel
            {
                CourseId = insertedCourse.Id
            };
        }
    }
}

using OnlineCourses.Domain.DomainEvents;
using OnlineCourses.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineCourses.Shared.ApplicationExceptions;

namespace OnlineCourses.Domain.Entities
{
    public class Course : EntityBase<Course, Guid>, IAggregatRoot
    {
        public Course(string name, int maximumStudentsCount, string lecturerName, Guid lecturerId)
        {
            if (maximumStudentsCount <= 0)
            {
                throw new ConflictOccuredException("Total capacity of course should be more than 0!");
            }
            Name = name;
            MaximumStudentsCount = maximumStudentsCount;
            EnrolledStudentsCount = 0;
            MinimumStudentAge = 0;
            MaximumStudentAge = 0;
            AverageStudentAge = 0;
            IsAvailableToEnroll = true;
            EnrolledStudents = new List<Student >();
            Id = Guid.NewGuid();
            LecturerName = lecturerName;
            LecturerId = lecturerId;
        }

        public string Name { get; private set; }

        public int MaximumStudentsCount { get; private set; }

        public int EnrolledStudentsCount { get; private set; }

        public int MinimumStudentAge { get; private set; }

        public int MaximumStudentAge { get; private set; }

        public int AverageStudentAge { get; private set; }

        public bool IsAvailableToEnroll { get; private set; }

        public  string LecturerName { get; private set; }

        public Guid LecturerId { get; private set; }
        public List<Student> EnrolledStudents { get; private set; }


        public void EnrollStudent(Student student)
        {
            if (!IsAvailableToEnroll)
                throw new ConflictOccuredException($"Course {Name} reached it's limit of enrolled students");

            AverageStudentAge = ((EnrolledStudentsCount * AverageStudentAge) + student.Age) / (EnrolledStudentsCount + 1);
            EnrolledStudentsCount++;
            EnrolledStudents.Add(student);

            if (EnrolledStudentsCount == MaximumStudentsCount)
            {
                IsAvailableToEnroll = false;
            }

            if (student.Age > MaximumStudentAge)
            {
                MaximumStudentAge = student.Age;
                AddDomainEvent(new CourseMaximumStudentAgeIncreasedDomainEvent // notify client for being old :)) 
                {
                    StudentId = student.Id,
                    CourseId = Id
                });
            }

            if (student.Age < MinimumStudentAge || MinimumStudentAge == 0)
            {
                MinimumStudentAge = student.Age;
            }

        }
    }
}

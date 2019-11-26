using OnlineCourses.Domain.SeedWork;
using OnlineCourses.Shared;
using OnlineCourses.Shared.ApplicationExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Domain.Entities
{
    public class Student : EntityBase<Student,Guid>
    {
        public Student(string name, string email, int age)
        {
            if (age < Consts.MinimalStudentAge)
            {
                throw new ConflictOccuredException($"Student Age can't be less than {Consts.MinimalStudentAge}");
            }
            Name = name;
            Email = email;
            Age = age;
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public int Age { get; private set; }

        public Course Course { get; private set; }

    }
}

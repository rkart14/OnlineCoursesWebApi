using OnlineCourses.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Domain.Entities
{
    public class Lecturer : EntityBase<Lecturer, Guid>
    {
        public Lecturer(string name)
        {
            Name = name;

        }
        public string Name { get; private set; }

        public string GetName()
        {
            return Name;
        }
    }
}

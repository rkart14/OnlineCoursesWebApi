using OnlineCourses.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Application.Interfaces
{
    public interface IDomainEventContext
    {
        IEnumerable<Event> GetDomainEvents();
    }
}

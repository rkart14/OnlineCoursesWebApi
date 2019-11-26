using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Domain.SeedWork
{
    public interface IAggregatRoot
    {
        IReadOnlyCollection<Event> DomainEvents { get; }
    }
}

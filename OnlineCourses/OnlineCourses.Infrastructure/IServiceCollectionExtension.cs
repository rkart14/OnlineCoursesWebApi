using Microsoft.Extensions.DependencyInjection;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Domain.Entities;
using OnlineCourses.EntityFrameworkCore.Data;
using OnlineCourses.EntityFrameworkCore.Repositories;
using System;

namespace OnlineCourses.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDomainEventContext>(sp => sp.GetRequiredService<OnlineCoursesContext>());
            serviceCollection.AddRepositories();
            return serviceCollection;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<Course>, SqlRepository<Course, Guid>>();
            serviceCollection.AddTransient<IRepository<Lecturer>, SqlRepository<Lecturer, Guid>>();
            return serviceCollection;
        }
    }
}

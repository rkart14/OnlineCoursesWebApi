using MassTransit;
using MessageBus.MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCourses.Application.Interfaces;
using OnlineCourses.Application.ProcessorEvents;
using OnlineCourses.Domain.Entities;
using OnlineCourses.EntityFrameworkCore.Data;
using OnlineCourses.EntityFrameworkCore.Repositories;
using StudentEnrollProcessor.CommandConsumers;
using System;
using System.IO;

namespace StudentEnrollProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Enrollment service";

            var builder = new ConfigurationBuilder();
               

            IConfigurationRoot configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("Default");
            var serviceProvider = new ServiceCollection()
                 .AddDbContext<OnlineCoursesContext>(options =>
                 {
                     options.UseSqlServer(connectionString);
                 })
              .AddTransient<IDomainEventContext, OnlineCoursesContext>()
              .AddTransient<IRepository<Course>, SqlRepository<Course, Guid>>()
              .BuildServiceProvider();

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.EnrollStudentServiceQueue, e =>
                {
                    e.Consumer<EnrollStudentCommandConsumer>();

                });
            });

            bus.Start();

            Console.WriteLine("Listening for commands.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}

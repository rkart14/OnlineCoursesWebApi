using MassTransit;
using MessageBus.MassTransit;
using StudentEnrollProcessor.CommandConsumers;
using System;

namespace NotificationProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Notification Service";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.NotificationServiceQueue, e =>
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

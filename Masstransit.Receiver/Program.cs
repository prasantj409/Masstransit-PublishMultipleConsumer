//using MassTransit;
using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Masstransit.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "This is the customer registration command receiver.";
            Console.WriteLine("CUSTOMER REGISTRATION COMMAND RECEIVER.");
            RunMassTransitReceiver();
        }

        private static void RunMassTransitReceiver()
        {
            IBusControl rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
            {
                rabbit.Host(new Uri("rabbitmq://localhost:5672"), settings =>
                {
                    settings.Password("guest");
                    settings.Username("guest");
                });

              rabbit.ReceiveEndpoint("mycompany.domains.queues", conf =>
                {
                    conf.Consumer<RegisterCustomerConsumer>();
                });
            });

            rabbitBusControl.Start();
            Console.ReadKey();

            rabbitBusControl.Stop();
        }
    }
}

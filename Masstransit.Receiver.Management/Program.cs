using MassTransit;
using System;

namespace Masstransit.Receiver.Management
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Management consumer";
            Console.WriteLine("MANAGEMENT");
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

                rabbit.ReceiveEndpoint("mycompany.domains.queues.events.mgmt", conf =>
                {
                    conf.Consumer<CustomerRegisteredConsumerMgmt>();
                });
            });
            rabbitBusControl.Start();
            Console.ReadKey();
            rabbitBusControl.Stop();
        }
    }
}

using MassTransit;
using System;

namespace Masstransit.Receiver.Notification
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Notification consumer";
            Console.WriteLine("NOTIFICATION");
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

                rabbit.ReceiveEndpoint("mycompany.domains.queues.events.notification", conf =>
                {
                    conf.Consumer<CustomerRegisteredConsumerNotification>();
                });
            });

            rabbitBusControl.Start();
            Console.ReadKey();

            rabbitBusControl.Stop();
        }
    }
}

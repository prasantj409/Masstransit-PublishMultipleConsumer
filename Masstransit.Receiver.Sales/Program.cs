﻿using MassTransit;
using System;

namespace Masstransit.Receiver.Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sales consumer";
            Console.WriteLine("SALES");
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

                rabbit.ReceiveEndpoint("mycompany.domains.queues.events.sales", conf =>
                {
                    conf.Consumer<CustomerRegisteredConsumerSls>();
                });
            });

            rabbitBusControl.Start();
            Console.ReadKey();

            rabbitBusControl.Stop();
        }
    }
}

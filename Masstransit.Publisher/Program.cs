using Masstransit.Message;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Masstransit.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("CUSTOMER REGISTRATION COMMAND PUBLISHER");
			Console.Title = "Publisher window";
			RunMassTransitPublisher();
		}

		private static void RunMassTransitPublisher()
		{
			string rabbitMqAddress = "rabbitmq://localhost:5672";
			string rabbitMqQueue = "mycompany.domains.queues";
			Uri rabbitMqRootUri = new Uri(rabbitMqAddress);

			IBusControl rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
			{
				rabbit.Host(rabbitMqRootUri, settings =>
				{
					settings.Password("guest");
					settings.Username("guest");
				});
			});

			Task<ISendEndpoint> sendEndpointTask = rabbitBusControl.GetSendEndpoint(new Uri(string.Concat(rabbitMqAddress, "/", rabbitMqQueue)));
			ISendEndpoint sendEndpoint = sendEndpointTask.Result;

			Task sendTask = sendEndpoint.Send<IRegisterCustomer>(new
			{
				Address = "New Street",
				Id = Guid.NewGuid(),				
				RegisteredUtc = DateTime.UtcNow,
				Name = "Nice people LTD"            				
			}, c =>
			{
				c.FaultAddress = new Uri("rabbitmq://localhost:5672/accounting/mycompany.queues.errors.newcustomers");
			});

			Console.ReadKey();
		}
	}
}

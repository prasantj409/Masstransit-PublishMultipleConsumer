using Masstransit.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.Receiver.Notification
{
    class CustomerRegisteredConsumerNotification : IConsumer<ICustomerRegistered>
    {
        public Task Consume(ConsumeContext<ICustomerRegistered> context)
        {
            ICustomerRegistered newCustomer = context.Message;
            Console.WriteLine("New customer is registerd. Send notification");
            Console.WriteLine(newCustomer.Address);
            Console.WriteLine(newCustomer.Name);
            Console.WriteLine(newCustomer.Id);
            return Task.FromResult(context.Message);
        }
    }
}

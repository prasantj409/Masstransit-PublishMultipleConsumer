using Masstransit.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.Receiver.Management
{
    public class CustomerRegisteredConsumerMgmt : IConsumer<ICustomerRegistered>
    {
        public Task Consume(ConsumeContext<ICustomerRegistered> context)
        {
            ICustomerRegistered newCustomer = context.Message;
            Console.WriteLine("A new customer has been registered, congratulations from Management to all parties involved!");
            Console.WriteLine(newCustomer.Address);
            Console.WriteLine(newCustomer.Name);
            Console.WriteLine(newCustomer.Id);
            return Task.FromResult(context.Message);
        }
    }
}

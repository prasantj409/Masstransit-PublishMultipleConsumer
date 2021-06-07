using Masstransit.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.Receiver
{
    public class RegisterCustomerConsumer : IConsumer<IRegisterCustomer>
    {
        public Task Consume(ConsumeContext<IRegisterCustomer> context)
        {
            IRegisterCustomer newCustomer = context.Message;
            Console.WriteLine("A new customer has signed up, it's time to register it in the command receiver. Details: ");
            Console.WriteLine(newCustomer.Address);
            Console.WriteLine(newCustomer.Name);
            Console.WriteLine(newCustomer.Id);
            

            context.Publish<ICustomerRegistered>(new
            {
                Address = newCustomer.Address,
                Id = newCustomer.Id,
                RegisteredUtc = newCustomer.RegisteredUtc,
                Name = newCustomer.Name
            });

            return Task.FromResult(context.Message);
        }
    }
}

<H1>Messaging through a service bus in .NET using MassTransit</h1></br>
<h2>publishing messages to multiple consumers</h2>
<p> This is a demo application around MassTransit. We are sending a message from a publisher to consumers using the MassTransit/RabbitMq client library. First we create a command to register customer. Our goal is to publish an event as soon as the command has been taken care of. One way of achieving it is publishing the event from the RegisterCustomerConsumer object. The IBusControl interface has a Publish method that could do the job. Here’s an example of publishing an IRegisterCustomer</p>

```
public class RegisterCustomerConsumer : IConsumer<IRegisterCustomer>
    {       
        public Task Consume(ConsumeContext<IRegisterCustomer> context)
        {           
            IRegisterCustomer newCustomer = context.Message;
            Console.WriteLine("A new customer has signed up, it's time to register it in the command receiver. Details: ");
            Console.WriteLine(newCustomer.Address);
            Console.WriteLine(newCustomer.Name);
            Console.WriteLine(newCustomer.Id);
            Console.WriteLine(newCustomer.Preferred);
 
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
  ```

<h6>We have following console application :</h6>
<ul>
  <li>Masstransit.Publisher</li>
  <li>Masstransit.Receiver</li>
  <li>Masstransit.Receiver.Management</li>
  <li>Masstransit.Receiver.Notification</li>
  <li>Masstransit.Receiver.Sales</li>  
</ul>

<p> Publisher console application publish a command to register a customer. Receiver application handle customer register command. As soon as the command has been taken care of, it publish customer registered event. It notify customer registerd event to  Receiver.Management, Receiver.Notification and Receiver.Sales applications.

using System;
using System.Collections.Generic;
using System.Text;

namespace Masstransit.Message
{
    public interface IRegisterCustomer
    {
        Guid Id { get; }
        DateTime RegisteredUtc { get; }
        string Name { get; }
        string Address { get; }
    }
}

using System;

namespace Exu.RouteService.Exceptions
{
    public class AddressNotFoundException 
        : ApplicationException
    {
        public AddressNotFoundException(string message) : base(message)
        { }
    }
}
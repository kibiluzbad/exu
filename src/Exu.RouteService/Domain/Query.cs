using System.Collections.Generic;

namespace Exu.RouteService.Domain
{
    public class Query
    {
        public IList<Address> Addresses { get; set; }
        public RouteType Type { get; set; }
    }
}
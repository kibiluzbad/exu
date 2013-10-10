using System.Collections.Generic;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;

namespace Exu.RouteService.Queries
{
    public interface IAddressQuery : IQuery<IEnumerable<Coordinate>>
    {
        IList<Address> Addresses { get; set; }
    }
}
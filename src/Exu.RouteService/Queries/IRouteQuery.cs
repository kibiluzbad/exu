using System.Collections.Generic;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;

namespace Exu.RouteService.Queries
{
    public interface IRouteQuery : IQuery<Route>
    {
        IEnumerable<Coordinate> Coordinates { get; set; }
    }
}
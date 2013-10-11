using System;
using System.Collections.Generic;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;
using Exu.RouteService.MaplinkRoute;

namespace Exu.RouteService.Queries
{
    public interface IRouteQuery : IQuery<Route>
    {
        IEnumerable<Coordinate> Coordinates { get; set; }
    }

    public class MaplinkRouteQuery : WcfBasicHttpQueryBase<Route, MaplinkRoute.RouteSoap>
            , IRouteQuery
    {
        protected override Func<RouteSoap, Route> GetExecuteMethod
        {
            get { throw new NotImplementedException(); }
        }

        protected override string ConfigurationName
        {
            get { return "RouteSoap"; }
        }

        public IEnumerable<Coordinate> Coordinates { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;
using Exu.RouteService.Maplink;

namespace Exu.RouteService.Queries
{
    public interface IRouteQuery : IQuery<Route>
    {
        IEnumerable<Coordinate> Coordinates { get; set; }
    }

    public class MaplinkRouteQuery : WcfBasicHttpQueryBase<IEnumerable<Coordinate>, object>
            , IRouteQuery
    {
        protected override Func<object, IEnumerable<Coordinate>> GetExecuteMethod
        {
            get { throw new NotImplementedException(); }
        }

        protected override string ConfigurationName
        {
            get { throw new NotImplementedException(); }
        }

        public Route Execute()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coordinate> Coordinates { get; set; }
    }
}
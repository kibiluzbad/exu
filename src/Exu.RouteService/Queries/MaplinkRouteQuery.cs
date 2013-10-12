using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;
using Exu.RouteService.MaplinkRoute;

namespace Exu.RouteService.Queries
{
    public class MaplinkRouteQuery : WcfBasicHttpQueryBase<Route, MaplinkRoute.RouteSoap>
        , IRouteQuery
    {
        private readonly string _token;

        public IEnumerable<Coordinate> Coordinates { get; set; }
        public RouteType RouteType { get; set; }

        public MaplinkRouteQuery(string token)
        {
            _token = token;
        }

        protected override Func<RouteSoap, Route> GetExecuteMethod
        {
            get
            {
                return
                    client =>
                        AutoMapper.Mapper.Map<RouteTotals,Route>(
                            client.getRoute(
                                new getRouteRequest(
                                    new getRouteRequestBody(GetRouteStop().ToArray(),
                                        GetRouteOptions(),
                                        _token))).Body.getRouteResult.routeTotals);
            }
        }

        private RouteOptions GetRouteOptions()
        {
            return new RouteOptions
                       {
                           routeDetails = new RouteDetails
                                              {
                                                  routeType = AutoMapper.Mapper.Map<RouteType, int>(RouteType)
                                              },
                           vehicle = new Vehicle{averageConsumption = 10, fuelPrice = 2.67, tankCapacity = 50}

                       };
        }

        private IEnumerable<RouteStop> GetRouteStop()
        {
            return Coordinates
                .Select(AutoMapper.Mapper.Map<Coordinate, RouteStop>);
        }

        protected override string ConfigurationName
        {
            get { return "RouteSoap"; }
        }
    }
}
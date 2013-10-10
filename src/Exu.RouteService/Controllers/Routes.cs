using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Responses;
using Nancy.ModelBinding;

namespace Exu.RouteService.Controllers
{
    public class Routes : NancyModule
    {
        public Routes()
        {
            Get["/"] = _ =>
            {
                var query = this.Bind<Query>();

                if (NoQueryOrAdderessesFound(query))
                    return HttpStatusCode.BadRequest;

                var routes = new List<Route>();
                if (RouteType.LessTraffic == query.Type)
                {
                    routes.Add(new Route
                    {
                        Time = new TimeSpan(0, 40, 0),
                        Distance = 2,
                        FuelCost = 2.00,
                        TotalCost = 2.00
                    });
                }
                else if (RouteType.FastestRoute == query.Type)
                {
                    routes.Add(new Route
                    {
                        Time = new TimeSpan(0, 30, 0),
                        Distance = 1,
                        FuelCost = 1.00,
                        TotalCost = 1.00
                    });
                }

                return Response.AsJson(routes);
            };
        }

        private static bool NoQueryOrAdderessesFound(Query query)
        {
            return null == query || null == query.Addresses || !query.Addresses.Any();
        }
    }

    public class Query
    {
        public IList<Address> Addresses { get; set; }
        public RouteType Type { get; set; }
    }

    public class Address
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public enum RouteType
    {
        None = 0,
        FastestRoute,
        LessTraffic
    }


    public class Route
    {
        public TimeSpan Time { get; set; }
        public int Distance { get; set; }
        public double FuelCost { get; set; }
        public double TotalCost { get; set; }
    }
}

using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using Exu.RouteService.Domain;
using Exu.RouteService.Queries;
using Nancy;
using Nancy.Responses;
using Nancy.ModelBinding;


namespace Exu.RouteService.Controllers
{
    public class Routes : NancyModule
    {
        private readonly IAddressQuery _addressFinder;
        private readonly IRouteQuery _routefinder;

        public Routes(IAddressQuery addressFinder, IRouteQuery routefinder)
        {
            _addressFinder = addressFinder;
            _routefinder = routefinder;

            Get["/"] = _ =>
            {
                var query = this.Bind<Query>();

                return NoQueryOrAdderessesFound(query) 
                    ? HttpStatusCode.BadRequest 
                    : Response.AsJson(GetRoute(query));
            };
        }

        private Route GetRoute(Query query)
        {
            _addressFinder.Addresses = query.Addresses;
            _routefinder.RouteType = query.Type;
            _routefinder.Coordinates = _addressFinder.Execute();
            return _routefinder.Execute();
        }

        private static bool NoQueryOrAdderessesFound(Query query)
        {
            return null == query || null == query.Addresses || !query.Addresses.Any();
        }
    }
}

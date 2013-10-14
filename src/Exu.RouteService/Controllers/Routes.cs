using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using Exu.RouteService.Domain;
using Exu.RouteService.Exceptions;
using Exu.RouteService.Queries;
using NLog;
using Nancy;
using Nancy.ErrorHandling;
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

            Post["/"] = _ =>
            {
                var query = this.Bind<Query>();

                try
                {
                    return NoQueryOrAdderessesFound(query)
                               ? HttpStatusCode.BadRequest
                               : Response.AsJson(GetRoute(query));
                }
                catch(ApplicationException exception)
                {
                    return Response.AsJson(exception.Message, HttpStatusCode.InternalServerError);
                }
            };

            Get["/"] = _ => "OK";
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

    public class NLogErrorHandler : IErrorHandler
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            object errorObject;
            context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out errorObject);

            if (null == errorObject) return;
            var error = (errorObject as Exception).InnerException;

            _logger.Fatal(error.Message, error);
        }
    }
}

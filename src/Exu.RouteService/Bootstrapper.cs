using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Nancy.Bootstrappers.Autofac;

namespace Exu.RouteService
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            //TODO: Add dependencies here
            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}
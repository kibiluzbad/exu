using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using AutoMapper;
using Exu.RouteService.Profile;
using Exu.RouteService.Queries;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;

namespace Exu.RouteService
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            Mapper.AddProfile(new AddressToMaplinkAddress());
            Mapper.AddProfile(new MaplinkAddressLocationToCoordinate());
            Mapper.AddProfile(new RouteTypeToInt());
            Mapper.AddProfile(new CoordinateToMaplinkRouteStop());
            Mapper.AddProfile(new MaplinkRouteTotalsToRoute());

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyModules(assembly);

            builder.RegisterType<MaplinkRouteQuery>()
                .WithParameter("token", ConfigurationManager.AppSettings["token"])
                .As<IRouteQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MaplinkAddressQuery>()
                .WithParameter("token", ConfigurationManager.AppSettings["token"])
                .As<IAddressQuery>()
                .InstancePerLifetimeScope();

            builder.Update(container.ComponentRegistry);

            base.ConfigureRequestContainer(container, context);
        }
    }
}
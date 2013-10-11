using System;
using Autofac;
using Exu.RouteService.Controllers;
using Exu.RouteService.Queries;
using Moq;
using Nancy;

namespace Exu.RouteService.Tests
{
    public class FakeBoostrapper : Bootstrapper
    {
        public FakeBoostrapper()
        {
            AddressQuery = () => new Mock<IAddressQuery>().Object;
            RouteQuery = () => new Mock<IRouteQuery>().Object;
        }
        
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            var builder = new ContainerBuilder();
            var assembly = typeof(Routes).Assembly;

            builder.RegisterAssemblyModules(assembly);

            builder.Register(c=> RouteQuery())
                .As<IRouteQuery>()
                .InstancePerLifetimeScope();

            builder.Register(c => AddressQuery())
                .As<IAddressQuery>()
                .InstancePerLifetimeScope();

            builder.Update(container.ComponentRegistry);
        }

        public Func<IAddressQuery> AddressQuery
        {
            get; set;
        }

        public Func<IRouteQuery> RouteQuery
        {
            get; set;
        }
    }
}
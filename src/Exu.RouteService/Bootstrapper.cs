using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using AutoMapper;
using Exu.RouteService.Domain;
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

    public class MaplinkAddressLocationToCoordinate : Profile
    {
        protected override void Configure()
        {
            CreateMap<Maplink.AddressLocation, Coordinate>()
                .ForMember(c => c.X,expression => expression.MapFrom(c => c.point.x))
                .ForMember(c => c.Y,expression => expression.MapFrom(c => c.point.y));
        }
    }

    public class AddressToMaplinkAddress : Profile
    {
        protected override void Configure()
        {
            CreateMap<Address, Maplink.Address>()
                .ForMember(c => c.city, expression => expression.MapFrom(c => new Maplink.City{ name = c.City, 
                    state = c.State}))
                .ForMember(c => c.street, expression => expression.MapFrom(c => c.Name))
                .ForMember(c => c.houseNumber, expression => expression.MapFrom(c => c.Number))
                .ForMember(c => c.district, expression => expression.Ignore())
                .ForMember(c => c.ExtensionData, expression => expression.Ignore())
                .ForMember(c => c.zip, expression => expression.Ignore());

        }
    }
}
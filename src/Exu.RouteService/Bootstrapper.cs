using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using AutoMapper;
using Exu.RouteService.Domain;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;

namespace Exu.RouteService
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            AutoMapper.Mapper.AddProfile(new AddressToMaplinkAddress());
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            //TODO: Add dependencies here
            base.ConfigureApplicationContainer(existingContainer);
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
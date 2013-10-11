using Exu.RouteService.Domain;

namespace Exu.RouteService.Profile
{
    public class AddressToMaplinkAddress : AutoMapper.Profile
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
using Exu.RouteService.Domain;

namespace Exu.RouteService.Profile
{
    public class MaplinkAddressLocationToCoordinate : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<Maplink.AddressLocation, Coordinate>()
                .ForMember(c => c.X,expression => expression.MapFrom(c => c.point.x))
                .ForMember(c => c.Y,expression => expression.MapFrom(c => c.point.y));
        }
    }
}
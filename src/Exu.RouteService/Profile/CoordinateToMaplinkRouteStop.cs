using Exu.RouteService.Domain;
using Exu.RouteService.MaplinkRoute;

namespace Exu.RouteService.Profile
{
    public class CoordinateToMaplinkRouteStop : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<Coordinate, RouteStop>()
                .ForMember(c => c.point, expression => expression.MapFrom(c => new Point {x = c.X, y = c.Y}))
                .ForMember(c=>c.description, expression => expression.Ignore())
                .ForMember(c=>c.ExtensionData, expression => expression.Ignore());

        }
    }
}
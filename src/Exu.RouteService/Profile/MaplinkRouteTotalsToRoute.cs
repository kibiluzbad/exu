using System.Xml;
using Exu.RouteService.Domain;
using Exu.RouteService.MaplinkRoute;

namespace Exu.RouteService.Profile
{
    public class MaplinkRouteTotalsToRoute : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<RouteTotals, Route>()
                .ForMember(c => c.Distance, expression => expression.MapFrom(c => c.totalDistance))
                .ForMember(c => c.FuelCost, expression => expression.MapFrom(c => c.totalfuelCost))
                .ForMember(c => c.TotalCost, expression => expression.MapFrom(c => c.totalCost))
                .ForMember(c => c.Time, expression => expression.MapFrom(c => XmlConvert.ToTimeSpan(c.totalTime)));
        }
    }
}
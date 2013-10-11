using Exu.RouteService.Domain;

namespace Exu.RouteService.Profile
{
    public class RouteTypeToInt : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<RouteType,int>().ConvertUsing(c=>{
                                                           switch (c)
                                                           {
                                                               case RouteType.FastestRoute:
                                                                   return 0;
                                                               case RouteType.LessTraffic:
                                                                   return 23;
                                                               default:
                                                                   return 1;//Rota mais curta
                                                           }
            });
        }
    }
}
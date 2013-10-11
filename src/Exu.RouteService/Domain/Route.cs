using System;

namespace Exu.RouteService.Domain
{
    public class Route
    {
        public TimeSpan Time { get; set; }
        public double Distance { get; set; }
        public double FuelCost { get; set; }
        public double TotalCost { get; set; }
    }
}
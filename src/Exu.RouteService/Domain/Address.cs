namespace Exu.RouteService.Domain
{
    public class Address
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public override string ToString()
        {
            return string.Format("{0} , {1} - {2} - {3}", Name, Number, City, State);
        }
    }
}
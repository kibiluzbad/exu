using System;
using System.Collections.Generic;
using System.Linq;
using Exu.RouteService.Domain;
using Exu.RouteService.Infra.Query;

namespace Exu.RouteService.Queries
{
    public class MaplinkAddressQuery
        : WcfBasicHttpQueryBase<IEnumerable<Coordinate>, Maplink.AddressFinderSoap>
            , IAddressQuery
    {
        private readonly string _token;

        public MaplinkAddressQuery(string token)
        {
            _token = token;
        }

        public IList<Address> Addresses { get; set; }


        protected override Func<Maplink.AddressFinderSoap, IEnumerable<Coordinate>> GetExecuteMethod
        {
            get
            {
                return client =>
                    AutoMapper.Mapper.Map<IEnumerable<Maplink.AddressLocation>, IEnumerable<Coordinate>>(
                        Addresses.SelectMany(
                            address =>
                                client.findAddress(new Maplink.findAddressRequest(new Maplink.findAddressRequestBody(AutoMapper.Mapper.Map<Address, Maplink.Address>(address),
                                    new Maplink.AddressOptions
                                    {
                                        resultRange = new Maplink.ResultRange
                                        {
                                            pageIndex = 1,
                                            recordsPerPage = 1000
                                        },
                                        searchType = 0
                                    }, _token))).Body.findAddressResult.addressLocation)
                            .ToList());
            }
        }

        protected override string ConfigurationName
        {
            get { return "AddressFinderSoap"; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exu.RouteService.Domain;
using Exu.RouteService.Exceptions;
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
                    Mapper.Map<IEnumerable<Maplink.AddressLocation>, IEnumerable<Coordinate>>(
                        Addresses.SelectMany(
                            address =>
                                {
                                    var result = client.findAddress(
                                        new Maplink.findAddressRequest(
                                            new Maplink.findAddressRequestBody(
                                                Mapper.Map<Address, Maplink.Address>(address),
                                                GetAddressOptions(),
                                                _token)));
                                    if (!result.Body.findAddressResult.addressLocation.Any())
                                        throw new AddressNotFoundException(
                                            string.Format("Não foi possível encontrar o endereço {0}.", address));
                                    return result.Body.findAddressResult.addressLocation;
                                })
                            .ToList());
            }
        }

        private static Maplink.AddressOptions GetAddressOptions()
        {
            return new Maplink.AddressOptions
            {
                resultRange = new Maplink.ResultRange
                {
                    pageIndex = 1,
                    recordsPerPage = 1000
                },
                searchType = 0
            };
        }

        protected override string ConfigurationName
        {
            get { return "AddressFinderSoap"; }
        }
    }
}
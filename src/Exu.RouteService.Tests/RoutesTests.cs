﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery.ExtensionMethods;
using Exu.RouteService.Controllers;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Exu.RouteService.Tests
{
    [Trait("Cálculos de valores totais da rota.", "")]
    public class RoutesTests
    {
        [Fact(DisplayName = "Ao chamar a raiz devo retornar HTTP Status OK (200)")]
        public void ChamadaARaiz()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var query = new Query { Addresses = new List<Address> { new Address(), new Address() }, Type = RouteType.FastestRoute };
            
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
                with.JsonBody(query);
            });
            
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact(DisplayName = "Dada uma lista de endreços devo retornar a menor distancia econtranda")]
        public void MenorDistancia()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var query = new Query { Addresses = new List<Address> { new Address(), new Address() }, Type = RouteType.FastestRoute };
            var routes = "[{\"Time\":{\"Days\":0,\"Hours\":0,\"Minutes\":30,\"Seconds\":0,\"Milliseconds\":0},\"Distance\":1,\"FuelCost\":1,\"TotalCost\":1}]";

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
                with.JsonBody(query);
            });

            var output = result.Body.AsString();

            Assert.Equal(routes, output);
        }

        [Fact(DisplayName = "Dada uma lista de endreços devo retornar a rota com menos trânsito")]
        public void MenosTransito()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            
            var query = new Query {Addresses = new List<Address>{new Address(), new Address()}, Type = RouteType.LessTraffic};
            var routes = "[{\"Time\":{\"Days\":0,\"Hours\":0,\"Minutes\":40,\"Seconds\":0,\"Milliseconds\":0},\"Distance\":2,\"FuelCost\":2,\"TotalCost\":2}]";

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
                with.JsonBody(query);
            });

            var output = result.Body.AsString();

            Assert.Equal(routes, output);
        }

        [Fact(DisplayName = "Se nenhum endereco for informado um erro 400 deve ser retornado")]
        public void NenhumEnderecoInformado()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var query = new Query { Addresses = new List<Address>(), Type = RouteType.LessTraffic };
           
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
                with.JsonBody(query);
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(DisplayName = "Se nenhuma entrada for informada um erro 400 deve ser retornado")]
        public void NenhumaEntrada()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using WireMock.Logging;
using WireMock.Net.StandAlone;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using WireMock.Types;

namespace SFA.DAS.EmployerDemand.MockServer
{
    public static class MockApiServer
    {
        public static IWireMockServer Start()
        {
            var settings = new WireMockServerSettings
            {
                Port = 5021,
                Logger = new WireMockConsoleLogger()
            };

            var server = StandAloneApp.Start(settings);
            
            server.Given(Request.Create().WithPath(arg => Regex.IsMatch(arg, @"/demand/create"))
                .UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyFromFile("create-demand.json"));
            
            server.Given(Request.Create().WithPath(arg => Regex.IsMatch(arg, @"/demand/create"))
                    .WithParam(MatchLocationParam)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("create-demand-location.json"));
            
            server.Given(Request.Create().WithPath(arg => Regex.IsMatch(arg, @"/locations"))
                .UsingGet()).RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyFromFile("locations.json"));

            return server;
        }
        
        private static bool MatchLocationParam(IDictionary<string, WireMockList<string>> arg)
        {
            return arg.ContainsKey("location") && arg["location"].Count !=0 && arg["location"][0].Equals($"{HttpUtility.UrlEncode("Camden, Camden")}", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
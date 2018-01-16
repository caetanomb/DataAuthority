using DataAuthority.Base64Result.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System.Threading;

namespace DataAuthority.IntegrationTest
{
    public class ResultEndPoint
    {
        private readonly TestServer _server;
        private readonly HttpClient _httpClient;

        public ResultEndPoint()
        {
            var webHost = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                });

            _server = new TestServer(webHost);
            _httpClient = _server.CreateClient();
        }        

        [Fact]
        public async void Get_DiifContent_response_OK_status_Code()
        {
            int id = 10;

            //Get diff Payload
            HttpRequestMessage requestResult = new HttpRequestMessage(HttpMethod.Get, $"v1/diff/{id}");
            var responseGet = await _httpClient.SendAsync(requestResult);

            string responseContent = await responseGet.Content.ReadAsStringAsync();            

            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.True(!string.IsNullOrEmpty(responseContent));
        }
    }
}

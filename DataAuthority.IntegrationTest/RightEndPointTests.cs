using DataAuthority.Base64Right.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataAuthority.IntegrationTest
{
    public class RightEndPointTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _httpClient;

        public RightEndPointTests()
        {
            var webHost = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");                    
                });

            _server = new TestServer(webHost);
            _httpClient = _server.CreateClient();
        }

        [Fact]
        public async Task Post_Content_response_Created_status_Code()
        {
            int id = 2;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/diff/{id}/Right");
            request.Content =
                new StringContent(JsonConvert.SerializeObject(new
                {
                    id = 1,
                    name = "Test",
                    address = "Avenue 1"
                }), UTF8Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_Content_response_NotFound_status_Code()
        {
            int id = 2;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"diff/{id}/Right");
            request.Content =
                new StringContent(JsonConvert.SerializeObject(new { }), UTF8Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

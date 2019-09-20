using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp;
using Xunit;

namespace XUnitTests
{
    public class PatientControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public PatientControllerTest(WebApplicationFactory<Startup> factory)
        {
            //System.Diagnostics.Debugger.Launch();
            _factory = factory;
            var appFactory = new WebApplicationFactory<Startup>().CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Patient/12")]
        //[InlineData("/Admin/Rooms/Index")]
        public async Task ResponseTest(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}

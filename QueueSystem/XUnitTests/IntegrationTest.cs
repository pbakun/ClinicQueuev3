using Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp;

namespace XUnitTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestCliet;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => 
                {
                    
                });
            TestCliet = appFactory.CreateClient();
        }

    }
}

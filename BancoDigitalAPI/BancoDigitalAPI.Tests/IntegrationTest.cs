using BancoDigitalAPI.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BancoDigitalAPI.Tests
{
    public class IntegrationTest
    {
        protected readonly HttpClient testClient;

        protected IntegrationTest()
        {
            //var appFactory = new WebApplicationFactory<Startup>()
            //    .WithWebHostBuilder(builder =>
            //    {
            //        builder.ConfigureServices(services =>
            //        {
            //            //services.RemoveAll(typeof(BancoDigitalDataContext));

            //            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BancoDigitalDataContext>));

            //            if (descriptor != null)
            //            {
            //                services.Remove(descriptor);
            //            }

            //            services.AddDbContext<BancoDigitalDataContext>(o => o.UseInMemoryDatabase("Database"));
            //        });
            //    });
        }
    }
}

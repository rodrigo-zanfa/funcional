using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoDigitalAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using BancoDigitalAPI.GraphQL;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types.Pagination;
using BancoDigitalAPI.Models;

namespace BancoDigitalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string mySqlConnection = Configuration.GetConnectionString("DefaultConnection");

            // adicionando o DataContext
            //services.AddDbContext<BancoDigitalDataContext>(o => o.UseInMemoryDatabase("Database"));
            services.AddDbContextPool<BancoDigitalDataContext>(o => o.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
            services.AddScoped<BancoDigitalDataContext, BancoDigitalDataContext>();

            services.AddScoped<Query>();
            services.AddScoped<Mutation>();

            //services.AddGraphQL(p => SchemaBuilder.New().AddServices(p)
            //    .AddType<CorrentistaType>()
            //    .AddQueryType<Query>()
            //    .AddMutationType<Mutation>()
            //    .Create());
            services.AddGraphQLServer()
                .AddType<CorrentistaType>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();

            services.AddErrorFilter<GraphQLErrorFilter>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BancoDigitalAPI", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/graphql",
                    Path = "/playground"
                });

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BancoDigitalAPI v1"));
            }

            app.UseGraphQL("/graphql");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //private static void InitializeDatabase(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetRequiredService<BancoDigitalDataContext>();

        //        if (context.Database.EnsureCreated())
        //        {
        //            var correntista = new Correntista { Cpf = "32516143800", Nome = "Rodrigo Zanferrari" };

        //            context.ContasCorrentes.Add(new ContaCorrente
        //            {
        //                Numero = 1,
        //                Saldo = 0,
        //                Correntista = correntista
        //            });

        //            context.ContasCorrentes.Add(new ContaCorrente
        //            {
        //                Numero = 2,
        //                Saldo = 0,
        //                Correntista = correntista
        //            });

        //            context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}

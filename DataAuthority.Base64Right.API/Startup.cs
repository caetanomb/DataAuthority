using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAuthority.ApplicationService.CommandHandlers;
using DataAuthority.ApplicationService.DomainEventHandlers;
using DataAuthority.DataInfrastructure.DataBaseInterface;
using DataAuthority.DataInfrastructure.Repositories;
using DataAuthority.Domain.Event;
using DataAuthority.Domain.Repository;
using DataAuthority.SqlServerEF;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAuthority.Base64Right.API
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
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

            //Configure Entity Context
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DataAuthorityContext>(opt => {
                    opt.UseSqlServer(Configuration.GetConnectionString("DataAuthorityContext")); 
                    }, 
                    ServiceLifetime.Scoped);

            //Configure MediatR
            services.AddMediatR(typeof(CreatePayLoadCommandHandler).Assembly, 
                                typeof(PayLoadCreatedDomainEventHandler).Assembly);

            //Register Interfaces and concrete classes               
            //Repository
            services.AddTransient<IDataAuthorityRepository, DataAuthorityRepository>();
            services.AddTransient<IDataBase, SqlServerDataBase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var database = serviceScope.ServiceProvider.GetRequiredService<DataAuthorityContext>().Database;
                    database.Migrate();
                }
            }

            app.UseMvc();
        }
    }
}

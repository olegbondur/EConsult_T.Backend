using System.IO;
using AutoMapper;
using EConsult_T.Api.Extensions;
using EConsult_T.Api.Services;
using EConsult_T.DAL.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EConsult_T.Api
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

            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfile()));
            services.AddJwtAthorization(Configuration.GetSection("AuthOptions"));
            services.AddSwaggerDocumentation();
            services.AddEFUnitOfWork(Configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<EConsultDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("EConsult_T.Api")
            ));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwaggerDocumentation();
            app.UseMvc();

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);

            });
        }
    }
}

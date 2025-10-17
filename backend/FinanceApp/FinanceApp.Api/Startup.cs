using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Mappings;
using FinanceApp.Application.Services;
using FinanceApp.Application.Validators;
using FinanceApp.Common.Middleware;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.Interfaces.Repositories;
using FinanceApp.Infrastructure.Data;
using FinanceApp.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace FinanceApp.Api
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
            //This code reads the connection string from the appsetting and in this way when
            //the project where the data access is located is compiled or in general the dbContext can connect
            //to the database and use the connection
            services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer
            (
               Configuration.GetConnectionString("FinanceDb")
            ));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder => builder.WithOrigins("http://localhost:5173")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddControllers()
                     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateBankValidator>());

            // AutoMapper
            services.AddAutoMapper(typeof(FinanceProfile));

            // Application services
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDepositCertificateService, DepositCertificateService>();
            services.AddScoped<ICardService, CardService>();

            // Repositories
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IDepositCertificateRepository, DepositCertificateRepository>();
            services.AddScoped<ICardRepository, CardRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinanceApp.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceApp.Api v1"));
            }

            var supportedCultures = new[] { new CultureInfo("en-US") }; 

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");
            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Diagnostics.CodeAnalysis;

using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Repositories;
using HealthPairDomain.Interfaces;
using HealthPairAPI.Helpers;

[assembly: ApiController]
namespace HealthPairAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string CorsPolicyName = "AllowConfiguredOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            // switch between database providers using runtime configuration
            // (the existing migrations are SQL-Server-specific, but the model itself is not)

            // this should be the name of a connection string.
            string whichDb = Configuration["DatabaseConnection"];
            if (whichDb is null)
            {
                throw new InvalidOperationException($"No value found for \"DatabaseConnection\"; unable to connect to a database.");
            }

            string connection = Configuration.GetConnectionString(whichDb);
            if (connection is null)
            {
                throw new InvalidOperationException($"No value found for \"{whichDb}\" connection; unable to connect to a database.");
            }

            if (whichDb.Contains("PostgreSql", StringComparison.InvariantCultureIgnoreCase))
            {
                //services.AddDbContext<HealthPairContext>(options => options.UseNpgsql(connection),ServiceLifetime.Transient);
                services.AddDbContext<HealthPairContext>(options => options.UseNpgsql(connection));
            }
            else
            {
                services.AddDbContext<HealthPairContext>(options => options.UseSqlServer(connection));
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthPair API", Version = "v1" });
            });



            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                    builder.WithOrigins("https://healthpair-client.azurewebsites.net",
                            "http://localhost:4200",
                            "http://a846dd4e8e4e14701bf08588e007d22b-636555553.us-east-2.elb.amazonaws.com",
                            "https://healthpair.1010.tecnaturelabs.com",
                            "http://healthpair.1010.tecnaturelabs.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                        //.AllowCredentials());
            });



            services.AddControllers(options =>
            {
                // remove the default text/plain string formatter to clean up the OpenAPI document
                options.OutputFormatters.RemoveType<StringOutputFormatter>();

                options.ReturnHttpNotAcceptable = true;
                options.SuppressAsyncSuffixInActionNames = false;
            });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var UserService = context.HttpContext.RequestServices.GetRequiredService<IPatientRepository>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = UserService.GetPatientByIdAsync(userId).Result;
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }

                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IInsuranceRepository, InsuranceRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            services.AddScoped<IProvInsurRepository, ProvInsurRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Configuration.GetValue("UseHttpsRedirection", defaultValue: true) is true)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(CorsPolicyName);


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthPair API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

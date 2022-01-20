using CarMarket.BusinessLogic.User.Service;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using CarMarket.Data;
using CarMarket.Data.User.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CarMarket.Data.Car.Repository;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using CarMarket.Server.Infrastructure;
using CarMarket.BusinessLogic.Car.Service;
using CarMarket.Data.Configuration.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Identity;
using CarMarket.Core.User.Domain;

namespace CarMarket.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.Authority = "https://localhost:5001";
                     options.Audience = "API";
                     options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                 });

            services.AddAuthorization();

            services.AddIdentity<UserModel, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/identification/login";
                    options.UserInteraction.LogoutUrl = "/identification/logout";
                })
                .AddProfileService<IdentityServerProfileService>()
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetScopes())
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarMarket", Version = "v1" });
            });

            services.AddBlazoredLocalStorage();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("CarMarketDb"));
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EntityToModelMappingProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarMarket v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}

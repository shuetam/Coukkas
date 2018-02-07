using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Coukkas.Infrastructure.Services;
using AutoMapper;
using Coukkas.Infrastructure.Mappers;
using Coukkas.Core;
using Coukkas.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Coukkas.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Coukkas.Infrastructure.EntityFramework;
using Coukkas.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Timers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;



// "ConnectionString": "Server=MATEUSZ-PC; User Id=Mateusz1;Password=mateusz1;Database=dydaktyka",
// "ConnectionString": "Server=localhost; User Id=sa;Password=P@$$w0rd;Database=CoukkasDatabase",

namespace Coukkas.Api
{
    public class Startup
    {
        Timer timer;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }
        

        public IConfiguration Configuration { get; }
        public IContainer Container {get; private set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .AddJsonOptions(j => j.SerializerSettings.Formatting = Formatting.Indented);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IFenceRepository, FenceRepository>();
            services.AddScoped<IFenceService, FenceService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddCors();
            
        
            services.AddSingleton(Configuration.GetSection("Jwt").Get<TokenParameters>());
            services.AddAuthorization();
           
            var connectionString = Configuration.GetSection("SqlConnecting").Get<SqlConnectingSettings>().ConnectionString; 
         
  
            services.AddDbContext<CoukkasContext>(options => options.UseSqlServer(connectionString));
            

            var tokenParameters = Configuration.GetSection("Jwt").Get<TokenParameters>();
        

            services.AddAuthentication(options =>
             {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }). AddJwtBearer(options =>
        
             {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
            {
                  
                    ValidIssuer = tokenParameters.Issuer,
                    ValidateAudience = false,
                
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.SigningKey))  
            };
             });

                var databaseConnect = new DataBaseConnect();
                timer = new Timer();
                timer.Interval = TimeSpan.FromMinutes(0.2).TotalMilliseconds;      
                timer.Elapsed +=  (sender, e) =>
                databaseConnect.ConnectAndChangeCouponsLocations(); 
                timer.Start(); 


             var builder = new ContainerBuilder();
             builder.Populate(services); // get existing services

             builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope(); // register types
             builder.RegisterInstance(Configuration.GetSettings<SqlConnectingSettings>());  // using my extension for configuration
            
            
             
             Container = builder.Build();
             return new AutofacServiceProvider(Container);
        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:5000").AllowAnyMethod()); // allow all methods on my api port
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            appLifeTime.ApplicationStopped.Register(() => Container.Dispose()); // cleaning container when app stop


        }
    }
}

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


namespace Coukkas.Api
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
            services.AddMvc();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IFenceRepository, FenceRepository>();
            services.AddScoped<IFenceService, FenceService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
          //  services. Configure<TokenParameters>(Configuration.GetSection("Jwt"));
            services.AddSingleton(Configuration.GetSection("Jwt").Get<TokenParameters>());
            services.AddAuthorization();
            

         var tokenParameters = Configuration.GetSection("Jwt").Get<TokenParameters>();
        // var tokenParameters = new TokenParameters();

            services.AddAuthentication(options =>
             {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }). AddJwtBearer(options =>
        
             {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
            {
                  //  ValidIssuer = tokenParameters.Value.Issuer,
                    ValidIssuer = tokenParameters.Issuer,
                    ValidateAudience = false,
                   // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.Value.SigningKey))
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.SigningKey))  
            };
             });
        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

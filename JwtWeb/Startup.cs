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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;

namespace JwtWeb
{
    public class Startup
    {
        public static  string Getkey
        {
            get
            {
                byte[] byte32 = new byte[32];
                RandomNumberGenerator.Create().GetBytes(byte32);

                string keystring = Base64UrlEncoder.Encode(byte32);
                
                return "TKgZKL3vfWoM1TDAt48LMgrpIP0qKxXSZ_TIt8F3ShM";
            }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var secretKey = Base64UrlEncoder.DecodeBytes(Startup.Getkey);
            //var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(secretKey);
        TokenValidationParameters tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricKey,
                ValidIssuer = "gh_iss",
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidAudience = "http://audience.com",
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
            };
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParams;
            });
            services.AddMvc();
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

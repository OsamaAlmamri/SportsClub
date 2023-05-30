using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreLib.JWTServices.Authenticators;
using CoreLib.JWTServices.RefreshTokenRepositories;
using CoreLib.JWTServices.TokenGenerators;
using CoreLib.JWTServices.TokenValidators;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using SportsClub.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CoreLib.Pagination.Services;
using System.Text;

namespace SportsClub
{
    public class Startup
    {
        // Order to run
        //1) Constructor
        //2) Configure services
        //3) Configure
        private IWebHostEnvironment HostingEnvironment { get; }
        public static IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            HostingEnvironment = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


       

            services.AddDbContext<SportsClubContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("SqlCon2022"));
            });

            AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
            _configuration.Bind("Authentication", authenticationConfiguration);

            //SecretClient keyVaultClient = new SecretClient(
            //    new Uri(_configuration.GetValue<string>("KeyVaultUri")),
            //    new DefaultAzureCredential());
            //authenticationConfiguration.AccessTokenSecret = keyVaultClient.GetSecret("access-token-secret").Value.Value;

            services.AddSingleton(authenticationConfiguration);



            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, DatabaseRefreshTokenRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }


        public void Configure(IApplicationBuilder app)
        {


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                   name: "default",
                                pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");


            });

           

      

        }
    }
}

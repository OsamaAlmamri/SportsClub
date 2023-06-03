using Microsoft.EntityFrameworkCore;
using SportsClub.Models;
using SportsClub.DataTransferObjects;
using AutoMapper;
using SportsClub.Models.Repositores;
using SportsClub.Core.Requests;
using SportsClub.Core.Pagination.Services;
using SportsClub.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SportsClub.Core.Payments;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddControllersWithViews();
builder.Services.AddCors();
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
  //  mc.AddProfile<UserDto>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddScoped<IRepositoryBase<UserSubscription>, UserSubscriptionRepostry>();
builder.Services.AddScoped<IRepositoryBase<PaymentGatway>, PaymentGatwayRepostry>();
builder.Services.AddScoped<IRepositoryBase<UserSubscriptionService>, UserSubscriptionServicesRepostry>();
builder.Services.AddScoped<IRepositoryBase<ServiceType>, ServiceTypeRepostry>();
builder.Services.AddScoped<IRepositoryBase<ServiceTime>, ServiceTimeRepostry>();
builder.Services.AddScoped<IRepositoryBase<Service>, ServiceRepostry>();
builder.Services.AddScoped<IRepositoryBase<UserDetail>, UserDetailRepostry>();
builder.Services.AddScoped<IRepositoryBase<User>, UserRepostry>();
builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();


builder.Services.AddDbContext<SportsClubContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon2022"));
});



builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//SecretClient keyVaultClient = new SecretClient(
//    new Uri(_configuration.GetValue<string>("KeyVaultUri")),
//    new DefaultAzureCredential());
//authenticationConfiguration.AccessTokenSecret = keyVaultClient.GetSecret("access-token-secret").Value.Value;

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
//_configuration.Bind("Authentication", authenticationConfiguration);
var assembly = typeof(Program).Assembly; ;
/*
builder.Services.AddMvc().AddControllersAsServices();*/

builder.Services
    .AddControllers()
    .AddApplicationPart(assembly)
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(typeof(ServiceRequest).Assembly);
        config.RegisterValidatorsFromAssembly(typeof(UserSubscriptionRequest).Assembly);
    })
    .AddControllersAsServices();

// paypal client configuration
/*builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseCors(MyAllowSpecificOrigins);

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(x => x.MapControllers());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

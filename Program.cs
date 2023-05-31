using Microsoft.EntityFrameworkCore;
using SportsClub.Models;

using SportsClub.Core.JWTServices.Authenticators;
using SportsClub.Core.JWTServices.RefreshTokenRepositories;
using SportsClub.Core.JWTServices.TokenGenerators;
using SportsClub.Core.JWTServices.TokenValidators;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using SportsClub.DataTransferObjects;
using AutoMapper;
using SportsClub.Models.Repositores;
using SportsClub.Core.Pagination.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddControllersWithViews();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddDbContext<SportsClubContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon2022"));
});

builder.Services.AddScoped<IRepositoryBase<ServiceType>, ServiceTypeRepostry>();
builder.Services.AddScoped<IRepositoryBase<ServiceTime>, ServiceTimeRepostry>();
builder.Services.AddScoped<IRepositoryBase<Service>, ServiceRepostry>();




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



AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
var assembly = typeof(Program).Assembly; ;
/*
builder.Services.AddMvc().AddControllersAsServices();*/

builder.Services
    .AddControllers()
    .AddApplicationPart(assembly)
    .AddControllersAsServices();

builder.Services.AddSingleton(authenticationConfiguration);
builder.Services.AddSingleton<AccessTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenValidator>();
builder.Services.AddScoped<Authenticator>();
builder.Services.AddSingleton<TokenGenerator>();
builder.Services.AddScoped<IRefreshTokenRepository, DatabaseRefreshTokenRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
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

app.UseEndpoints(x => x.MapControllers());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

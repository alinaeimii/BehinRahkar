using ApiGateway.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Shared.Helper;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var rsa = RSA.Create();
               string key = RsaKey.GetPublicKey();
               rsa.FromXmlString(key);

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = true,
                   ValidateIssuer = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "BehinRahkar",
                   ValidAudience = "BehinRahkar",
                   IssuerSigningKey = new RsaSecurityKey(rsa)
               };
           });



var app = builder.Build();

app.UseMiddleware<RevokeMiddleware>();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();



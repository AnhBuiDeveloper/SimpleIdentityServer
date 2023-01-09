using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.Authority = "https://localhost:7240";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


// authentication policy
var authPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllers(configure =>
{
    configure.Filters.Add(new AuthorizeFilter(authPolicy));
});

//builder.Services.AddAuthorization(options =>
//  options.AddPolicy("all", policy =>
//    policy.RequireClaim("scope", "usersservice.all")
//    )
//  );

//builder.Services.AddAuthorization(options =>
//  options.AddPolicy("read", policy =>
//    policy.RequireClaim("scope", "usersservice.all", "usersservice.read")
//    )
//  );

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

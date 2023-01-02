using IdentityServerWeb;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder(args);

new IdentityServerManager().Configure(builder.Services);
var app = builder.Build();
app.UseIdentityServer();

app.Run();

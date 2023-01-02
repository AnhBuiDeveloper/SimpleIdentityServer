using IdentityServerConsole;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

new IdentityServerManager().Configure(builder.Services);

var server = builder.Build();
server.UseIdentityServer();

server.Run();
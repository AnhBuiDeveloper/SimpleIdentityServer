using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerWeb
{
    public class IdentityServerManager
    {
        public void Configure(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(GetClients())
                .AddInMemoryApiResources(GetApiResources())
                .AddInMemoryApiScopes(GetScopes())
                .AddTestUsers(GetUsers().ToList())
                .AddDeveloperSigningCredential();
        }

        private IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope { Name = "api1", Emphasize = true, },
            };
        }

        private IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };
        }

        private IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        private IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                }
            };
        }
    }
}

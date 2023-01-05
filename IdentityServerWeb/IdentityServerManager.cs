using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.OpenSsl;
using System.Security.Cryptography;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;

namespace IdentityServerWeb
{
    public class IdentityServerManager
    {
        public void Configure(IServiceCollection services)
        {
            //var fileStream = System.IO.File.OpenText("./RSAKeyPair/public.pem");
            //var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
            //var keyParameter = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)pemReader.ReadObject();

            //string pemString = "------BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAssktZhCxXFvBv9WqPnf8\r\nSpbmPXWLuJ0dg+GBL9h+sDXqH5q7PlUeXakMwX8tYxTydv+Wz4rMYzIIIzHmWn1Q\r\n7YO3QtyjAHLiuWZ+RmCcvdzhp3lvQ842nfqcC63VbU0tRBQU7BbOwHG5OV3UhIwA\r\nwCg3hiZnLt4LaG5Sa3uyqSFvA9Wg417XxFa1RqUhrQZjaRAEJA9196NjEQFUp+1h\r\n0RslzFkO7b9EYtBLfmarWoCH1ktrYNtN3O0ZysYqb0xFHTZ/BcZq1rmESzXnvk+T\r\n0qVhZIUffIJzGOMyD2rzXYMEw31yIKdNjwIkw747vJqjuWW9HBYvb6BjFeAgBq6H\r\nWwIDAQAB\r\n-----END PUBLIC KEY-----\r\n";
            //using var reader = new StringReader(pemString);
            //var pemReader = new PemReader(reader);
            //var keyParameter = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)pemReader.ReadObject();

            //var rsa = new RSACryptoServiceProvider();
            //rsa.ImportParameters(new RSAParameters
            //{
            //    Modulus = keyParameter.Modulus.ToByteArrayUnsigned(),
            //    Exponent = keyParameter.Exponent.ToByteArrayUnsigned()
            //});

            services.AddIdentityServer()
                .AddInMemoryClients(GetClients())
                .AddInMemoryApiResources(GetApiResources())
                .AddInMemoryApiScopes(GetScopes())
                .AddTestUsers(GetUsers().ToList())
                //.AddSigningCredential(new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256));
                .AddSigningCredential(CreateSigningCredential());
        }

        private SigningCredentials CreateSigningCredential()
        {
            var credentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.RsaSha256);

            return credentials;
        }
        private RSACryptoServiceProvider GetRSACryptoServiceProvider()
        {
            return new RSACryptoServiceProvider(2048);
        }
        private SecurityKey GetSecurityKey()
        {
            return new RsaSecurityKey(GetRSACryptoServiceProvider());
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

//using IdentityServer4.Models;
//using IdentityServer4.Test;
//using Microsoft.IdentityModel.Tokens;
//using Org.BouncyCastle.OpenSsl;
//using System.Security.Cryptography;
//using Org.BouncyCastle.Pkcs;

//namespace IdentityServerWeb
//{
//    public class IdentityServerManager
//    {
//        public void Configure(IServiceCollection services)
//        {
//            var fileStream = System.IO.File.OpenText("./RSAKeyPair/private.pem");
//            var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
//            var keyParameter = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)pemReader.ReadObject();

//            var rsa = new RSACryptoServiceProvider();
//            rsa.ImportParameters(new RSAParameters
//            {
//                Modulus = keyParameter.Modulus.ToByteArrayUnsigned(),
//                Exponent = keyParameter.Exponent.ToByteArrayUnsigned()
//                //Modulus = Convert.FromBase64String(ReadFile("./RSAKeyPair/public.pem")),
//                //Exponent = Convert.FromBase64String(ReadFile("./RSAKeyPair/private.pem"))
//            });

//            services.AddIdentityServer()
//                .AddInMemoryClients(GetClients())
//                .AddInMemoryApiResources(GetApiResources())
//                .AddInMemoryApiScopes(GetScopes())
//                .AddTestUsers(GetUsers().ToList())
//                .AddSigningCredential(new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256));

//            //string ReadFile(string filePath)
//            //{
//            //    var fileStream = System.IO.File.OpenText(filePath);
//            //    var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
//            //    var KeyParameter = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemReader.ReadObject();

//            //    //using (var reader = File.OpenText(filePath))
//            //    //{
//            //    //    var key = new PemReader(reader).ReadPemObject();
//            //    //    return key.Modulus.ToBase64String();
//            //    //}

//            //    return string.Empty;
//            //}
//        }

//        private IEnumerable<ApiScope> GetScopes()
//        {
//            return new List<ApiScope>
//            {
//                new ApiScope { Name = "api1", Emphasize = true, },
//            };
//        }

//        private IEnumerable<Client> GetClients()
//        {
//            return new List<Client>
//            {
//                new Client
//                {
//                    RequireConsent = false,
//                    ClientId = "client",
//                    AllowedGrantTypes = GrantTypes.ClientCredentials,
//                    ClientSecrets =
//                    {
//                        new Secret("secret".Sha256())
//                    },
//                    AllowedScopes = { "api1" },
//                    AllowAccessTokensViaBrowser = true,
//                    AccessTokenLifetime = 3600
//                }
//            };
//        }

//        private IEnumerable<ApiResource> GetApiResources()
//        {
//            return new List<ApiResource>
//            {
//                new ApiResource("api1", "My API")
//            };
//        }

//        private IEnumerable<TestUser> GetUsers()
//        {
//            return new List<TestUser>
//            {
//                new TestUser
//                {
//                    SubjectId = "1",
//                    Username = "alice",
//                    Password = "password"
//                }
//            };
//        }
//    }
//}

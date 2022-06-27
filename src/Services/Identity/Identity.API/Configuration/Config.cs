using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.Configuration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]{
                new ApiResource{
                    Name = "news_api",
                    DisplayName= "News API"
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>{
                new ApiScope("full_access")
                };
        }

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls)
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "news_web_app",
                    ClientName = "News Web App Client",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },
                    ClientUri = $"{clientUrls["NewsWebApp"]}", // public uri of the client
                    AllowedCorsOrigins = { clientUrls["NewsWebApp"] },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                        {
                            $"{clientUrls["NewsWebApp"]}/authentication/login-callback"
                        },
                    PostLogoutRedirectUris = new List<string>
                        {
                            $"{clientUrls["NewsWebApp"]}/authentication/logout-callback"
                        },
                    AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,
                            "full_access",
                        },
                    AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                    IdentityTokenLifetime = 60 * 60 * 2, // 2 hours
                    RequireClientSecret = true, // !Important for authorization

                },

                new Client
                {
                    ClientId = "news_web_admin",
                    ClientName = "News Web Admin Client",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },
                    ClientUri =  $"{clientUrls["NewsWebAdmin"]}", // public uri of the client
                    AllowedCorsOrigins = { clientUrls["NewsWebAdmin"] },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                        {
                            $"{clientUrls["NewsWebAdmin"]}/authentication/login-callback"
                        },
                    PostLogoutRedirectUris = new List<string>
                        {
                           $"{clientUrls["NewsWebAdmin"]}/authentication/logout-callback"
                        },
                    AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,
                            "full_access",
                        },
                    AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                    IdentityTokenLifetime = 60 * 60 * 2, // 2 hours
                    RequireClientSecret = false, // !Important for authorization
                },

                new Client
                {
                    ClientId = "news_api_swaggerui",
                    ClientName = "News API Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientUrls["NewsApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["NewsApi"]}/swagger/" },

                    AllowedScopes =
                        {
                            "full_access",
                        },
                }
        };
        }
    }
}

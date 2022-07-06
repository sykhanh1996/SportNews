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
                    Name = "sportnews_api",
                    DisplayName= "SportNews API"
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
                    ClientId = "sportnews_web_app",
                    ClientName = "SportNews Web App Client",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },
                    ClientUri = $"{clientUrls["SportNewsWebApp"]}", // public uri of the client
                    AllowedCorsOrigins = { clientUrls["SportNewsWebApp"] },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                        {
                            $"{clientUrls["SportNewsWebApp"]}/authentication/login-callback"
                        },
                    PostLogoutRedirectUris = new List<string>
                        {
                            $"{clientUrls["SportNewsWebApp"]}/authentication/logout-callback"
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
                    ClientId = "sportnews_web_admin",
                    ClientName = "SportNews Web Admin Client",
                    ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha256())
                        },
                    ClientUri =  $"{clientUrls["SportNewsWebAdmin"]}", // public uri of the client
                    AllowedCorsOrigins = { clientUrls["SportNewsWebAdmin"] },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                        {
                            $"{clientUrls["SportNewsWebAdmin"]}/auth/auth-callback"
                        },
                    PostLogoutRedirectUris = new List<string>
                        {
                           $"{clientUrls["SportNewsWebAdmin"]}"
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
                    ClientId = "sportnews_api_swaggerui",
                    ClientName = "SportNews API Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientUrls["SportNewsWebApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["SportNewsWebApi"]}/swagger/" },

                    AllowedScopes =
                        {
                            "full_access",
                        },
                }
        };
        }
    }
}

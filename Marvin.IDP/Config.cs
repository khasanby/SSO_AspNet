using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Marvin.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>();

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>();

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientName = "Image Gallery",
                ClientId = "image-gallery-client",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7184/signin-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                ClientSecrets ={ new Secret("gallery-secret".Sha256()) },
                RequireConsent = false,
                AllowedCorsOrigins = new List<string> { "https://localhost:7184" },
                AccessTokenLifetime = 86400,
                //FrontChannelLogoutUri = "https://localhost:7184/signout-oidc",
                BackChannelLogoutUri = "https://localhost:7184/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7184/signout-callback-oidc" },
            },
            new Client
            {
                ClientName = "Notes",
                ClientId = "notes-client",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7020/signin-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                ClientSecrets ={ new Secret("notes-secret".Sha256()) },
                RequireConsent = false,
                AllowedCorsOrigins = new List<string> { "https://localhost:7020" },
                AccessTokenLifetime = 86400,
                //FrontChannelLogoutUri = "https://localhost:7020/signout-oidc",
                BackChannelLogoutUri = "https://localhost:7020/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7020/signout-callback-oidc" },
            }
        ];
}
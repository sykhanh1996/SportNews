import { environment } from "@environments/environment";
import { AuthConfig } from "angular-oauth2-oidc";

export const authCodeFlowConfig: AuthConfig = {
    // Url of the Identity Provider
    issuer: environment.authorityUrl,

    // URL of the SPA to redirect the user to after login
    redirectUri: environment.adminUrl + "/auth/auth-callback",

    // The SPA's id. The SPA is registerd with this id at the auth-server
    // clientId: 'server.code',
    clientId: environment.clientId,

    // Just needed if your auth server demands a secret. In general, this
    // is a sign that the auth server is not configured with SPAs in mind
    // and it might not enforce further best practices vital for security
    // such applications.
    // dummyClientSecret: 'secret',

    responseType: 'code',

    // set the scope for the permissions the client should request
    // The first four are defined by OIDC.
    // Important: Request offline_access to get a refresh token
    // The api scope is a usecase specific one
    scope: "openid profile full_access",

    showDebugInformation: true,
    dummyClientSecret:"secret",
    logoutUrl:environment.adminUrl
 
  };
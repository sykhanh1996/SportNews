import { Injectable } from '@angular/core';
import { CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { UserManagerSettings } from 'oidc-client';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (localStorage.getItem('isLoggedin')) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
export function getClientSettings(): UserManagerSettings {
  return {
      authority: environment.authorityUrl,
      client_id: environment.clientId,
      redirect_uri: environment.adminUrl + "/auth-callback",
      post_logout_redirect_uri: environment.adminUrl,
      response_type: "code",
      scope: "openid profile offline_access full_access",
      client_secret: "secret",
      filterProtocolClaims: true,
      loadUserInfo: true
  };
}
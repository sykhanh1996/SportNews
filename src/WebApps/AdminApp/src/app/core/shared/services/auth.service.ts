import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { User, UserManager, UserManagerSettings, WebStorageStateStore } from "oidc-client";
import { BehaviorSubject } from "rxjs";
import { BaseService } from "./base.service";


@Injectable({
    providedIn: "root",
})
export class AuthService extends BaseService {
    // Observable navItem source
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    authNavStatus$ = this._authNavStatusSource.asObservable();

    private manager = new UserManager(getClientSettings());
    private user: User | null;

    constructor() {
        super();
        this.manager.getUser().then((user) => {
            this.user = user;
            return user;
        });
    }

    login() {
        return this.manager.signinRedirect();
    }

    async completeAuthentication() {
        await this.manager.signinRedirectCallback().then((user) => {
            // this.manager.storeUser(user);
            this.user = user;
        });
    }

    async isAuthenticated(): Promise<boolean> {
        return this.manager.getUser().then((user) => {
            if (user != null && !user.expired) {
                return true;
            }
            // need clear first and then remove to not auto generate new item in localStorage
            this.manager.clearStaleState();
            this.manager.removeUser();
            return false;
        }).catch((err) => {
            return false;
        });
    }

    get authorizationHeaderValue(): string {
        if (this.user) {
            return `${this.user.token_type} ${this.user.access_token}`;
        }
        return '';
    }

    get name(): string {
        if (this.user != null) {
            return this.user.profile.name ? this.user.profile.name : "";
        }
        return '';
    }

    get profile() {
        return this.user != null ? this.user.profile : null;
    }
    async signout() {
        await this.manager.signoutRedirect();
    }
}

export function getClientSettings(): UserManagerSettings {
    return {
        authority: environment.authorityUrl,
        client_id: environment.clientId,
        redirect_uri: environment.adminUrl + "/auth/auth-callback",
        post_logout_redirect_uri: environment.adminUrl,
        response_type: "code",
        scope: "openid profile full_access",
        filterProtocolClaims: true,
        client_secret: "secret",
        loadUserInfo: true,
        automaticSilentRenew: false,
        silent_redirect_uri: environment.adminUrl + "/dashboard",
        userStore: new WebStorageStateStore({ store: window.localStorage })
    };
}

import { makeAutoObservable } from 'mobx';
import { User, UserManager, Log, WebStorageStateStore } from 'oidc-client';

const config = {
    authority: 'http://www.alevelwebsite.com:5002',
    client_id: 'client_pkce',
    client_secret: "secret",
    redirect_uri: `http://www.alevelwebsite.com/callback`,
    response_type: 'code',
    scope: 'openid profile mvc',
    post_logout_redirect_uri: `http://www.alevelwebsite.com`,
    automaticSilentRenew: true,
    monitorSession: true,
    checkSessionInterval: 2000,
    silent_redirect_uri: `http://www.alevelwebsite.com`,
    filterProtocolClaims: true,
    revokeAccessTokenOnSignout: true,
    revokeRefreshTokenOnSignout: true,
    usePkce: true,
};

Log.level = Log.INFO;
Log.logger = console;

class AuthStore {
    user: User | null = null;
    oidc_client: UserManager;

    constructor() {
        this.oidc_client = new UserManager(config);
        makeAutoObservable(this);
        this.oidc_client.events.addUserLoaded((user) => {
            if (user) {
                this.user = user;
            }
        });
    };

    async login() {
        await this.oidc_client.signinRedirect();
        this.user = await this.oidc_client.getUser();
    };

    async logout() {
        this.oidc_client.signoutRedirect();
        this.user = null;
    };

    handleCallback() {
        this.oidc_client.signinRedirectCallback().then(response => {
            this.user = response;
        }).catch(error => {
            console.log(error);
        });
    }
};


export default AuthStore;
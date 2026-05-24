import { createContext, useContext, useEffect, useMemo, useState } from 'react';
import keycloak from './keycloak-config';

type KeycloakProfile = {
  username?: string;
  email?: string;
  firstName?: string;
};

type AuthContextValue = {
  initialized: boolean;
  authenticated: boolean;
  loading: boolean;
  username?: string;
  email?: string;
  login: () => Promise<void>;
  logout: () => Promise<void>;
  token?: string;
  tokenParsed?: Record<string, unknown>;
  keycloak: any;
};

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [initialized, setInitialized] = useState(false);
  const [authenticated, setAuthenticated] = useState(false);
  const [profile, setProfile] = useState<KeycloakProfile | undefined>();
  const [token, setToken] = useState<string | undefined>();

  useEffect(() => {
    if (keycloak.didInitialize)
        return;

    keycloak
      .init({
        onLoad: 'check-sso',
        pkceMethod: 'S256',
        checkLoginIframe: false,
      })
      .then(async (auth) => {
        setAuthenticated(auth);
        setToken(keycloak.token);

        if (auth) {
          try {
            const userProfile = await keycloak.loadUserProfile();
            setProfile(userProfile);
          } catch (error) {
            console.warn('Unable to load Keycloak profile', error);
          }
        }
      })
      .catch((error) => {
        console.error('Keycloak initialization failed', error);
      })
      .finally(() => {
        setInitialized(true);
      });
  }, []);

  useEffect(() => {
    if (!initialized || !authenticated) {
      return undefined;
    }

    const interval = window.setInterval(() => {
      keycloak
        .updateToken(30)
        .then((refreshed) => {
          if (refreshed) {
            setToken(keycloak.token);
          }
        })
        .catch((error) => {
          console.error('Failed to refresh Keycloak token', error);
        });
    }, 60_000);

    return () => {
      window.clearInterval(interval);
    };
  }, [initialized, authenticated]);

  const login = async () => keycloak.login();

  const logout = async () => keycloak.logout({ redirectUri: window.location.origin });

  const value = useMemo(
    () => ({
      initialized,
      authenticated,
      loading: !initialized,
      username: profile?.username ?? profile?.firstName,
      email: profile?.email,
      login,
      logout,
      token,
      tokenParsed: keycloak.tokenParsed,
      keycloak,
    }),
    [initialized, authenticated, profile, token],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error('useAuth must be used inside AuthProvider');
  }

  return context;
}

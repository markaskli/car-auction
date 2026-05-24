import { Link, Outlet, createRootRoute } from '@tanstack/react-router';
import { Button } from '@/components/ui/button';
import { useAuth } from '@/lib/auth';

export const Route = createRootRoute({
  component: RootLayout,
});

function RootLayout() {
  const auth = useAuth();

  return (
    <>
      <div className="p-2 flex flex-wrap gap-2 items-center">
        <Link to="/" className="[&.active]:font-bold">
          Home
        </Link>
        <Link to="/weather-forecast" className="[&.active]:font-bold">
          Weather Forecast
        </Link>
        <div className="ml-auto flex flex-wrap items-center gap-2">
          {auth.loading ? (
            <span>Checking authentication…</span>
          ) : auth.authenticated ? (
            <>
              <span>
                Signed in as {auth.username ?? auth.email ?? 'authenticated user'}
              </span>
              <Button onClick={auth.logout}>Logout</Button>
            </>
          ) : (
            <Button onClick={auth.login}>Login</Button>
          )}
        </div>
      </div>
      <hr />
      <Outlet />
    </>
  );
}

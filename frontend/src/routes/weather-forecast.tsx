import { createFileRoute } from '@tanstack/react-router';
import WeatherForecastList from '@/features/weatherforecast/components/weather-forecast-list';
import { useAuth } from '@/lib/auth';
import { Button } from '@/components/ui/button';

export const Route = createFileRoute('/weather-forecast')({
  component: RouteComponent,
});

function RouteComponent() {
  const auth = useAuth();

  if (auth.loading) {
    return <div className="p-4">Checking access…</div>;
  }

  if (!auth.authenticated) {
    return (
      <div className="p-4 space-y-4">
        <p className="text-base">
          This page is protected and requires authentication.
        </p>
        <Button onClick={auth.login}>Login with Keycloak</Button>
      </div>
    );
  }

  return <WeatherForecastList />;
}

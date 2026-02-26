import { createFileRoute } from '@tanstack/react-router';
import WeatherForecastList from '@/features/weatherforecast/components/weather-forecast-list';

export const Route = createFileRoute('/weather-forecast')({
  component: RouteComponent,
});

function RouteComponent() {
  return <WeatherForecastList />;
}

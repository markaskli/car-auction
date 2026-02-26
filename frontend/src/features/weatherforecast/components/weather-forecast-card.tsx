import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';
import type { WeatherForecast } from '../types';
import { Badge } from '@/components/ui/badge';

type Props = {
  forecast: WeatherForecast;
};

const WeatherForecastCard = ({ forecast }: Props) => {
  return (
    <Card className="transition-shadow hover:shadow-md">
      <CardHeader className="pb-2">
        <CardDescription>{forecast.date}</CardDescription>
        <CardTitle className="text-2xl">
          {forecast.temperatureC}°C{' '}
          <span className="text-muted-foreground text-base font-normal">
            / {forecast.temperatureF}°F
          </span>
        </CardTitle>
      </CardHeader>

      {forecast.summary && (
        <CardContent>
          <Badge variant="secondary">{forecast.summary}</Badge>
        </CardContent>
      )}
    </Card>
  );
};

export default WeatherForecastCard;

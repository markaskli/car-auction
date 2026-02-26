import { queryOptions } from '@tanstack/react-query';
import { getWeatherForecast } from './api/get-weather-forecast';

const QUERY_KEY = 'weather-forecast';

export const weatherForecastQueryOptions = () =>
  queryOptions({
    queryKey: [QUERY_KEY],
    queryFn: getWeatherForecast,
  });

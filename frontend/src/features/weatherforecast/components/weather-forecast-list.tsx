import { useQuery } from '@tanstack/react-query';
import WeatherForecastCard from './weather-forecast-card';
import { weatherForecastQueryOptions } from '../queries';

const WeatherForecastList = () => {
  const { data, isLoading, error } = useQuery(weatherForecastQueryOptions());

  if (isLoading) return <div>Loading forecast…</div>;
  if (error) return <div>Failed to load forecast</div>;

  console.log(data);

  return (
    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3">
      {data?.map((forecast) => (
        <WeatherForecastCard key={forecast.date} forecast={forecast} />
      ))}
    </div>
  );
};

export default WeatherForecastList;

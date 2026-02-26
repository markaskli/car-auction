import api from '@/lib/api-client';
import type { WeatherForecast } from '../types';

export const getWeatherForecast = async (): Promise<WeatherForecast[]> => {
  const response = await api.get('/weather-forecast');
  return response.data;
};

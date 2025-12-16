import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [weather, setWeather] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchWeather = async () => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(
        `${import.meta.env.VITE_API_URL ?? "api"}/weatherforecast`
      );

      if (!response.ok) {
        throw new Error("Failed to fetch data");
      }

      const data = await response.json();
      setWeather(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleClick = () => {
    setCount((c) => c + 1);
    fetchWeather();
  };

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={handleClick}>
          count is {count}
        </button>
        {loading && <p>Loading...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}

        <ul>
          {weather.map((item, index) => (
            <li key={index}>
              <strong>{item.date}</strong> —{" "}
              {item.temperatureC}°C / {item.temperatureF}°F —{" "}
              {item.summary}
            </li>
          ))}
        </ul>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App

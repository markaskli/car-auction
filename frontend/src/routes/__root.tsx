import { Link, Outlet, createRootRoute } from '@tanstack/react-router';

export const Route = createRootRoute({
  component: RootLayout,
});

function RootLayout() {
  return (
    <>
      <div className="p-2 flex gap-2">
        <Link to="/" className="[&.active]:font-bold">
          Home
        </Link>{' '}
        <Link to="/weather-forecast" className="[&.active]:font-bold">
          Weather Forecast
        </Link>{' '}
      </div>
      <hr />
      <Outlet />
    </>
  );
}

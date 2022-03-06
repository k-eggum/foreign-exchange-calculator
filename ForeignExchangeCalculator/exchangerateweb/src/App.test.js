import { render, screen } from '@testing-library/react';
import App from './App';

test('renders app with heading', () => {
  render(<App />);
  const titleElement = screen.getByText(/Calculate currency/i);
  expect(titleElement).toBeInTheDocument();
});

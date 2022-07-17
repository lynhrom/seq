import { render, screen } from '@testing-library/react';
import App from './index';

test('renders the landing page', () => {
  render(<App />);
});


test('should be renders all labels', () => {
  render(<App />);
  const element1 = screen.getByText(/Price source:/i)
  expect(element1).toBeInTheDocument();

  const element2 = screen.getByText(/Ticker:/i)
  expect(element2).toBeInTheDocument();
});

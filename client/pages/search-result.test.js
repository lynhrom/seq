import { getByTestId, render } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import App from './index.js';

test("should render search result component", () => {
  const { getByRole, getByTestId } = render(<App/>);

        const root = getByRole('root');
        
        const parent = getByTestId('filter-data-history');
        const child = getByTestId('search-result');

        expect(root).toContainElement(parent);
        expect(parent).toContainElement(child);
        
        expect(child).not.toContainElement(parent); // Pass
});
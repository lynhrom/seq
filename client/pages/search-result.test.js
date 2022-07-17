import { within } from '@testing-library/react';
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { getByTestId, render, screen,waitFor } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import App from './index';
import SearchResult from './search-result';

test("should render search result component", () => {
  const { getByRole, getByTestId } = render(<App/>);

        const root = getByRole('root');
        
        const parent = getByTestId('filter-data-history');
        const child = getByTestId('search-result');

        expect(within(parent).queryByTestId('search-result')).not.toBeNull();
});

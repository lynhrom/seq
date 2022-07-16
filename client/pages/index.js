import axios from "axios";
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { useState, useEffect } from "react";
import { SearchResult } from "./search-result";
import nextConfig from "./../config.json";

export default () => {
    const [query, setQuery] = useState({ sourceId: "1", tickerId: "1" });
    const [sources, setSources] = useState({});
    const [tickers, setTickers] = useState({});
    const [data, setData] = useState({});
    // Add: Our SignalR Hub
    const [hubCx, setHubCx] = useState(null);
    const API_URL = nextConfig.SERVER_URL;
    const PAGE_SIZE = nextConfig.PAGE_SIZE;
    
    const handleChange = (event) => {
        setQuery(values => ({
            ...values,
            [event.target.name]: event.target.value
        }))
    }

    const fetchTickers = async () => {
        const res = await axios.get(`${API_URL}/ticker`);
        setTickers(res.data);
    }

    const fetchSources = async () => {
        const res = await axios.get(`${API_URL}/source`);
        setSources(res.data);
    }

    const fetchItems = async () => {
        const res = await axios.get(`${API_URL}/market/${PAGE_SIZE}/0/${query.sourceId}/${query.tickerId}`);
        setData(res.data);
    }

    const setUpSignalRConnection = async (query) => {
        const connection = new HubConnectionBuilder()
            .withUrl(`${API_URL}/hubs/notifications`) // Ensure same as BE
            .withAutomaticReconnect()
            .build();

        connection.on('ReceiveData', (result) => {
            debugger
            if(result.tickerId == query.tickerId && result.sourceId == query.sourceId)
                setData(result);
        });

        try {
            await connection.start();
        } catch (err) {
            console.log(err);
        }

        if (connection.state === HubConnectionState.Connected) {
            connection.invoke('SendData', query.tickerId, query.sourceId).catch((err) => {
                console.error(err);
            });
        }

        return connection;
    };

    useEffect(() => {
        fetchTickers();
        fetchSources();
    }, []);

    useEffect(() => {
        setUpSignalRConnection(query);
    }, [query]);

    const renderedTickers = tickers != null && tickers.items != null && tickers.items.length > 0 && tickers.items.map(x => { return <option key={x.id} value={x.id}>{x.name}</option> });
    const renderedSources = sources != null && sources.items != null && sources.items.length > 0 && sources.items.map(x => { return <option key={x.id} value={x.id}>{x.name}</option> });

    return <form>
        <div className="m-5">
            <div className="row mb-3 mt-3">
                <label className="col-sm-7 col-form-label">Price source:</label>
                <div className="col-sm-5">
                    <select className="form-select" name="sourceId" value={query.sourceId} onChange={handleChange}>
                        {renderedSources}
                    </select>
                </div>
            </div>
            <div className="row mb-3">
                <label className="col-sm-7 col-form-label">Ticker:</label>
                <div className="col-sm-5">
                    <select className="form-select" name="tickerId" value={query.tickerId} onChange={handleChange}>
                        {renderedTickers}
                    </select>
                </div>
            </div>

            <SearchResult data={data} />
        </div>
    </form>
}
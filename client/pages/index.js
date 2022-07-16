import axios from "axios";
import { useState, useEffect } from "react";
import { SearchResult } from "./search-result";
import nextConfig from "./../config.json";

export default () => {
    const [query, setQuery] = useState({ source: "1", ticker: "1" });
    const [sources, setSources] = useState({});
    const [tickers, setTickers] = useState({});
    const [data, setData] = useState({});
    const API_URL = nextConfig.SERVER_URL;

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
        const res = await axios.get(`${API_URL}/market/5/0/${query.source}/${query.ticker}`);
        setData(res.data);
    }

    useEffect(() => {
        fetchTickers();
        fetchSources();
    }, []);

    useEffect(() => {
        fetchItems();
    }, [query]);

    const renderedTickers = tickers != null && tickers.items != null && tickers.items.length > 0 && tickers.items.map(x => { return <option key={x.id} value={x.id}>{x.name}</option> });
    const renderedSources = sources != null && sources.items != null && sources.items.length > 0 && sources.items.map(x => { return <option key={x.id} value={x.id}>{x.name}</option> });

    return <form>
        <div className="m-5">
            <div className="row mb-3 mt-3">
                <label className="col-sm-7 col-form-label">Price source:</label>
                <div className="col-sm-5">
                    <select className="form-select" name="source" value={query.source} onChange={handleChange}>
                        {renderedSources} 
                    </select>
                </div>
            </div>
            <div className="row mb-3">
                <label className="col-sm-7 col-form-label">Ticker:</label>
                <div className="col-sm-5">
                    <select className="form-select" name="ticker" value={query.ticker} onChange={handleChange}>
                        {renderedTickers}
                    </select>
                </div>
            </div>

            <SearchResult data ={data} />
        </div>
    </form>
}
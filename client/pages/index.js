import axios from "axios";
import * as moment from 'moment'
import { useState, useEffect } from "react"

export default () => {
    const [query, setQuery] = useState({ source: "1", ticker: "1" });
    const [sources, setSources] = useState({});
    const [tickers, setTickers] = useState({});
    const [data, setData] = useState({});

    const handleChange = (event) => {
        setQuery(values => ({
            ...values,
            [event.target.name]: event.target.value
        }))
    }

    const fetchTickers = async () => {
        const res = await axios.get(`https://localhost:5099/ticker`);
        setTickers(res.data);
    }

    const fetchSources = async () => {
        const res = await axios.get(`https://localhost:5099/source`);
        setSources(res.data);
    }

    const fetchItems = async () => {
        const res = await axios.get(`https://localhost:5099/market/5/0/${query.source}/${query.ticker}`);
        setData(res.data);
    }

    useEffect(() => {
        fetchTickers();
        fetchSources();
    }, []);

    useEffect(() => {
        fetchItems();
    }, [query]);

    const renderedItems = data != null && data.items != null && data.items.length > 0 ? (data.items.map(item => {
        return <tr key={item.date}><td>{moment(item.date).format('yyyy-MM-DD hh:mm:ss')}</td><td>{item.price}</td></tr>
    })) : (<tr><td colSpan={2} className="font-weight-light text-center">No data</td></tr>);

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

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th className="col-sm-5">Date</th>
                        <th className="col-sm-5">Price</th>
                    </tr>
                </thead>
                <tbody>
                    {renderedItems}
                </tbody>
            </table>
        </div>

    </form>
}
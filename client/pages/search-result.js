import * as moment from 'moment'
const SearchResult = ({data}) => {
    
    const renderedItems = data != null && data.items != null && data.items.length > 0 ? (data.items.map(item => {
        return <tr key={item.date}><td>{moment(item.date).format('yyyy-MM-DD hh:mm:ss')}</td><td>{item.price}</td></tr>
    })) : (<tr><td colSpan={2} className="font-weight-light text-center">No data</td></tr>);

    return <table className="table table-bordered">
        <thead>
            <tr>
                <th className="col-sm-5">Date</th>
                <th className="col-sm-5">Price</th>
            </tr>
        </thead>
        <tbody>
            {renderedItems}
        </tbody>
    </table>;
};

export {SearchResult};
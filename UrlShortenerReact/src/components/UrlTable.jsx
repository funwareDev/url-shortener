import { useState, useEffect, useContext } from 'react';
import axios from 'axios';
import getLocation from '../tools/getLocation';
import timeAgo from '../tools/timeAgo';
import "../styles/UrlTable.css"
import { AuthContext } from '../services/AuthProvider';
import { getLastUrls, performDeleteUrl } from '../services/ApiCallsService';
import { Link, useNavigate } from 'react-router-dom';
import API_ROUTES from '../config/apiRoutes';

const UrlTable = () => {
    const [urls, setUrls] = useState([]);
    const { username, isAdmin } = useContext(AuthContext)
    const navigate = useNavigate();

    const fetchData = () => {
        getLastUrls(15)
        .then(res => {
            setUrls(res.data.urls)
        })
        .catch(err => console.error('Error fetching data:', err))
    };

    useEffect(() => {
        fetchData();

        const interval = setInterval(() => {
            fetchData();
        }, 10000);

        return () => clearInterval(interval);
    }, []);

    const performDelete = (id) => {
        console.log('delete ' + id)

        performDeleteUrl(id)
            .then((res) => {
                console.log(res.data);
                fetchData();
            })
            .catch(error => console.log(error))
    }

    return (
        <div>
            <div className="table-container">
                <table>
                    <thead>
                        <tr>
                            <th>Short url</th>
                            <th>To site</th>
                        </tr>
                    </thead>
                    <tbody>
                        {urls.map((url) => (
                            <tr key={url.identificator}>
                                <td><a href={`${API_ROUTES.BaseUrl}/${url.identificator}`} target="_blank">{`${API_ROUTES.BaseUrl}/${url.identificator}`}</a></td>
                                <td>{getLocation(url.longUrl)?.hostname}</td>
                                <td><Link to={`/url/${url.identificator}`}>Details</Link></td>
                                {(username === url.createdBy || isAdmin) &&
                                    <td><button onClick={() => performDelete(url.identificator)}>Delete</button></td>}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default UrlTable;
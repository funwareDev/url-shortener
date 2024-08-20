import React, { useState, useEffect } from 'react';
import axios from 'axios';
import getLocation from '../tools/getLocation';
import timeAgo from '../tools/timeAgo';
import UrlData from '../models/UrlData';
import "../styles/UrlTable.css"

const UrlTable = () => {
    const [urls, setUrls] = useState([]);


    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.post('https://localhost:7201/Url', { count: 5 });
                setUrls(response.data.urls);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    return (
        <div>
            <div className="table-container">
                <table>
                    <thead>
                        <tr>
                            <th>Identificator</th>
                            <th>To site</th>
                            <th>Created By</th>
                            <th>Created</th>
                        </tr>
                    </thead>
                    <tbody>
                        {urls.map((url) => (
                            <tr key={url.identificator}>
                                <td>{url.identificator}</td>
                                <td><a href={url.longUrl} target="_blank" rel="noopener noreferrer">{getLocation(url.longUrl)?.hostname}</a></td>
                                <td>{url.createdBy || 'N/A'}</td>
                                <td>{timeAgo(url.createdDate)}</td>
                                <td>Details</td>
                                <td>Delete</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default UrlTable;
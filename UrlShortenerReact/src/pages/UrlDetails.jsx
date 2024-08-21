import { useParams, useNavigate } from 'react-router-dom';
import { getUrlData } from '../services/ApiCallsService';
import { useEffect, useState } from 'react';
import timeAgo from '../tools/timeAgo';
import "../styles/UrlDetails.css"


const UrlDetails = () => {
    const { Identificator } = useParams();
    const navigate = useNavigate();
    const [urlData, setUrlData] = useState(null);
    const firstCountOfCharactersToShow = 50;

    useEffect(() => {
        getUrlData(Identificator).then((result) => {
            console.log(result.data)
            setUrlData(result.data)
        }).catch((err) => {
            console.error(err)
        });
    }, [Identificator]);

    const handleBack = () => {
        navigate(-1)
    };

    const showLongUrl = (longUrl) => {
        console.log(longUrl)
        let longUrlLonger = longUrl.length > firstCountOfCharactersToShow;


        if (longUrlLonger) {
            return longUrl.substring(0, firstCountOfCharactersToShow) + "..."
        }

        return longUrl
    }

    return (
        <div>
            <button onClick={handleBack}>Back</button>
            <div className='url-details-container'>
                <h1>URL details</h1>
                {urlData ? (
                    <>
                        <strong>Long url:</strong> <a href={urlData.longUrl} target="_blank">{showLongUrl(urlData.longUrl)}</a>
                        <p><strong>Created By:</strong> {urlData.createdBy}</p>
                        <p><strong>Created:</strong> {timeAgo(urlData.createdDate)}</p>
                    </>
                ) : (
                    <p>Loading...</p>
                )}
            </div>
        </div>
    );
};

export default UrlDetails;
import { useState } from "react";
import { performCreateUrl } from "../services/ApiCallsService";
import { useNavigate } from "react-router-dom";
import "../styles/CreateUrl.css"

const CreateUrl = () => {
    const [input, setInput] = useState({
        shorten_url: ""
    });
    const [error, setError] = useState(null);

    const navigate = useNavigate()

    const handleSubmitEvent = (e) => {
        e.preventDefault();
        if (input.shorten_url !== "") {
            performCreateUrl(input.shorten_url).then((data) => {
                console.log(data.succeed);
                navigate('/')
            }).catch(error => {
                console.log(error.response.data)
                setError(error.response.data.errors[0])
            })
        }
    };

    const handleInput = (e) => {
        const { name, value } = e.target;

        setInput((prev) => ({
            ...prev,
            [name]: value,
        }));
    };


    return (
        <form onSubmit={handleSubmitEvent}>
            <div className="form_control">
                <label htmlFor="shorten_url">Url:</label>
                <input
                    id="shorten_url"
                    name="shorten_url"
                    onChange={handleInput}
                />
                <button className="btn-submit">Submit</button>
            </div>
            {error && (
                <div style={{ backgroundColor: 'red', color: 'white', padding: '10px', marginTop: '10px' }}>
                    {error}
                </div>
            )}
        </form>
    );
};

export default CreateUrl;
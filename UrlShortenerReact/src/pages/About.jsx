import { getAboutData, updateAboutData } from "../services/ApiCallsService";
import { AuthContext } from "../services/AuthProvider";
import { useContext, useEffect, useState } from "react";
import "../styles/About.css"

const About = () => {
    const { isAdmin } = useContext(AuthContext);
    const [aboutData, setAboutData] = useState(null);


    useEffect(() => {
        getAboutData().then((result) => {
            console.log(result.data.staticDatas[0].content)
            setAboutData(result.data.staticDatas[0].content)
        }).catch((err) => {
            console.error(err)
        });
    }, []);

    const updateAbout = (e) => {
        e.preventDefault();
        const form = e.target;
        const formData = new FormData(form);

        const formJson = Object.fromEntries(formData.entries());
        console.log(formJson.aboutContent);
        setAboutData(formJson.aboutContent);

        updateAboutData("Description", formJson.aboutContent).then((result) => {
            console.log(result.data)
        }).catch((err) => {
            console.error(err)
        });
    }

    return (
        <div className="about">
            {aboutData ? (
                <>
                    <p>
                        {aboutData}
                    </p>
                </>
            ) : (
                <p>Loading...</p>
            )}

            {isAdmin &&
                <>
                    <form onSubmit={updateAbout}>
                        <label> Edit about: </label>

                        <div>
                            <textarea
                                name="aboutContent"
                                rows={4}
                                cols={40}
                            />
                        </div>

                        <div className="edit-buttons-container">
                            <button type="reset">Reset</button>
                            <button type="submit">Save</button>
                        </div>
                    </form>
                </>
            }
        </div>
    );
}

export default About;
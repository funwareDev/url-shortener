import axios from 'axios';
import API_ROUTES from '../config/apiRoutes';
import { AuthContext } from './AuthProvider';
import { useContext } from 'react';

const performLogin = (username, password) => {
    return axios.post(API_ROUTES.Login,
        {
            username: username,
            password: password
        },
        {
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
            },
        })
}

const getUrlData = (id) => {
    console.log("get url data")

    return axios.get(API_ROUTES.Url, { params: { id: id } })
}

const getAboutData = () => {
    console.log("get about data")

    return axios.get(API_ROUTES.About)
}

const updateAboutData = (name, content) => {
    console.log("update about data " + name + " " + content)

    return axios.patch(API_ROUTES.About,
        {
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
            },
            staticData: {
                "name": name,
                "content": content
            }
        })
}

const performCreateUrl = (url) => {
    console.log("start shortening")

    return axios.post(API_ROUTES.ShortenUrl,
        {
            longUrl: url
        },
        {
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
            },
        })
}

const performDeleteUrl = (id) => {
    return axios.delete(API_ROUTES.Url,
        {
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
            },
            data: {
                urlId: id
            }
        })
}

const getLastUrls = (count) => {
    return axios.post(API_ROUTES.Url, { count: count });
};

export { performDeleteUrl, performLogin, performCreateUrl, getUrlData, getLastUrls, getAboutData, updateAboutData }

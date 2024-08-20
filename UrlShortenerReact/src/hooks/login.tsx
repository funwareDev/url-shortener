import axios from 'axios';
import API_ROUTES from '../config/apiRoutes';
import { useContext } from 'react';
import AuthContext, { setStoredToken } from '../services/authProvider';
import { jwtDecode, JwtPayload } from "jwt-decode";

const performLogin = async (username: string, password: string) => {

    try {
        const response = await axios.post(API_ROUTES.Login,
            {
                username: username,
                password: password
            },
            {
                headers: {
                    'accept': '*/*',
                    'Content-Type': 'application/json',
                },
            });

        return response.data;

    } catch (error) {
        console.error('Error:', error);
    }
};

export default performLogin;
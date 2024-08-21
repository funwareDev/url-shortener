import { createContext, useState, useEffect } from 'react';
import Cookies from 'js-cookie'; // You can install this package using `npm install js-cookie`
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';

// Create the context
const AuthContext = createContext();

// Create the provider component
const AuthProvider = ({ children }) => {
  const [authToken, setAuthToken] = useState(null);
  const [isAdmin, setIsAdmin] = useState(null);
  const [username, setUsername] = useState(null);

  // Retrieve the token from cookies when the application starts
  useEffect(() => {
    const token = Cookies.get('authToken');
    if (token) {
      setAuthToken(token);
      retrieveDataFromToken(token)
      console.log("token was retrieved from cookies")
    }

    
  }, []);

  // Function to log in and set the token
  const login = (token) => {
    Cookies.set('authToken', token);

    console.log("token was set")

    setAuthToken(token);
    retrieveDataFromToken(token)
  };

  const retrieveDataFromToken = (token) => {
    const decoded = jwtDecode(token);
    const name = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]
    const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]

    setIsAdmin(role === "Admin")
    setUsername(name)
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
  }

  // Function to log out and remove the token
  const logout = () => {
    Cookies.remove('authToken');
    console.log("token was removed")
    setAuthToken(null);
    axios.defaults.headers.common['Authorization'] = null;
  };

  return (
    <AuthContext.Provider value={{ authToken, login, logout, isAdmin, username }}>
      {children}
    </AuthContext.Provider>
  );
};

export { AuthContext, AuthProvider };
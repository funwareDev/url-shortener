import axios from "axios";
import React, { createContext, useContext, useEffect, useMemo, useState } from "react";
import Cookies from "js-cookie";

let isDesignMode = false;

let isLoggedIn = false;
let isAdmin = false;
let userId = undefined;
let userName = undefined;


export function getStoredToken() {
    return Cookies.get('bearer-token');
}

export function setStoredToken(token) {
    Cookies.set('bearer-token', token);
}

if (isDesignMode) {
    isLoggedIn = true;
    isAdmin = true;
    userId = "admin-id";
    userName = "Admin";
}

const AuthContext = React.createContext({    
    isLoggedIn,
    isAdmin,
    userId,
    userName
});

export default AuthContext;
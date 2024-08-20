const API_BASE_URL = "https://localhost:7201";

const API_ROUTES = {
  Url: `${API_BASE_URL}/Url`,
  About: `${API_BASE_URL}/About`,
  Login: `${API_BASE_URL}/Auth/login`,
  ShortenUrl: `${API_BASE_URL}/Url/shorten-url`,
  // Add more routes as needed
};

export default API_ROUTES;
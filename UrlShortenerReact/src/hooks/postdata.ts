import axios from 'axios';
import API_ROUTES from '../config/apiRoutes';

const getUrls = async () => {
  try {
    const response = await axios.post(API_ROUTES.Url, 
      {
        count: 5
      }, 
      {
        headers: {
          'accept': '*/*',
          'Content-Type': 'application/json',
        },
      });

      console.log("api route " + API_ROUTES.Url)
      console.log(response.data)

    return response.data;
    
  } catch (error) {
    console.error('Error:', error);
  }
};

export default getUrls;

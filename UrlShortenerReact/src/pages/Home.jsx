import { useContext } from "react";
import UrlTable from "../components/UrlTable";

const Home = () => {

  return (
    <div className="home">
      { <UrlTable /> }
    </div>
  );
}
 
export default Home;
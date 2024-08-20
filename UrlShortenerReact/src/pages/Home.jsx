import { useContext } from "react";
import UrlTable from "../components/UrlTable";

const Home = () => {
  return (
    <div className="home">
      {/* { error && <div>{ error }</div> } */}
      {/* { isPending && <div>Loading...</div> } */}
      {/* { <UrlTable /> } */}
      { <UrlTable /> }
    </div>
  );
}
 
export default Home;
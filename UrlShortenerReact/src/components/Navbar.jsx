import { useContext } from "react";
import "../styles/Navbar.css"
import AuthContext from "../services/authProvider";

const Navbar = () => {
  let context = useContext(AuthContext);

  return (
    <nav className="navbar">
      <h1>Url shorterer</h1>
      <div className="links">
        <a href="/">Home</a>
        <a href="/about">About</a>
        {context.isLoggedIn ? <a href="/create-url" className="create-link">Create</a> : <a href="/login" className="login">Login</a>}
      </div>
    </nav>
  );
}

export default Navbar;
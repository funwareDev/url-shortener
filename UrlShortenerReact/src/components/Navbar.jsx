import { useContext } from "react";
import "../styles/Navbar.css"
import { AuthContext } from "../services/AuthProvider";

const Navbar = () => {
  const { authToken, logout } = useContext(AuthContext);

  return (
    <nav className="navbar">
      <h1>Url shorterer</h1>
      <div className="links">
        <a href="/">Home</a>
        <a href="/about">About</a>
        {authToken ? <a href="/create-url" className="create-link">Create</a> : <a href="/login" className="login">Login</a>}
        {authToken && <a><button onClick={logout}>Logout</button></a>}
      </div>
    </nav>
  );
}

export default Navbar;
import { useState, useContext } from "react";
import performLogin from "../hooks/login";
import AuthContext, { setStoredToken } from "../services/authProvider";
import { jwtDecode } from "jwt-decode";

const Login = () => {
  let context = useContext(AuthContext);


  const [input, setInput] = useState({
    username: "",
    password: "",
  });

  const handleSubmitEvent = (e) => {
    e.preventDefault();
    if (input.username !== "" && input.password !== "") {
      performLogin(input.username, input.password).then((data) => {
        setStoredToken(data.token)
        const decoded = jwtDecode(data.token);
        const name = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]
        const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]

        context.isAdmin = role === "Admin"
        context.userName = name

        console.log(decoded)
        console.log(name)
        console.log(role)
      })
    }
  };

  const handleInput = (e) => {
    const { name, value } = e.target;
    setInput((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  return (
    <form onSubmit={handleSubmitEvent}>
      <div className="form_control">
        <label htmlFor="username">Username:</label>
        <input
          type="username"
          id="username"
          name="username"
          onChange={handleInput}
        />
      </div>
      <div className="form_control">
        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          name="password"
          onChange={handleInput}
        />
      </div>
      <button className="btn-submit">Submit</button>
    </form>
  );
};

export default Login;
import { useState, useContext } from "react";
import { performLogin } from "../services/ApiCallsService";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../services/AuthProvider";

const Login = () => {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogin = (token) => {
    login(token);
    navigate("/");
    console.log("token " + token)
  };

  const [input, setInput] = useState({
    username: "",
    password: "",
  });

  const handleSubmitEvent = (e) => {
    e.preventDefault();
    if (input.username !== "" && input.password !== "") {
      performLogin(input.username, input.password).then((res) => {
        handleLogin(res.data.token);
    })
    }
  };

  const handleInput = (e) => {
    e.preventDefault();
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
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
      let data = performLogin(input.username, input.password)

      setStoredToken(data.token)
      const decoded = jwtDecode(data.token);
      console.log(decoded)
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
          placeholder="username"
          aria-describedby="user-name"
          aria-invalid="false"
          onChange={handleInput}
        />
      </div>
      <div className="form_control">
        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          name="password"
          aria-describedby="user-password"
          aria-invalid="false"
          onChange={handleInput}
        />
      </div>
      <button className="btn-submit">Submit</button>
    </form>
  );
};

export default Login;
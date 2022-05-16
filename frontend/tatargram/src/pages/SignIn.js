import { Button, TextField } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../services/auth.service";
import "../css/SignIn.css";

export function SignIn() {

  const [form, setForm] = useState({ userName: "", password: "" });
  const [error, setError] = useState({});
  const navigate = useNavigate();

  function uploadForm() {
    authService.signIn(form).then(response => {
      if (response) {
        localStorage.setItem("token", response.data.accessToken);
        navigate("/");
      }
    })
      .catch(error => {
        setError(error.response.data);
        console.log(error)
      })
  }

  return (
    <div className="login">
      <div className="loginWrapper">
        <div className="loginLeft">
          <h3 className="loginLogo">Авторизация</h3>
        </div>
        <div className="loginRight">
          <span>{error.message}</span>
          <TextField
            label="Имя пользователья"
            variant="standard"
            onChange={(e) => setForm({ ...form, userName: e.target.value })}
          />
          <TextField
            label="Пароль"
            variant="standard"
            margin="normal"
            type="password"
            onChange={(e) => setForm({ ...form, password: e.target.value })}
          />
          <Button
            variant="contained"
            onClick={uploadForm}
          >
            Войти
          </Button>
        </div>
      </div>
    </div>
  );
}

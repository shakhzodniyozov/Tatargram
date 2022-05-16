import { useState } from "react";
import { Button, TextField } from "@mui/material";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import authService from "../services/auth.service";
import { useNavigate } from "react-router-dom";
import "../css/SignUp.css";

export function SignUp() {

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    userName: "",
    dateOfBirth: new Date(),
    password: ""
  });

  const navigate = useNavigate();

  function uploadForm() {
    authService.signUp(form).then(response => {
      if (response) {
        localStorage.setItem("token", response.data.accessToken);
        navigate("/");
      }
    });
    console.log(form);
  }

  return (
    <div className="login">
      <div className="loginWrapper">
        <div className="loginLeft">
          <h3 className="loginLogo">Our App</h3>
        </div>
        <div className="loginRight">
          <TextField
            label="Имя"
            variant="standard"
            onChange={e => setForm({ ...form, firstName: e.target.value })}
          />
          <div className="mt-2 d-flex flex-column">
            <TextField
              label="Фамилия"
              variant="standard"
              onChange={e => setForm({ ...form, lastName: e.target.value })}
            />
          </div>
          <div className="mt-2 d-flex flex-column">
            <TextField
              label="Имя пользователя"
              variant="standard"
              onChange={e => setForm({ ...form, userName: e.target.value })}
            />
          </div>
          <div className="mt-2 d-flex flex-column">
            <TextField
              label="Пароль"
              type="password"
              variant="standard"
              onChange={e => setForm({ ...form, password: e.target.value })}
            />
          </div>
          <div className="mt-4 mb-2 d-flex flex-column">
            <LocalizationProvider dateAdapter={AdapterDateFns}>
              <DatePicker
                renderInput={(props) => <TextField {...props} />}
                value={form.dateOfBirth}
                label="Дата рождения"
                onChange={(newValue) => {
                  setForm({ ...form, dateOfBirth: newValue })
                }}
              />
            </LocalizationProvider>
          </div>
          <Button
            variant="contained"
            onClick={uploadForm}
          >
            Зарегистрироваться
          </Button>
        </div>
      </div>
    </div>
  );
}

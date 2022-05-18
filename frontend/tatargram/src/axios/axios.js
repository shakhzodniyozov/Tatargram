import axios from "axios";

const ax = axios.create({
    baseURL: `http://localhost:5000/api`,
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
});

ax.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response.status === 401)
        window.location.href = "/signin";
    return Promise.reject(error);
});

ax.interceptors.request.use(request => {
    const user = JSON.parse(localStorage.getItem("user"));
    if (user) {
        request.headers["Authorization"] = "Bearer " + user.accessToken;
    }

    return request;
});

export default ax;
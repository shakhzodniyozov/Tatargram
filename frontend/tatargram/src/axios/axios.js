import axios from "axios";

axios.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response.status === 401)
        window.location.href.replace("/signin");
    return Promise.reject(error);
});

axios.interceptors.request.use(request => {
    const user = JSON.parse(localStorage.getItem("user"));
    if (user) {
        request.headers["Authorization"] = "Bearer " + user.accessToken;
    }

    return request;
})

export default axios.create({
    baseURL: "https://localhost:5001/api",
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
});
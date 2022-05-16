import axios from "axios";

axios.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response.status === 401)
        window.location.href.replace("/login");
    return Promise.reject(error);
});

axios.interceptors.request.use(request => {
    const token = localStorage.getItem("token");
    if (token) {
        request.headers["Authorization"] = "Bearer " + token;
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
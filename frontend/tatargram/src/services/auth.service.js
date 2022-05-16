import axios from "../axios/axios";

class AuthService {
    async signIn(data) {
        return await axios.post("/account/signin", data);
    }

    async signUp(data) {
        return await axios.post("/account/signup", data);
    }

    isAuthenticated() {
        return localStorage.getItem("token") !== null
    }
}

export default new AuthService();
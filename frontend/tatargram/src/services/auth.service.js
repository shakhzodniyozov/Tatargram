import axios from "../axios/axios";

class AuthService {
    user;
    constructor() {
        this.user = JSON.parse(localStorage.getItem("user"));
    }

    async signIn(data) {
        return await axios.post("/account/signin", data);
    }

    async signUp(data) {
        return await axios.post("/account/signup", data);
    }

    signOut() {
        localStorage.removeItem("user");
    }

    isAuthenticated() {
        return localStorage.getItem("user") !== null
    }
}

export default new AuthService();
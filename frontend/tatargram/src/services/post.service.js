import axios from "../axios/axios";

class PostService {

    async getPosts() {
        return await axios.get("/posts");
    }
}

export default new PostService;
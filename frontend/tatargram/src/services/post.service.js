import axios from "../axios/axios";

class PostService {

    async getPosts() {
        return await axios.get("/posts");
    }

    async getLikedUsers(postId) {
        return await axios.get(`/likes/${postId}`)
    }

    async likePost(postId) {
        return await axios.post(`/posts/like/${postId}`)
    }

    async unlikePost(postId) {
        return await axios.post(`/posts/unlike/${postId}`)
    }
}

export default new PostService;
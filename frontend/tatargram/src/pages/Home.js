import "../css/App.css";
import "../css/post.css";
import { Post } from '../components/Post';
import { Topbar } from '../components/Topbar';
import { useEffect, useState } from "react";
import postService from "../services/post.service";
import { CircularProgress } from "@mui/material";

export function Home() {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    postService.getPosts().then(response => {
      if (response) {
        setPosts(response.data);
        setLoading(false);
      }
    })
  }, []);

  return !loading ? (
    <>
      <Topbar />
      <div className="container">
        {posts.map(post => {
          return <Post key={post.id} post={post} posts={posts} setPosts={setPosts} />
        })}
      </div>
    </>
  ) : (
    <div className="d-flex justify-content-center">
      <CircularProgress />
    </div>
  )
}
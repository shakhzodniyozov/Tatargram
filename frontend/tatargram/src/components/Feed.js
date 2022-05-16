
import { Avatar, Button, CircularProgress, TextField } from "@mui/material";
import { useState, useEffect } from "react";
import postService from "../services/post.service";
import "../css/post.css";
import { Link } from "react-router-dom";
import { BsHeart, BsHeartFill } from "react-icons/bs";
import { IoChatbubbleOutline } from "react-icons/io5";

export function Feed() {
  const [posts, setPosts] = useState([]);
  const [comment, setComment] = useState({ postId: "", text: "", publishDate: null });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    postService.getPosts().then(response => {
      if (response) {
        setPosts(response.data);
        setLoading(false);
      }
    })
  }, []);

  function likeThePost() {

  }

  return !loading ? (
    <div className="d-flex flex-column align-items-center">
      {posts.map(post => {
        return (
          <div key={post.id} className="post">
            <div className="px-4 py-3">
              <Link
                to={`/profile/${post.authorId}`}
                style={{ textDecoration: "none", color: "black" }}
              >
                <div className="d-flex flex-row align-items-center">
                  <Avatar
                    src={post.authorPhoto}
                  />
                  <span className="mx-1 text-black">
                    <strong>{post.authorFullName}</strong>
                  </span>
                </div>
              </Link>
            </div>
            <div>
              {post.images.map(image => {
                return (
                  <img
                    className="postImg"
                    src={`http://localhost:5000/api/content?path=${image}`}
                    alt={post.description}
                  />
                )
              })}
            </div>
            <div className="p-3 d-flex flex-column">
              <div className="d-flex">
                {
                  post.liked ?
                    <BsHeartFill
                      size={32}
                      color="#ed404f"

                    /> :
                    <BsHeart
                      size={32}
                      onClick={likeThePost}
                    />
                }
                <IoChatbubbleOutline size={32} className="mx-3" />
              </div>
              <span className="mt-1"><strong>{post.likes} лайков</strong></span>
              <span className="mt-2">{post.description}</span>
              <span className="text-secondary">{post.publishDate}</span>
              <div className="d-flex mt-1">
                <TextField
                  label="Коммент..."
                  variant="standard"
                  fullWidth
                  onChange={e => setComment({ ...comment, postId: post.id, text: e.target.value })}
                />
                <Button
                  variant="text"
                  className="ms-1"
                  disabled={comment.text.length === 0}
                >
                  Отправить
                </Button>
              </div>
            </div>
          </div>
        )
      })}
    </div>
  ) : (
    <div className="d-flex justify-content-center mt-4">
      <CircularProgress />
    </div>
  );
}
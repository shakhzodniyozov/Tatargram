
import { Avatar, Button, TextField } from "@mui/material";
import { useState } from "react";
import postService from "../services/post.service";
import "../css/post.css";
import { Link } from "react-router-dom";
import { BsHeart, BsHeartFill } from "react-icons/bs";
import { IoChatbubbleOutline } from "react-icons/io5";

export function Post({ post, posts, setPosts }) {
  const [comment, setComment] = useState({ postId: "", text: "", publishDate: null });

  function likePost(postId) {
    let postsCopy = [...posts];
    let likedPost = postsCopy.find(x => x.id === postId);
    likedPost.likes += 1;
    likedPost.liked = true;
    setPosts(postsCopy);
    postService.likePost(postId);
  }

  function unlikePost(postId) {
    let postsCopy = [...posts];
    let likedPost = postsCopy.find(x => x.id === postId);
    likedPost.likes -= 1;
    likedPost.liked = false;
    setPosts(postsCopy);
    postService.unlikePost(postId);
  }

  function getCorrectWord(x) {
    let str = "лайк";
    if (x === 0 || x > 4)
      str += "ов";
    else if (x > 1 && x < 5)
      str += "а";
    return str;
  }

  return (
    <div className="d-flex flex-column align-items-center">
      <div key={post.id} className="post">
        <div className="px-4 py-3">
          <Link
            to={`/${post.authorUserName}`}
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
          {post.images.map((image, i) => {
            return (
              <img
                key={i}
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
                  onClick={(e) => unlikePost(post.id)}
                /> :
                <BsHeart
                  size={32}
                  onClick={_ => likePost(post.id)}
                />
            }
            <IoChatbubbleOutline size={32} className="mx-3" />
          </div>
          <div className="mt-1">
            <span
              style={{ cursor: "pointer" }}
            >
              <strong>{post.likes} {getCorrectWord(post.likes)}</strong>
            </span>
          </div>
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
    </div>
  )
}
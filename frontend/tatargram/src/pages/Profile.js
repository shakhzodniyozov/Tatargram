import "../css/App.css";
import "../css/post.css";
import "../css/profile.css"
import { Topbar } from '../components/Topbar';
import axios from "../axios/axios";
import { useState, useEffect } from "react";
import { Post } from "../components/Post";
import { useParams } from "react-router-dom";
import { NewPost } from "../components/NewPost";
import { Button } from "@mui/material";

export function Profile() {
  const [user, setUser] = useState({ followers: [], followings: [], posts: [] });
  const profileImage = {
    imagePath: "",
    imageAsBase64: ""
  };

  const params = useParams();

  useEffect(() => {
    axios.get(`/User/${params.userName}`).then(response => {
      if (response) setUser(response.data)
    })
  }, [params.userName]);

  function setPosts(updatedPosts) {
    setUser({ ...user, posts: updatedPosts });
  }

  function onProfileImageChange(e) {
    const reader = new FileReader();

    reader.onload = (e) => {
      profileImage.imageAsBase64 = e.target.result;

      axios.post("/user/profile-image", profileImage).then(response => {
        if (response) {
          let posts = [...user.posts];

          posts.forEach(x => x.authorPhoto = response.data.profileImage);
          setUser({ ...user, profileImage: response.data.profileImage, posts: posts });
        }
      });
    }

    reader.readAsDataURL(e.target.files[0]);
  }

  return (
    <>
      <Topbar />
      <div className="profile">
        <div className="profileRight">
          <div className="profileRightTop">
            <div className="profileCover">
              <img
                className="profileCoverImg"
                src={require("../assets/noCover.png")}
                alt=""
              />
              <img
                className="profileUserImg"
                src={user.profileImage ? `http://localhost:5000/api/content?path=${user.profileImage}` : require("../assets/noAvatar.png")}
                alt=""
              />
              <input hidden type="file" id="profileImg" onChange={onProfileImageChange} />
            </div>
            <div className="profileInfo">
              <Button
                onClick={_ => document.getElementById("profileImg").click()}
              >
                Сменить фото
              </Button>
              <h4 className="profileInfoName">{user.fullName}</h4>
              <span className="profileInfoDesc">Followers: {user.followers.length} Following: {user.followings.length}</span>
            </div>
          </div>
          <div className="profileRightBottom">
          </div>
        </div>
      </div>
      <div className="container">
        <NewPost posts={user.posts} setPosts={setPosts} />
        {user.posts.map(post => {
          return <Post key={post.id} post={post} posts={user.posts} setPosts={setPosts} />
        })}
      </div>
    </>
  );
}

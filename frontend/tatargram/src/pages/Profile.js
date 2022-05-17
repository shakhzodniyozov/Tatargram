import "../css/App.css";
import "../css/post.css";
import "../css/profile.css"
import { Topbar } from '../components/Topbar';
import axios from "../axios/axios";
import { useState, useEffect } from "react";
import { Post } from "../components/Post";
import { useParams } from "react-router-dom";

export function Profile() {
  const [user, setUser] = useState({ followers: [], followings: [], posts: [] });

  const params = useParams();

  useEffect(() => {
    axios.get(`/User/${params.userName}`).then(response => {
      if (response) setUser(response.data)
    })
      .catch(function (error) {
        console.log(error);
      });
  }, [params.userName]);


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
                src={require("../assets/noAvatar.png")}
                alt=""
              />
            </div>
            <div className="profileInfo">
              <h4 className="profileInfoName">{user.fullName}</h4>
              <span className="profileInfoDesc">Followers: {user.followers.length} Following: {user.followings.length}</span>
            </div>
          </div>
          <div className="profileRightBottom">
          </div>
        </div>
      </div>
      <div className="container">
        {user.posts.map(post => {
          return <Post key={post.id} post={post} posts={user.posts} setPosts={null} />
        })}
      </div>
    </>
  );
}

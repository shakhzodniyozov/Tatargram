import "../css/App.css";
import "../css/post.css";
import "../css/profile.css"
import Feed from '../components/Feed';
import Topbar from '../components/Topbar';
import Sidebar from '../components/Sidebar';
import axios from "../axios/axios";
import { useState, useEffect } from "react";    

function Profile() {
    const token = localStorage.getItem('user')
    const [dt, setDt] = useState({})
    const [followers, setFollowers] = useState()
    const [followings, setFollowings] = useState()

    useEffect(() => {
        axios.get('/User/current')
        .then(function (response) {
            console.log(response.data);
            setFollowers(response.data.followers.length)
            setFollowings(response.data.followings.length)
            setDt(response.data);
            
        })
        .catch(function (error) {
            console.log(error);
        });  
      }, []);
    

    return (
        <>
            <Topbar />
            <div className="profile">
                <Sidebar />
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
                            <h4 className="profileInfoName">{dt.fullName}</h4>
                            {/* <span className="profileInfoDesc">This is currently empty</span> */}
                            <span className="profileInfoDesc">Followers: {followers} Following: {followings}</span>
                        </div>
                    </div>
                    <div className="profileRightBottom">
                        <Feed />
                    </div>
                </div>
            </div>
        </>
    );
}

export default Profile;

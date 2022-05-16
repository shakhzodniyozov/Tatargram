import "../css/App.css";
import "../css/post.css";
import "../css/profile.css"
import Feed from '../components/Feed';
import Topbar from '../components/Topbar';
import Sidebar from '../components/Sidebar';
import axios from "../axios/axios";

function Profile() {
    const token = localStorage.getItem('user')
    const data = () => {
        axios.get('/User/current')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    const fakedata = {
        fullName: "Имя Фамилия",
        dateOfBirth: "string",
        isSubscribed: true,
        profileImage: "string",
        posts: [
            {
                id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                authorId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                authorFullName: "string",
                description: "string",
                publishDate: "string",
                likes: 0,
                liked: true,
                images: [
                    "string"
                ],
                authorPhoto: "string"
            }
        ],
        followers: [
            {
                id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                fullName: "string"
            }
        ],
        followings: [
            {
                id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                fullName: "string"
            }
        ]
    }

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
                            <h4 className="profileInfoName">{fakedata.fullName}</h4>
                            <span className="profileInfoDesc">This is currently empty</span>
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
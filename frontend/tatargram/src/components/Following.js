// import "./closeFriend.css";

export default function Following({user}) {
  return (
    <li className="sidebarFriend">
      {/* <img className="sidebarFriendImg" src={PF+user.profilePicture} alt="" /> */}
      <span className="sidebarFriendName">{user.username}</span>
    </li>
  );
}

import { Search, Person, Chat, Notifications } from "@mui/icons-material";
import { Link } from "react-router-dom";
import authService from "../services/auth.service";
import { Avatar, Typography } from "@mui/material";
import "../css/topbar.css";

export default function Topbar() {

  return (
    <div className="topbarContainer">
      <div className="topbarLeft">
      </div>
      <div className="topbarCenter">
        <div className="searchbar">
          <Search className="searchIcon" />
          <input
            placeholder="Search for friend, post or video"
            className="searchInput"
          />
        </div>
      </div>
      <div className="topbarRight">
        <div className="topbarLinks">
          <span className="topbarLink">Home</span>
          <span className="topbarLink">Timeline</span>
        </div>
        <div className="topbarIcons">
          <div className="topbarIconItem">
            <Person />
            <span className="topbarIconBadge">1</span>
          </div>
          <div className="topbarIconItem">
            <Chat />
            <span className="topbarIconBadge">2</span>
          </div>
          <div className="topbarIconItem">
            <Notifications />
            <span className="topbarIconBadge">1</span>
          </div>
        </div>
        {authService.isAuthenticated() ?
          <Link to={`/profile`}>
            <div className="d-flex align-items-center">
              <Avatar
                src={authService.user.profileImage}
              />
              <Typography className="mx-1" style={{ color: "white" }}>{authService.user.fullName}</Typography>
            </div>
          </Link> :
          <Link
            to={"/signin"}
            style={{ color: "white" }}
          >
            Войти
          </Link>
        }
      </div>
    </div>
  );
}
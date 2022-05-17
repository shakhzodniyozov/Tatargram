import { Search } from "@mui/icons-material";
import { Link } from "react-router-dom";
import authService from "../services/auth.service";
import { Avatar, Typography } from "@mui/material";
import "../css/topbar.css";

export function Topbar() {

  return (
    <div className="topbarContainer">
      <div className="topbarLeft">
        <Link
          to="/"
          style={{ color: "white", marginLeft: "3rem", fontSize: "2em", textDecoration: "none" }}
        >
          Tatargram
        </Link>
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
        {authService.isAuthenticated() ?
          <Link to={`/${authService.user?.userName}`}>
            <div className="d-flex align-items-center">
              <Avatar
                src={authService.user?.profileImage}
              />
              <Typography className="mx-1" style={{ color: "white" }}>{authService.user?.fullName}</Typography>
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
import { Avatar, Dialog, DialogContent, DialogTitle, IconButton } from "@mui/material";
import { useEffect, useState } from "react";
import { IoCloseSharp } from "react-icons/io5";
import postService from "../services/post.service";

export function Likes(props) {

    const [users, setUsers] = useState([]);

    useEffect(() => {
        postService.getLikedUsers(props.postId).then(response => {
            if (response) setUsers(response.data);
        });
    }, []);
    return (
        <Dialog open={props.open}>
            <DialogTitle>
                <strong>Лайки</strong>
                <IconButton>
                    <IoCloseSharp />
                </IconButton>
            </DialogTitle>
            <DialogContent dividers>
                {users.map(user => {
                    return (
                        <div className="d-flex">
                            <Avatar
                                src={user.profileImage}
                            />
                            <div>{user.fullName}</div>
                        </div>
                    )
                })}
            </DialogContent>
        </Dialog>
    )
}
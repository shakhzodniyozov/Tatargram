import * as React from 'react';
import { useState, useEffect } from "react";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';

export default function Comments({ postid }) {
    const [comments, setComments] = useState([])
    useEffect(() => {
        fetch(`https://jsonplaceholder.typicode.com/comments/?postId=${postid}`)
            .then(response => response.json())
            .then(json => setComments(json))
    }, [])

    return (
        <>
            <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper', marginLeft: '2%' }}>
                {comments.map(comment => {
                    return <>
                        <Divider style={{ marginLeft: '0%' }} variant="inset" component="li" />
                        <ListItem alignItems="flex-start">
                            <ListItemAvatar>
                                <Avatar alt="U" src="/static/images/avatar/1.jpg" />
                            </ListItemAvatar>
                            <ListItemText
                                primary={comment.name}
                                secondary={
                                    <React.Fragment>
                                        <Typography
                                            sx={{ display: 'inline' }}
                                            component="span"
                                            variant="body2"
                                            color="text.primary"
                                        >
                                            {comment.email}
                                        </Typography>
                                        {/* <span>     {comment.body}</span> */}
                                    </React.Fragment>
                                }
                            />


                        </ListItem>
                        <span style={{ fontSize: '80%' }}>     {comment.body}</span>
                    </>
                })}
            </List>
        </>

    );
}
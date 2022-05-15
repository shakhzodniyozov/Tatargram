
import { useState, useEffect } from "react";
import "./post.css"
import Comments from './Comments';
//import TodoApp from './Todo';

function Feed() {
    const [resourceType, setResourceType] = useState([])
    const [items, setItems] = useState([])
    const [comments, setComments] = useState([])
    const [id, setId] = useState([])

    const getInputValue = (event) => {
        // show the user input value to console
        setId(event.target.value)
        console.log(comments)
        console.log(items)
    };

    useEffect(() => {
        fetch(`https://jsonplaceholder.typicode.com/${resourceType}${id}`)
            .then(response => response.json())
            .then(json => setItems(json))
        console.log(id)

        fetch(`https://jsonplaceholder.typicode.com/comments/?postId=${id}`)
            .then(response => response.json())
            .then(json => setComments(json))
    }, [resourceType])

    return (

        <>

            <div className="feed">

                <div>
                    <button onClick={() => setResourceType('posts/?userId=')}>Posts</button>
                    <button onClick={() => setResourceType('users/?id=')}>Users</button>
                    <button onClick={() => setResourceType('comments/?postId=')}>Comments</button>

                    <input type="text" onChange={getInputValue} />
                </div>

                <h1>
                    {resourceType}
                </h1>

                {items.map(item => {
                    //   return <div>
                    //       <h2>{item.title}</h2>

                    //       <span>{item.body}</span>
                    //       </div>


                    return <div className="post">
                        <div className="postWrapper">
                            <div className="postTop">
                                <div className="postTopLeft">
                                    {/* <img
                                            className="postProfileImg"
                                            src={Users.filter((u) => u.id === post?.userId)[0].profilePicture}
                                            alt=""
                                        /> */}
                                    <span className="postUsername">
                                        User: {item.userId}
                                    </span>


                                    {/* <span className="postDate">{post.date}</span> */}
                                </div>

                                {/* <div className="postTopRight">
                                    </div> */}
                            </div>
                            <div><h2>{item.title}</h2></div>
                            <div className="postCenter">
                                <span className="postText">{item.body}</span>
                                <img className="postImg" src="https://upload.wikimedia.org/wikipedia/ru/7/7c/Goofy2013.jpg" alt="" />
                            </div>
                            <div className="postBottom">
                                <div className="postBottomLeft">
                                    {/* <img className="likeIcon" src="assets/like.png" onClick={likeHandler} alt="" />
                                        <img className="likeIcon" src="assets/heart.png" onClick={likeHandler} alt="" />
                                        <span className="postLikeCounter">{like} people like it</span> */}
                                </div>
                                <div className="postBottomRight">
                                    {/* <span className="postCommentText">{post.comment} comments</span> */}
                                </div>
                            </div>
                        </div>
                        <Comments postid={item.id} />
                    </div>



                })}

            </div>

        </>
    );
}

export default Feed;

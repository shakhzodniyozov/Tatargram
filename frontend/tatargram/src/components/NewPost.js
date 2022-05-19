import { Button, TextField } from "@mui/material";
import { useState } from "react";
import { MdSend } from "react-icons/md";
import postService from "../services/post.service";

export function NewPost({ posts, setPosts }) {
  const [newPost, setNewPost] = useState({ photos: [], description: "" })

  function uploadPost() {
    postService.create(newPost).then(response => {
      if (response) {
        setNewPost({ description: "", photos: [] });
        console.log(response.data);
        setPosts([response.data, ...posts])
      }
    });
  }

  function onFileUpload(e) {
    const reader = new FileReader();

    reader.onload = (e) => {
      document.getElementById("preview").setAttribute("src", e.target.result);
      setNewPost({ ...newPost, photos: [...newPost.photos, e.target.result] });
    }

    reader.readAsDataURL(e.target.files[0]);
  }

  return (
    <div className="d-flex flex-column m-auto" style={{ width: "70%" }}>
      <div>
        <img src={newPost.photos[0]} alt="" id="preview" className="postImg" />
        <input hidden type="file" id="postImg" onChange={onFileUpload} />
      </div>
      <div className="d-flex justify-content-center p-2 shadow mt-3">
        <Button
          variant="outlined"
          onClick={() => document.getElementById("postImg").click()}
        >
          Загрузить фото
        </Button>
        <TextField
          variant="standard"
          placeholder="Что нового?"
          style={{ width: "60%" }}
          className="mx-2"
          value={newPost.description}
          onChange={(e => setNewPost({ ...newPost, description: e.target.value }))}
        />
        <Button
          endIcon={<MdSend color="white" />}
          variant="contained"
          onClick={_ => uploadPost()}
          disabled={newPost.photos.length === 0}
        >
          Отправить
        </Button>
      </div>
    </div>
  )
}
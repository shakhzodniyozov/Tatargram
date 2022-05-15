import '../App.css';
import { useState, useEffect } from "react";
import "../components/post.css"
import Comments from '../components/Comments';
import Feed from '../components/Feed';
import Topbar from '../components/Topbar';
import Sidebar from '../components/Sidebar';
import Register from './Register';
import Login from './Login';
import { Routes, Route, Link } from 'react-router-dom';
//import TodoApp from './Todo';

function Home() {

    return (
        <>
            <Topbar />
            <div style={{display: 'flex', width: '100%'}} className="homeContainer">
                <Sidebar />
                <Feed />
            </div>
        </>
    );
}

export default Home;

import './App.css';
import { useState, useEffect } from "react";
//import "./post.css"
import Home from './pages/Home';
import Register from './pages/Register';
import Login from './pages/Login';
import { Routes, Route, Link } from 'react-router-dom';
//import TodoApp from './Todo';

function App() {

    return (
        <>
           
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/signup" element={<Register />} />
            </Routes>




        </>
    );
}

export default App;

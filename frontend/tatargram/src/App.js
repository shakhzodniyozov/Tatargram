import './css/App.css';
import Home from './pages/Home';
import { SignUp } from './pages/SignUp';
import { Routes, Route } from 'react-router-dom';
import { SignIn } from './pages/SignIn';
import { Profile } from './pages/Profile';

function App() {

    return (
        <Routes>
            <Route path="/" element={<Profile />} />
            <Route path="/home" element={<Home />} />
            <Route path="/signin" element={<SignIn />} />
            <Route path="/signup" element={<SignUp />} />
        </Routes>
    );
}

export default App;


import './css/App.css';
import Home from './pages/Home';
import { SignUp } from './pages/SignUp';
import { Routes, Route, Link } from 'react-router-dom';
import { SignIn } from './pages/SignIn';

function App() {

    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/signin" element={<SignIn />} />
            <Route path="/signup" element={<SignUp />} />
        </Routes>
    );
}

export default App;

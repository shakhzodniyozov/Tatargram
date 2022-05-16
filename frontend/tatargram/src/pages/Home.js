import "../css/App.css";
import "../css/post.css";
import { Feed } from '../components/Feed';
import { Topbar } from '../components/Topbar';

function Home() {
    return (
        <>
            <Topbar />
            <div className="container">
                <Feed />
            </div>
        </>
    );
}

export default Home;
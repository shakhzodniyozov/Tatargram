import "../css/App.css";
import "../css/post.css";
import Feed from '../components/Feed';
import Topbar from '../components/Topbar';
import Sidebar from '../components/Sidebar';

function Home() {
    return (
        <>
            <Topbar />
            <div className="container">
                {/* <Sidebar /> */}
                <Feed />
            </div>
        </>
    );
}

export default Home;
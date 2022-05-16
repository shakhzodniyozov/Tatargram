import "../css/App.css";
import "../css/post.css";
import Feed from '../components/Feed';
import Topbar from '../components/Topbar';
import Sidebar from '../components/Sidebar';

function Home() {

    return (
        <>
            <Topbar />
            <div style={{ display: 'flex', width: '100%' }} className="homeContainer">
                <Sidebar />
                <Feed />
            </div>
        </>
    );
}

export default Home;
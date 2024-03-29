import { Link } from "react-router-dom";
import TableWithPaginationUsage from "./Table/TableWithPaginationUsage"

const HomePage = () => (
    <div className="jumbotron">
        < TableWithPaginationUsage />
        <br />
        <h1>Curated Art</h1>
        <p>React and React Router for ultra-responsive web apps.</p>
        <Link to="about" className="btn btn-primary btn-lg">
            Learn more
        </Link>
    </div>
);

export default HomePage;

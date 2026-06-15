import { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";

export default function NavBar() {
    const { isAuthenticated } = useContext(AuthContext);

    return (
        <>
            {isAuthenticated && <nav className="navbar">
                <ul >
                    <li><Link to="/dashboard">Dashboard</Link></li>
                    <li><Link to="/workspaces">Workspace</Link></li>
                    <li><Link to="/project/:id">Project</Link></li>
                    <li><Link to="/task/:projectId">Tasks</Link></li>
                </ul>
            </nav>}
        </>
    )
}
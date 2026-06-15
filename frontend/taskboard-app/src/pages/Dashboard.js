
import React, { useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import WorkspaceList from './WorkspaceList';
import ProjectList from './ProjectList';
import TaskList from './TaskList';
import { getWorkspace } from "../api/workspaceAPI";


export default function Dashboard() {
    const navigate = useNavigate();
    const { currentUser } = useContext(AuthContext);
    const [workspaceList, setWorkspaceList] = useState([]);
    const [projectList, setProjectList] = useState([]);
    const [taskList, setTaskList] = useState([]);
    const [loading, setLoading] = useState(false);


    useEffect(() => {

    }, [])   

    const viewDetails = (workspace) => {
        const id = workspace.workspaceId;
        navigate(`/Project/${id}`, {
            state: {
                workspaceName: workspace.workspaceName
            }
        });
    }

    return (
        <>  
            <div>
                <h3 className='App-heading'>Dashboard</h3>
                <div className='dashboard-container'>
                    <WorkspaceList viewWorkspaceDetails={viewDetails} />
                </div>
            </div>

        </>
    )
}
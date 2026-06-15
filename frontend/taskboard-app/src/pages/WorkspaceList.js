import { useEffect, useState } from "react";
import { getWorkspace, deleteWorkspace } from "../api/workspaceAPI";
import WorkspaceModal from "./WorkspaceModal";
import Modal from '../components/Modal';


export default function WorkspaceList({ viewWorkspaceDetails }) {
    const [workspaceList, setWorkspaceList] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalTitle, setModalTitle] = useState("");
    const [workspaceId, setWorkspaceId] = useState(0);
    const [loading, setLoading] = useState(false);



    useEffect(() => {
        loadWorkspace();
    }, [])
    function loadWorkspace() {
        setLoading(true);
        getWorkspace()
            .then((res) => {
                setWorkspaceList(res.data.data);
                console.log("Response", res.data.data);
            })
            .catch((error) => {
                console.log(error)
                if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
            })
            setLoading(false);
    }

    function createWorkspace() {
        setShowModal(true);
        setModalTitle("Create Workspace");
    }

    function handleDeleteWorkspace(workspaceId) {
        let confirmAnswer = window.confirm("Are you sure you want to delete this Workspace? All the projects tagged to this workspace will also be deleted.Please Confirm")
        if (!confirmAnswer)
            return;
        deleteWorkspace(workspaceId)
            .then((res) => {
                console.log(res.data.data)
                setWorkspaceList(prev => prev.filter(ws => ws.workspaceId !== workspaceId))
            }
            )
            .catch((error) => {
                console.log(error)
                if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
            })

    }

    function handleView(workspaceId) {
        viewWorkspaceDetails(workspaceId);
    }
    function handleEdit(Id) {
        setWorkspaceId(Id)
        setModalTitle("Create Workspace");
        setShowModal(true);
    }
    const closeModal = () => {
        loadWorkspace();
        setShowModal(false);
    }
    const cancelModal = () => {
        setShowModal(false);
    }
    return (
        <>
            {loading && (
                <div className="overlay">
                    <div className="spinner"></div>
                </div>
            )}
            <div className="workspace-page">
                <div className="workspace-header">
                    <button onClick={createWorkspace}> + Create</button>
                </div>
                <div>
                    {workspaceList.length === 0 && <p>No workspaces available for this user.</p>}
                </div>
                <div className="workspace-container">
                    {workspaceList.map((ws) => (

                        <div className="workspace-card" key={ws.workspaceId}>
                            <div className="workspace-info">
                                <h3>{ws.workspaceName}</h3>
                                <p>{ws.description}</p>
                            </div>
                            <div className="workspace-stats">

                                <div className="stat-item">
                                    <span className="stat-label">Projects</span>
                                    <span className="stat-value">
                                        {ws.projectCount}
                                    </span>
                                </div>

                            </div>
                            <div className="workspace-actions">
                                <button onClick={() => handleView(ws)}>View</button>
                                <button onClick={() => handleEdit(ws.workspaceId)}>Edit</button>
                                <button onClick={() => handleDeleteWorkspace(ws.workspaceId)}>Delete</button>
                            </div>

                        </div>
                    ))}
                </div>
            </div>
            {showModal &&
                <Modal isOpen={showModal} title={modalTitle} onClose={closeModal}>
                    <WorkspaceModal onSuccess={closeModal} onCancel={cancelModal} workspaceId={workspaceId} />
                </Modal>

            }

        </>
    )
}

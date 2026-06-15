import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { deleteProject, getProjectbyWorkspace } from '../api/projectAPI';
import Modal from '../components/Modal';
import ProjectModal from './ProjectModal';

export default function ProjectList() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [projectDetails, setprojectDetails] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [projectId, setProjectId] = useState(0);
    const [modalTitle, setModalTitle] = useState("");
    const [deleteReq, setDeleteReq] = useState({
        projectId: 0,
        workspaceId: parseInt(id)
    })

    useEffect(() => {
        console.log(id);
        loadProjectList();
    }, [])

    const location = useLocation();
    const workspaceName = location.state?.workspaceName;
    const loadProjectList = () => {
        getProjectbyWorkspace(id)
            .then((res) => {
                console.log(res.data.data)
                setprojectDetails(res.data.data);
            }
            )
            .catch((error) => {
                 if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
            })
    }

    function createProject() {
        setModalTitle("Create Project")
        setShowModal(true);
    }

    const handleView = (proj) => {
        navigate(`/task/${proj.projectId}`, {
            state: {
                projectTitle: proj.projectTitle,
                workspaceName: workspaceName,
                workspaceId:id
            }
        });
    }

    const handleEdit = (projectId) => {
        console.log("Project Id", projectId);
        setModalTitle("Edit Project")
        setProjectId(projectId);
        setShowModal(true);
    }
    async function handleDeleteProject(projId) {
        setDeleteReq({ ...deleteReq, projectId: parseInt(projId) });
        console.log(projId);
        let answer = window.confirm("Are you sure you want to delete this project ?")
        if (!answer)
            return
        try {
            const res = await deleteProject(deleteReq);
            if (res.data.success) {
                loadProjectList();
                window.alert(res.data.description);
            }
        }
        catch (error) {
             if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
        }

    }
    function closeModal() {
        loadProjectList();
        setShowModal(false);
        setProjectId(0);
        setModalTitle("");
    }
    function cancelModal() {
        setShowModal(false);
        setProjectId(0);
        setModalTitle("");
    }
    function handleBack()
    {
        navigate('/dashboard');
    }
    return (
        <>

            <div className="project-page">
                <div className='project-header'>
                    <button onClick={handleBack}> Back</button>
                    <h4>Workspace : {workspaceName}</h4>
                    <button onClick={createProject}>Create</button>
                </div>
                 <h5>Project</h5>
                <div>
                    {projectDetails.length === 0 && <p>No project available for this workspace.</p>}
                </div>
                <div className='project-container'>
                    {projectDetails.map((proj) => (

                        <div className="project-card" key={proj.projectId}>
                            <h5>{proj.projectTitle}</h5>

                            <p>{proj.description}</p>
                            <div className='project-stats'>

                            </div>
                            <div className='project-actions'>
                                <button onClick={() => handleView(proj)}>View</button>
                                <button onClick={() => handleEdit(proj.projectId)}>Edit</button>
                                <button onClick={() => handleDeleteProject(proj.projectId)}>Delete</button>
                            </div>

                        </div>
                    ))}


                </div>


            </div>
            <Modal isOpen={showModal} title={modalTitle} onClose={closeModal}>
                <ProjectModal workspaceId={id} projectId={projectId} onSuccess={closeModal} onCancel={cancelModal} />
            </Modal>
        </>
    )
}

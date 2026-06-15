import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { deletTask, getTasksbyProject } from '../api/taskAPI';
import Modal from '../components/Modal';
import TaskModal from './TaskModal';
import { formatDate } from '../utils/dateFormatter';

export default function TaskList() {
    const { projectId } = useParams();
    const [taskDetails, setTaskDetails] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalTitle, setModalTitle] = useState("");
    const [taskId, setTaskId] = useState(0);

    useEffect(() => {
        loadTask();
    }, [])
    const location = useLocation();
    const navigate=useNavigate();

    const projectTitle = location.state?.projectTitle;
    const workspaceName = location.state?.workspaceName;
    const workspaceId =parseInt(location.state?.workspaceId);

    const loadTask = () => {
        getTasksbyProject(projectId)
            .then((res) => {
                console.log(res.data.data);
                setTaskDetails(res.data.data);
            })
            .catch((error) => {
                console.log("Error", error)
                 if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
            })
    }

    const handleDeleteTask = async (taskId) => {
        try {
            if (window.confirm("Are you sure you want to delete this task?")) {
                const res = await deletTask(taskId);
                if (res.data.success) {
                    loadTask();
                    window.alert(res.data.description);
                }
            }

        }
        catch (error) {
            console.log(error);
             if (error.response && error.response.data.description) {
                    window.alert(error.response.data.description)
                }
                else {
                    window.alert("An error occured.Please try again after sometime");
                }
        }

    }
    function createTask() {
        setModalTitle("Create task");
        setShowModal(true);

    }
    function handleEditTask(Id) {
        setTaskId(Id);
        setModalTitle("Edit task");
        setShowModal(true);

    }
    function closeModal() {
        setShowModal(false);
        setModalTitle("");
        setTaskId(0);
        loadTask();
    }
    function cancelModal() {
        setShowModal(false);
        setModalTitle("");
        setTaskId(0);
    }
    function handleBack()
    {
        debugger;
         navigate(`/Project/${workspaceId}`,{
            state:{
                workspaceName:workspaceName
            }
        });
    }
    return (
        <>
            <div className="task-page">
                <div className='task-page-header'>
                    <button onClick={handleBack}>Back</button>
                    <h4>Project : {projectTitle}</h4>
                    <button onClick={createTask}>Create</button>
                </div>
                <h5>List Of Tasks</h5>
                <div>
                    {taskDetails.length === 0 && <p>No tasks available for this project.</p>}
                </div>
                {taskDetails.length > 0 && <div className="task-list">
                    <div className="task-header-row">
                        <span>Task</span>
                        <span>Priority</span>
                        <span>Due Date</span>
                        <span>Status</span>
                        <span>Actions</span>
                    </div>
                    {taskDetails.map((task) => (

                        <div className='task-row' key={task.taskId}>
                            <span>{task.taskName}</span>
                            <span>TBD</span>
                            <span>{formatDate(task.dueDate)}</span>
                            <span>{task.status}</span>

                            <div className='task-actions'>
                                <button onClick={() => handleEditTask(task.taskId)}>Edit</button>
                                <button onClick={() => handleDeleteTask(task.taskId)}>Delete</button>
                            </div>
                        </div>

                    ))}
                </div>}
            </div>
            <Modal isOpen={showModal} onClose={closeModal} title={modalTitle}>
                <TaskModal onSuccess={closeModal} taskId={taskId} onCancel={cancelModal}></TaskModal>
            </Modal>
        </>
    )
}

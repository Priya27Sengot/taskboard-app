import { useState, useEffect } from "react";
import { createTask, getTaskByTaskId, getTaskDropdownValues, updateTask } from "../api/taskAPI";
import { formatDate, formatForHtmlDate } from "../utils/dateFormatter";

export default function TaskModal({ onSuccess, taskId, onCancel }) {
    const [taskReq, setTaskReq] = useState({
        taskName: "",
        projectId: 0,
        assignedToUser: 0,
        status: 1,
        dueDate: ""
    })
    const [projectDD, setProjectDD] = useState([]);
    const [userDD, setUserDD] = useState([]);
    const [statusDD, setStatusDD] = useState([]);
    const [errors, setErrors] = useState({});
    const [Id, setId] = useState(taskId);


    useEffect(() => {
        loadDropDownValues();
        loadTaskDetail();
    }, [])
    async function loadDropDownValues() {
        try {
            const res = await getTaskDropdownValues();
            if (res.data.success) {
                console.log(res.data.data);
                const data = res.data.data;
                setProjectDD(data.projects);
                setStatusDD(data.status);
                setUserDD(data.users);
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
    async function loadTaskDetail() {
        try {
            console.log(Id, taskId);
            if (Id === 0)
                return;
            const res = await getTaskByTaskId(Id);
              console.log("Task Details",res.data.data);
              console.log("Task Due date",formatDate(res.data.data.dueDate));
            res.data.data.dueDate = formatForHtmlDate(res.data.data.dueDate)
          
            if (res.data.success) {
                setTaskReq(res.data.data);
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
    const validate = () => {
        let err = {};
        let today = new Date();
        let dueDate = new Date(taskReq.dueDate);
        if (!taskReq.taskName.trim()) {
            err.taskName = "Task Name is required";
        }
        else if (taskReq.taskName.trim().length > 250) {
            err.taskName = "Task Name should be less than 250.";
        }
        if (taskReq.projectId === 0) {
            err.projectId = "Please select project";
        }
        if (taskReq.assignedToUser === 0) {
            err.assignedToUser = "Please select assigned user";
        }
        if (!taskReq.dueDate.trim()) {
            err.dueDate = "Please select Due Date";
        }
        else if (dueDate <= today) {
            err.dueDate = "Due date should be greater than today's date";
        }

        setErrors(err);
        return err;
    }
    const handleSubmit = async (e) => {
        console.log(taskReq);
        e.preventDefault();
        let validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            return;
        }
        try {
            if (Id === 0) {
                const res = await createTask(taskReq);
             
                if (res.data.success) {
                    window.alert(res.data.description);
                }
            }
            else {
                const res = await updateTask(taskReq);
                if (res.data.success) {
                    window.alert(res.data.description);
                }
            }
        }
        catch (err) {
            console.log(err);
            if (err.response && err.response.data.description) {
                window.alert(err.response.data.description)
            }
            else {
                window.alert("An error occured.Please try again after sometime");
            }
        }
        setTaskReq({
            taskName: "",
            projectId: "",
            assignedToUser: 0,
            status: 1,
            dueDate: ""
        })
        onSuccess();
    }
    function handleCancel() {
        onCancel();
    }
    return (
        <>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    {errors.taskName && (
                        <small className="error">{errors.taskName}</small>
                    )}
                    <label htmlFor="taskName">Name</label>
                    <input id="taskName" placeholder="Task Name" value={taskReq.taskName} onChange={(e) => setTaskReq({ ...taskReq, taskName: e.target.value })}  >
                    </input>
                </div>
                <div className="form-group">
                    {errors.projectId && (
                        <small className="error">{errors.projectId}</small>
                    )}
                    <label htmlFor="projectId">Project</label>
                    <select id="projectId" value={taskReq.projectId} onChange={(e) => {
                        console.log("ProjectId", e.target.value)
                        setTaskReq({ ...taskReq, projectId: parseInt(e.target.value) })
                    }}>
                        <option value="0">--Select Project--</option>
                        {projectDD.map((prj) =>
                            <option key={prj.projectId} value={prj.projectId}>{prj.projectTitle}
                            </option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    {errors.assignedToUser && (
                        <small className="error">{errors.assignedToUser}</small>
                    )}
                    <label htmlFor="assignedToUser">Assign to User</label>
                    <select id="assignedToUser" value={taskReq.assignedToUser} onChange={(e) => setTaskReq({ ...taskReq, assignedToUser: parseInt(e.target.value) })}>
                        <option value="0">--Select User--</option>
                        {userDD.map((user) =>
                            <option key={user.userId} value={user.userId}>
                                {user.userName}
                            </option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="status">Status</label>
                    <select id="status" value={taskReq.status} onChange={(e) => setTaskReq({ ...taskReq, status: parseInt(e.target.value) })}>
                        {statusDD.map((s) =>
                            <option key={s.id} value={s.id}>{s.description}</option>
                        )}
                    </select>
                </div>
                <div className="form-group">
                    {errors.dueDate && (
                        <small className="error">{errors.dueDate}</small>
                    )}
                    <label htmlFor="dueDate">Due Date</label>
                    <input id="dueDate" type="date" placeholder="Due Date" value={taskReq.dueDate} onChange={(e) => setTaskReq({ ...taskReq, dueDate: e.target.value })}  >
                    </input>
                </div>
                <div className="modal-actions">
                    <button type="submit">{Id === 0 ? "Create" : "Edit"}</button>
                    <button type="button" onClick={handleCancel}>Cancel</button>
                </div>

            </form>
        </>
    )
}
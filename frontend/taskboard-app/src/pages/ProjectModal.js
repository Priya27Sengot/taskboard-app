import { useEffect, useState } from "react"
import { createProject, getProjectById, updateProject } from "../api/projectAPI";
import Alert from "../layouts/AlertModal";

export default function ProjectModal({ workspaceId, projectId, onSuccess, onCancel }) {

    const [createprojReq, setCreateProjReq] = useState({
        projectId: 0,
        projectTitle: "",
        description: "",
        workspaceId: parseInt(workspaceId),
        isActive: true
    })
    const [projId, setProjId] = useState(projectId);
    const [showAlert, setShowAlert] = useState(false);
    const [message, setMessage] = useState("");
    const [errors, setErrors] = useState({});

    useEffect(() => {
        loadProjectDetails();
    }, [])
    const loadProjectDetails = async () => {
        try {
            if (projId === 0)
                return;
            const res = await getProjectById(projId)
            if (res.data.success) {
                console.log("Project", res.data.data)
                setCreateProjReq(res.data.data);
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
        if (!createprojReq.projectTitle.trim()) {
            err.projectTitle = "Project Title is required";
        }
        else if (createprojReq.projectTitle.trim().length > 1000) {
            err.projectTitle = "Project Title should be less than 1000 characters";
        }
        if (!createprojReq.description.trim()) {
            err.description = "Description is required";
        }
        else if (createprojReq.description.trim().length > 5000) {
            err.description = "Description should be less than 5000 characters";
        }
        setErrors(err);
        return err;
    }
    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("Request", createprojReq);
        let validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            return;
        }
        try {
            if (projId === 0) {
                const res = await createProject(createprojReq);
                window.alert(res.data.description);
                onSuccess();
            }
            else {
                const res = await updateProject(createprojReq);
                console.log(res.data.data);
                window.alert(res.data.description);
                onSuccess();
            }

        }
        catch (err) {
            if (err.response && err.response.data.description) {
                window.alert(err.response.data.description)
            }
            else {
                window.alert("An error occured.Please try again after sometime");
            }
        }

    }
    const handleCancel = () => {
        onCancel();
    }

    return (<>
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                {errors.projectTitle && (
                    <small className="error">{errors.projectTitle}</small>
                )}
                <label htmlFor="projectTitle">Name</label>
                <input id="projectTitle" placeholder="Project Title" value={createprojReq.projectTitle} onChange={(e) => setCreateProjReq({ ...createprojReq, projectTitle: e.target.value })}  >
                </input>
            </div>
            <div className="form-group">
                {errors.description && (
                    <small className="error">{errors.description}</small>
                )}
                <label htmlFor="description">Description</label>
                <textarea id="description" placeholder="Description" value={createprojReq.description} onChange={(e) => setCreateProjReq({ ...createprojReq, description: e.target.value })}  >
                </textarea>
            </div>
            <div className="modal-actions">
                <button type="submit">{projId === 0 ? "Create" : "Edit"}</button>
                <button type="button" onClick={handleCancel}>Cancel</button>
            </div>

        </form>

        <Alert
            isOpen={showAlert}
            title="Success"
            message="Workspace created successfully."
            onClose={() => setShowAlert(false)}
        />
    </>)
}
import { useEffect, useState } from "react"
import { createWorkspace, getWorkspaceById, updateWorkspace } from "../api/workspaceAPI";
import Alert from "../layouts/AlertModal";
import { updateProject } from "../api/projectAPI";

export default function WorkspaceModal({ onSuccess, onCancel,workspaceId }) {
    const [createWorkspacereq, setCreateWorkspaceReq] = useState({
        workspaceName: "",
        description: ""
    })
    const [wsId,setwsId]=useState(workspaceId);
    useEffect(()=>{
        loadWorkspace();
    },[])
     const loadWorkspace= async()=>{
        if(wsId === 0)
        {
            return;
        }
        const res=await getWorkspaceById(wsId);
        if(res.data.success)
        {
            console.log(res.data.data);
 setCreateWorkspaceReq(res.data.data);
        }
       

     }
    const [showAlert, setShowAlert] = useState(false);
    const [errors, setErrors] = useState({});

    const validate = () => {
        let err = {};
        if (!createWorkspacereq.workspaceName.trim()) {
            err.workspaceName = "Workspace name is required";
        }
        else if(createWorkspacereq.workspaceName.trim().length >500)
        {
            err.workspaceName="Workspace Name should be less than 500 characters"
        }

        if (!createWorkspacereq.description.trim()) {
            err.description = "Description is required";
        }
        else if(createWorkspacereq.description.trim().length >1000)
        {
            err.description="Description should be less than 1000 characters"
        }
        setErrors(err);
        return err;
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("Request", createWorkspacereq);
        const validationErrors = validate();
        try {
            if (Object.keys(validationErrors).length == 0) {
                if(wsId === 0)
                {
                   const res = await createWorkspace(createWorkspacereq);
                if (res.data.success) {                   
                    window.alert("Workspace created successfully");
                    setShowAlert(true);
                } 
                }
                else{
                const res = await updateWorkspace(createWorkspacereq);
                if (res.data.success) {                   
                    window.alert("Workspace updated successfully");
                    
                } 
                }
                
                onSuccess();
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

    }

    const handleCancel = () => {
        onCancel();
    }

    return (
        <>

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    {errors.workspaceName && (
                        <small className="error">{errors.workspaceName}</small>
                    )}
                    <label htmlFor="workspaceName">Name</label>
                    <input id="workspaceName" placeholder="Workspace Name" value={createWorkspacereq.workspaceName} onChange={(e) => setCreateWorkspaceReq({ ...createWorkspacereq, workspaceName: e.target.value })}  >
                    </input>

                </div>
                <div className="form-group">
                    {errors.description && (
                        <small className="error">{errors.description}</small>
                    )}
                    <label htmlFor="description">Description</label>
                    <textarea id="description" placeholder="Description" value={createWorkspacereq.description} onChange={(e) => setCreateWorkspaceReq({ ...createWorkspacereq, description: e.target.value })}  >

                    </textarea>
                </div>
                <div className="modal-actions">
                    <button type="submit">{wsId === 0 ? "Create":"Edit"}</button>
                    <button type="button" onClick={handleCancel}>Cancel</button>
                </div>

            </form>

            <Alert
                isOpen={showAlert}
                title="Success"
                message="Workspace created successfully."
                onClose={() => setShowAlert(false)}
            />
        </>

    )
}
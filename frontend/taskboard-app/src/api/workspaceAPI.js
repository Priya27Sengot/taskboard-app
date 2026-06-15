import api from "./axiosInstance";

export const getWorkspace= async ()=>{   
return await api.get(`/Workspace`);
}

export const createWorkspace = async (newWorkspace)=>{
    return await api.post(`/Workspace`,newWorkspace);
}

export const deleteWorkspace = async(Id) =>{
    return await api.delete(`/Workspace/${Id}`)
}

export const getWorkspaceById= async (workspaceId)=>{  
return await api.get(`/Workspace/${workspaceId}`);
}

export const updateWorkspace = async(workspace)=>{
    return await api.put("/Workspace",workspace)
}
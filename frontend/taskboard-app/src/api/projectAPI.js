import api from './axiosInstance';

export const getProjectbyWorkspace=async(workspaceId)=>{
    return await api.get(`/Project/workspace/${workspaceId}`);
}

export const createProject = async(newproject)=>{
    return await api.post("/Project",newproject);
}

export const deleteProject=async (deletePrj)=>{
    console.log(deletePrj)
    return await api.delete("/Project",{data:deletePrj});
}

export const updateProject = async(updatePrj)=>{
    return await api.put("/Project",updatePrj)
}
export const getProjectById = async(projectId)=>{    
    return await api.get(`/Project/${projectId}`);
}
import api from './axiosInstance';

export const getTasksbyProject = async(projectId) =>{
    return await api.get(`/Task/${projectId}`);
}

export const createTask = async(taskObj)=>{
    return await api.post(`/Task`,taskObj)
}

export const deletTask = async(taskId)=>{
    return await api.delete(`/Task/${taskId}`);
}

export const updateTask=async(taskObj)=>{
    return await api.put(`/Task`,taskObj)
}

export const getTaskDropdownValues=async()=>{
    return await api.get(`/Task`);
}

export const getTaskByTaskId = async(taskId)=>{
    return await api.get(`/Task/taskdetails/${taskId}`);
}
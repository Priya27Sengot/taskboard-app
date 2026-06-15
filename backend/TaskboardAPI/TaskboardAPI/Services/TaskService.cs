using TaskboardAPI.Models;
using TaskboardAPI.Repositories;


namespace TaskboardAPI.Services
{
    public class TaskService
    {

        private readonly TaskRepository _taskRepo;

        public TaskService(TaskRepository taskRepo) {
            _taskRepo = taskRepo;
        }

        public async Task<APIResponse> CreateTask(CreateTaskRequest createTask,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {               
                var project = await _taskRepo.ValidateProject(createTask.ProjectId, userId);
                if (project == null)
                {
                   response.Success = false;
                   response.Description = "Project does not exist in User's Workspace";
                   return response;
                }
                else
                {                    
                    var task = new TaskItem
                    {
                        TaskName = createTask.TaskName,
                        ProjectId = createTask.ProjectId,
                        AssignedToUser = createTask.AssignedToUser,
                        IsActive = createTask.IsActive,
                        Status = createTask.Status,
                        DueDate = createTask.DueDate,
                        CreatedBy = userId
                    };
                    var result =await _taskRepo.CreateTask(task);
                    if (result > 0)
                    {
                        response.Success = true;
                        response.Description = "Task created successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Description = "Task creation failed";
                    }

                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
            
        }

        public async Task<APIResponse> GetTaskByProject(int projectId, int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var project = await _taskRepo.ValidateProject(projectId, userId);
                if (project == null)
                {
                    response.Success = false;
                    response.Description = "Project does not exist in User's Workspace";
                    return response;
                }
                else
                {
                    var taskList = await _taskRepo.GetTasksByProject(projectId);
                   if(taskList!=null)
                    {
                        response.Success = true;
                        response.Data =taskList.ToList();
                    }
                    else
                    {
                        response.Success = false;
                        response.Description = "No tasks available for particular project";
                    }

                }
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
   
        }

        public async Task<APIResponse> DeleteTask(int taskId, int userId) 
        {
            APIResponse response = new APIResponse();
            try
            {               
                var task = await _taskRepo.GetTaskById(taskId);
                if (task.ProjectId != 0)
                {
                    var project = _taskRepo.ValidateProject(task.ProjectId, userId);
                    if (project == null)
                    {
                        response.Success = false;
                        response.Description = "Project does not exist in User's Workspace";
                        return response;
                    }
                    else
                    {
                        var result = await _taskRepo.DeleteTask(taskId, task.ProjectId, userId);
                        if (result > 0)
                        {
                            response.Success = true;
                            response.Description = "Task deleted successfully";
                        }
                        else
                        {
                            response.Success = false;
                            response.Description = "Task deletion failed";
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;

           
        }

        public async Task<APIResponse> UpdateTask(TaskItem updateTask, int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var project = await _taskRepo.ValidateProject(updateTask.ProjectId, userId);
                if (project == null)
                {
                    response.Success = false;
                    response.Description = "Project does not exist in User's Workspace";
                    return response;
                }
                else
                {
                    var result = await _taskRepo.UpdateTask(updateTask, userId);
                    if (result > 0)
                    {
                        response.Success = true;
                        response.Description = "Task updated successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Description = "Task updation failed";
                    }
                }
              
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;

         
        }
        public async Task<APIResponse> GetTaskDropdownValues(int userId)
        {
            APIResponse response = new APIResponse();
            TaskDropdowns dropdowns = new TaskDropdowns();
            try
            {
                var projectList = await _taskRepo.GetProjectsByUserId(userId);
                if (projectList.Count() > 0)
                {
                    dropdowns.projects= projectList;
                }
                var membersList = await _taskRepo.GetOtherUsers(userId);
                if(membersList.Count() > 0)
                {
                    dropdowns.users = membersList;
                }
                var status = await _taskRepo.GetStatus();
                if (status.Count() > 0)
                {
                    dropdowns.status = status;

                }
                response.Success = true;
                response.Data = dropdowns;
               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;


        }
        public async Task<APIResponse> GetTaskById(int taskId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var task = await _taskRepo.GetTaskById(taskId);
                    if (task != null)
                    {
                        response.Success = true;
                        response.Data = task;
                    }
                    else
                    {
                        response.Success = false;
                        response.Description = "No tasks available for particular project";
                    }

                

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;

        }
    }
}

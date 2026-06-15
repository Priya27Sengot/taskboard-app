using TaskboardAPI.Models;
using TaskboardAPI.Repositories;

namespace TaskboardAPI.Services
{
    public class ProjectService
    {
        private readonly ProjectRepositroy _repo;
        private readonly WorkspaceService _workspaceService;

        public ProjectService(ProjectRepositroy repo, WorkspaceService workspaceService)
        {
            _repo = repo;
            _workspaceService = workspaceService;
        }

        public async Task<APIResponse> CreateProject(CreateProjectRequest newProj,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if(newProj.WorkspaceId == 0 || userId ==0)
                {
                    response.Success = false;
                    response.Description = "Workspace or User does not exist.Please Check";
                    return response;
                }             
                var ws = await _workspaceService.GetWorkspaceById(newProj.WorkspaceId, userId);

                if (ws.Data == null)
                {
                    response.Success= false;
                    response.Description = "Workspace not found";
                    return response;
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(newProj.ProjectTitle) || string.IsNullOrWhiteSpace(newProj.Description) || newProj.WorkspaceId == 0)
                    {
                        response.Success = false;
                        response.Description = "Input values cannot be empty or null";
                        return response;
                    }
                    else
                    {
                        var project = new Project
                        {
                            ProjectTitle = newProj.ProjectTitle,
                            Description = newProj.Description,
                            WorkspaceId = newProj.WorkspaceId,
                            IsActive = true,
                            CreatedBy = userId,
                        };

                        var result = await _repo.CreateProject(project);
                        if (result > 0)
                        {
                            response.Success = true;
                            response.Description = "Project created successfully";
                        }
                        else
                        {
                            response.Success = false;
                            response.Description = "Project creation failed";
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

        public async Task<APIResponse> GetProjectsByWorkspace(int workspaceId,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (workspaceId == 0 || userId == 0)
                {

                    response.Success = false;
                    response.Description = "Input values cannot be empty or null";
                    return response;
                }
                var ws = await _workspaceService.GetWorkspaceById(workspaceId, userId);

                if (ws == null)
                {
                    response.Success = false;
                    response.Description = "Workspcae does not exist";
                    return response;
                }
                else
                {

                    var projectList = await _repo.GetProjectByWorkspace(workspaceId);
                    response.Success = true;
                    response.Data = projectList.ToList();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
           
        }

        public async Task<APIResponse> DeleteProject(int projectId,int workspaceId,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ws = await _workspaceService.GetWorkspaceById(workspaceId, userId);

                if (ws == null)
                {
                    response.Success = false;
                    response.Description = "Workspcae does not exist";
                    return response;
                }
                var result = await _repo.DeleteProject(projectId, workspaceId, userId);

                if (result > 0)
                {
                    response.Success = true;
                    response.Description = "Project deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Description = "Project deletion failed";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
           
        }

        public async Task<APIResponse> UpdateProject(Project updatePrj, int workspaceId,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ws = await _workspaceService.GetWorkspaceById(workspaceId, userId);

                if (ws == null)
                {

                    response.Success = false;
                    response.Description = "Workspcae does not exist";
                    return response;
                }

                updatePrj.ModifiedBy = userId;
                var result = await _repo.UpdateProject(updatePrj);

                if (result > 0)
                {
                    response.Success = true;
                    response.Description = "Project updated successfully";
                }
                else
                {
                    response.Success = false;
                    response.Description = "Project updation failed";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;

        }
        public async Task<APIResponse> GetProjectById(int projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (projectId == 0 )
                {

                    response.Success = false;
                    response.Description = "Input values cannot be empty or null";
                    return response;
                }              

                    var project = await _repo.GetProjectById(projectId);
                    response.Success = true;
                    response.Data = project;
                
             
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

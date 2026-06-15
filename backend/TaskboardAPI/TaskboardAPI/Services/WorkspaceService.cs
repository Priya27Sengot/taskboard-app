using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;
using TaskboardAPI.Models;
using TaskboardAPI.Repositories;

namespace TaskboardAPI.Services
{
    public class WorkspaceService
    {
        private readonly WorkspaceRepository _workspaceRepo;     
        public WorkspaceService(WorkspaceRepository workspaceRepo)
        {
            _workspaceRepo = workspaceRepo;          
        }
        public async Task<APIResponse> CreateWorkspace(string workspaceName, string description,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if(string.IsNullOrWhiteSpace(workspaceName) || string.IsNullOrWhiteSpace(description) || userId ==0)
                {
                    response.Success = false;
                    response.Description = "Input values cannot be null";
                    return response;
                }
                else
                {
                    var workspace = new Workspace
                    {
                        WorkspaceName = workspaceName,
                        Description = description,
                        UserId = userId,
                        CreatedBy = userId
                    };
                    var result = await _workspaceRepo.CreateWorkspace(workspace);

                    if(result>0)
                    {
                        response.Success = true;
                        response.Description = "Workspace created successfully";                       
                    }
                    else
                    {
                        response.Success= false;
                        response.Description = "Workspace creation failed";
                    }
                }
           
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }          

            return response;
        }

        public async Task<APIResponse> GetWorkspaceByUser(int userId) 
        {
            APIResponse response = new APIResponse();
            try
            {
                if(userId !=0)
                {
                    var workspaces = await _workspaceRepo.GetWorkspaceByUserId(userId);
                    if (workspaces == null)
                    {
                        response.Success = false;
                        response.Description = "No workspace available for the user";
                        return response;
                    }
                    else
                    {
                        response.Success= true;
                        response.Data = workspaces.ToList();                       
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

        public async Task<APIResponse> DeleteWorkspace(int workspaceId,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var result = await _workspaceRepo.DeleteWorkspace(workspaceId, userId);
                if (result > 0)
                {
                    response.Success = true;
                    response.Description = "Workspace deleted successfully";                   
                }
                else
                {
                    response.Success = false;
                    response.Description = "No workspaces available to delete";
                }               
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
          
        }

        public async Task<APIResponse> GetWorkspaceById(int Id,int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var workspace = await _workspaceRepo.GetWorkSpaceById(Id, userId);
               if(workspace !=null)
                {
                    response.Success = true;
                    response.Data = workspace;
                }
                else
                {
                    response.Success= false;
                    response.Description = "Workspace does not exist";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Description = ex.Message;
            }
            return response;
           
        }
        public async Task<APIResponse> UpdateWorkspace(Workspace updateObj, int userId)
        {
            APIResponse response = new APIResponse();
            try
            {

                updateObj.ModifiedBy = userId;
                var result = await _workspaceRepo.UpdateWorkspace(updateObj);

                if (result > 0)
                {
                    response.Success = true;
                    response.Description = "Workspace updated successfully";
                }
                else
                {
                    response.Success = false;
                    response.Description = "Workspace updation failed";
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskboardAPI.Models;
using TaskboardAPI.Services;

namespace TaskboardAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceService _workspaceService;        

        public WorkspaceController(WorkspaceService workspaceService) { 
            _workspaceService = workspaceService;
        }
      

        [HttpPost]
        public async Task<ActionResult> CreateWorkspace(CreateWorkspace newWorkspace)
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _workspaceService.CreateWorkspace(newWorkspace.WorkspaceName, newWorkspace.Description,userId);
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> GetWorkspace()
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _workspaceService.GetWorkspaceByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteWorkspace(int Id)
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _workspaceService.DeleteWorkspace(Id,userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpGet("{workspaceId}")]
        public async Task<ActionResult> GetWorkspaceById(int workspaceId)
        {
            var userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _workspaceService.GetWorkspaceById(workspaceId,userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateWorkspace(Workspace workspace)
        {
            int userid = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _workspaceService.UpdateWorkspace(workspace, userid);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
    }
}

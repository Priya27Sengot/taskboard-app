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
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(CreateProjectRequest newProject)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _projectService.CreateProject(newProject, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpGet("workspace/{workspaceId}")]
        public async Task<ActionResult> GetProjectByWorkspace(int workspaceId)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _projectService.GetProjectsByWorkspace(workspaceId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject([FromBody]DeleteProjectRequest deletePrj)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _projectService.DeleteProject(deletePrj.ProjectId,deletePrj.WorkspaceId, userId);
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
        public async Task<ActionResult> UpdateProject(Project prj)
        {
            int userid = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _projectService.UpdateProject(prj, prj.WorkspaceId, userid);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
        [HttpGet("{projectId}")]
        public async Task<ActionResult> GetProjectById(int projectId)
        {
            int userid = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _projectService.GetProjectById(projectId);
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

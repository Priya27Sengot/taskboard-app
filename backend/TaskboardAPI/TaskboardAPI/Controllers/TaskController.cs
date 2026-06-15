using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskboardAPI.Models;
using TaskboardAPI.Services;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;

namespace TaskboardAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(CreateTaskRequest taskObj)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.CreateTask(taskObj, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("{projectid}")]
        public async Task<ActionResult> GetTaskByProject(int projectid)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.GetTaskByProject(projectid, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeletTask(int taskId)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.DeleteTask(taskId, userId);
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
        public async Task<ActionResult> UpdateTask(TaskItem task)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.UpdateTask(task, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTaskDropdownValues()
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.GetTaskDropdownValues(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpGet("taskdetails/{taskId}")]
        public async Task<ActionResult> GetTaskByTaskId(int taskId)
        {
            int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _taskService.GetTaskById(taskId);
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

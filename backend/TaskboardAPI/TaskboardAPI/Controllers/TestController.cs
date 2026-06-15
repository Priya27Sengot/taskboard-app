using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskboardAPI.Data;

namespace TaskboardAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DbConnectionFactory _factory;

        public TestController(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public IActionResult TestConnection()
        {
            using var conn =_factory.CreateConnection();
            conn.Open();
            return Ok("DB Connected Successfully");
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class HtppMetodeController : ControllerBase
    {
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}

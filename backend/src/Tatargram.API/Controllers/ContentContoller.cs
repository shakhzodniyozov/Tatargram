using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tatargram.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ContentController : Controller
    {
        private readonly IWebHostEnvironment env;

        public ContentController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet]
        public IActionResult GetImage([FromQuery] string path)
        {
            return PhysicalFile(Path.Combine(env.ContentRootPath, "files" + path), "image/jpeg");
        }
    }
}
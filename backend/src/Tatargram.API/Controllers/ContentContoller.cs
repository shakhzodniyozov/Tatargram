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
            string imagePath = env.WebRootPath + path;
            return PhysicalFile(imagePath, "image/jpeg");
        }
    }
}
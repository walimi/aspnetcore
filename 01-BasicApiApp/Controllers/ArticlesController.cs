using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication.Controllers
{

    [Route("/api/[controller]")]
    public class ArticlesController
    {
        [HttpGet]
        public string Get() => "Hello, from the controller!";
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Articles.Entities;
using System.Linq; 


namespace ConsoleApplication.Controllers
{

    [Route("/api/[controller]")]
    public class ArticlesController : Controller
    {

        private static List<Article> _Articles = new List<Article>(new[] {
          new Article() { Id = 1, Title = "Intro to ASP.NET Core" },
          new Article() { Id = 2, Title = "Docker Fundamentals" },
          new Article() { Id = 3, Title = "Deploying to Azure with Docker" },
        });

        [HttpGet("{id}")]
        public Article Get(int id)
        {
            return _Articles.Single(a => a.Id == id);
        }
    }
}

using System.Collections.Generic;
using Articles.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ConsoleApplication.Infrastructure;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


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

        private readonly ArticlesContext _context;

        public ArticlesController(ArticlesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> Get() => await _context.Set<Article>().ToListAsync();

        [HttpGet("{id}")]
        public Article Get(int id)
        {
            return _Articles.Single(a => a.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }
    }
}
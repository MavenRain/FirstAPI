using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstAPI
{
    [Route("api/[controller]")]
    public class FirstAPIController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        static readonly List<Movie> _movies = new List<Movie>()
        {
            new Movie { ID = 1, Title = "First movie" }
        };

        [HttpGet]
        public IEnumerable<Movie> GetAll() { return _movies; }

        [HttpGet("{id: int}", Name = "GetIDByRoute")]
        public IActionResult GetByID (int ID)
        {
            var movie = _movies.FirstOrDefault(x => x.ID == ID);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(movie);
        }

        [HttpPost]
        public void CreateToDoItem([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                movie.ID = 1 + _movies.Max(x => (int?)x.ID) ?? 0;
                _movies.Add(movie);

                string url = Url.RouteUrl("GetByIdRoute", new { id = movie.ID },
                    Request.Scheme, Request.Host.ToUriComponent());

                Context.Response.StatusCode = 201;
                Context.Response.Headers["Location"] = url;
            }
        }
    }
}

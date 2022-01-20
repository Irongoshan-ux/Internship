using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBTestApp.Models;
using MongoDBTestApp.Services;

namespace MongoDBTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _bookService.Get();
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var s = await _bookService.Get(id);
            if (s == null)
            {
                return NotFound();
            }

            return s;
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] Book s)
        {
            await _bookService.Create(s);
            return CreatedAtRoute("Get", new { id = s.Id.ToString() }, s);

        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(string id, [FromBody] Book su)
        {
            var s = await _bookService.Get(id);
            if (s == null)
            {
                return NotFound();
            }
            su.Id = s.Id;

            await _bookService.Update(id, su);
            return CreatedAtRoute("Get", new { id = su.Id.ToString() }, su);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(string id)
        {
            var s = await _bookService.Get(id);
            if (s == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

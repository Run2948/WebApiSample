using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    public class ShelfController : ApiController
    {
        private readonly List<Book> _list = new List<Book>()
        {
            new Book() { Id = 1,Name = "C语言程序设计", Price = 3.8 },
            new Book() { Id = 2,Name = "C++程序设计", Price = 7.8 },
            new Book() { Id = 3, Name = "Java程序设计", Price = 12.8 },
            new Book() { Id = 4,Name = "C#入门经典", Price = 15.8 }
        };

        // GET: api/Shelf
        public IEnumerable<Book> GetShelfs()
        {
            return _list;
        }

        // GET: api/Shelf/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetShelf(int id)
        {
            Book book = _list.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Shelf/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShelf(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                var model = _list.FirstOrDefault(b => b.Id == id);
                int index = _list.IndexOf(model);
                _list[index] = book;
            }
            catch (Exception)
            {
                if (!ShelfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Shelf
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostShelf(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _list.Add(book);

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Shelf/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteShelf(int id)
        {
            Book book = _list.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _list.Remove(book);

            return Ok(book);
        }

        private bool ShelfExists(int id)
        {
            return _list.Count(e => e.Id == id) > 0;
        }
    }
}

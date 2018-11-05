using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Newtonsoft.Json;
using WebApiSample;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    public class BookController : ApiController
    {
        private SampleDbContext db = new SampleDbContext();

        #region 模板自动生成的Api

        [EnableCors(origins: "http://localhost:53462", headers:"*",methods:"*")]
        // GET: api/Book
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Book/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Book
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Book/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }

        #endregion

        #region 自定义参数查询的Api方法
        // GET: api/Book/5   name = ""
        /// <summary>
        ///  FromUri：url参数
        ///  FromBody：请求参数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IHttpActionResult GetBookInfo([FromUri] int id, [FromBody]string name)
        {
            var model = db.Books.Find(id);
            if (model == null)
                return NotFound();
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        } 
        #endregion

        #region 自定义JSONP跨域调用返回的方法

        // GET api/Book?callback=''
        //public HttpResponseMessage GetBooks(string callback)
        //{
        //    string func = $"{callback}({JsonConvert.SerializeObject(db.Books)})";
        //    return new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(func, Encoding.UTF8, "text/javascript")
        //    };
        //}

        #endregion
    }
}
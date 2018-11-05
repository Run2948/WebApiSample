using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiSample.Models;

namespace WebApiSample
{
    public class SampleDbContext:DbContext
    {
        public SampleDbContext()
            :base("Sample")
        {
            
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
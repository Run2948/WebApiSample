namespace WebApiSample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApiSample.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiSample.SampleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApiSample.SampleDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Books.AddOrUpdate(new Book() { Name = "C++ 程序设计", Price = 7.8 },new Book() { Name = "C# 入门经典", Price = 15.8 });
        }
    }
}

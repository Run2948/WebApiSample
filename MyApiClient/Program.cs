using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyApiClient
{
    class Program
    {
        static string address = "http://127.0.0.1:8081";

        static void Main(string[] args)
        {
            HttpClient client =
                //  new HttpClient(); 
                // 将自定义Handler植入管道
                //new HttpClient(new MyMessageHandler());
                HttpClientFactory.Create(new MyMessageHandler());

            client.BaseAddress = new Uri(address);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            #region Get请求
            HttpResponseMessage resp = client.GetAsync("api/v2/shelf").Result;
            if (resp.IsSuccessStatusCode)
            {
                var book = resp.Content.ReadAsAsync<List<Book>>().Result;
                book.ForEach(b => Console.WriteLine(b.Name));             
            }
            else
                Console.WriteLine(resp.StatusCode + resp.ReasonPhrase);
            #endregion

            #region Post、Put、Delete请求

            //resp = client.PostAsJsonAsync("api/v2/shelf", new Book() { Id = 1, Name = "C++ 程序设计经典版", Price = 20.0 }).Result;

            //resp =client.PutAsJsonAsync("api/v2/shelf", new Book() { Id = 1, Name = "C++ 程序设计经典版", Price = 20.0 }).Result;

            //resp = client.DeleteAsync("api/v2/shelf/1").Result;

            #endregion

            Console.ReadKey();
        }
    }

    public class Book
    {
        /// <summary>
        /// 图书编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 图书名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图书价格
        /// </summary>
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace MyApiServer
{
    class Program
    {
        static string address = "http://127.0.0.1:8081";

        static void Main(string[] args)
        {
            //加载插件
            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "plugins"))
                .Where(f=>f.ToLower().EndsWith(".dll")).ToList();
            files.ForEach(file => Assembly.LoadFile(file));

            //配置主机
            var config = new HttpSelfHostConfiguration(address);
            //配置路由
            config.Routes.MapHttpRoute("Default", "api/{version}/{controller}/{id}", new { version = RouteParameter.Optional, id = RouteParameter.Optional });

            // 自定义api根据需求返回格式
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("format", "json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("format", "xml", "application/xml"));

            //监听HTTP
            var server = new HttpSelfHostServer(config);
            //开启来自客户端的请求
            server.OpenAsync().Wait();
            Console.WriteLine($"服务已经启动 {address} 请按任意键键退出");
            Console.ReadKey();
        }
    }
}

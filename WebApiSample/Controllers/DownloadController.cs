using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using Ionic.Zip;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// WebApi下载文件控制器
    /// </summary>
    [RoutePrefix("download")]
    public class DownloadController : ApiController
    {
        /// <summary>
        /// 单个下载
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("single")]
        public HttpResponseMessage DownloadSingle()
        {
            var response = new HttpResponseMessage();
            //从List集合中获取byte[]
            var bytes = DemoData.File1.Select(x => x + "\n").SelectMany(x => Encoding.UTF8.GetBytes(x)).ToArray();
            try
            {
                var fileName = $"download_single_{DateTime.Now:yyyyMMddHHmmss}.txt";
                var content = new ByteArrayContent(bytes);
                response.Content = content;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(ex.ToString());
            }
            return response;
        }

        /// <summary>
        /// 打包下载
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("zip")]
        public HttpResponseMessage DownloadZip()
        {
            var response = new HttpResponseMessage();
            try
            {
                var zipFileName = $"download_compressed_{DateTime.Now:yyyyMMddHHmmss}.zip";
                var downloadDir = HttpContext.Current.Server.MapPath($"~/downloads/download");
                var archive = $"{downloadDir}/{zipFileName}";
                var temp = HttpContext.Current.Server.MapPath("~/downloads/temp");

                // 清空临时文件夹中的所有临时文件
                Directory.EnumerateFiles(temp).ToList().ForEach(File.Delete);
                ClearDownloadDirectory(downloadDir);
                // 生成新的临时文件
                var counter = 1;
                foreach (var c in DemoData.GetMultiple)
                {
                    var fileName = $"each_file_{counter}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                    if (c.Count <= 0)
                    {
                        continue;
                    }
                    var docPath = $"{temp}/{fileName}";
                    File.WriteAllLines(docPath, c, Encoding.UTF8);
                    counter++;
                }
                Thread.Sleep(500);
                using (var zip = new ZipFile())
                {
                    // Make zip file
                    zip.AddDirectory(temp);
                    zip.Save(archive);
                }
                response.Content = new StreamContent(new FileStream(archive, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = zipFileName };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(ex.ToString());
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        private void ClearDownloadDirectory(string directory)
        {
            var files = Directory.GetFiles(directory);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiSample.Helpers;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FilesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //Get : api/Files
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(IO_Helper.RootPath_MVC + @"Files/Files");
            FileInfo foundFileInfo = directoryInfo.GetFiles().FirstOrDefault(x => x.Name == fileName);
            if (foundFileInfo != null)
            {
                FileStream fs = new FileStream(foundFileInfo.FullName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(fs) };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = foundFileInfo.Name
                };
            }
            else
            {
                result = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //POST : api/Files
        public async Task<IHttpActionResult> Post()
        {
            //get server root physical path
            string root = IO_Helper.RootPath_MVC;
            //new folder
            string newRoot = root + @"Files/Files/";
            //check path is exist if not create it
            IO_Helper.CreateDirectoryIfNotExist(newRoot);
            List<string> fileNameList = new List<string>();
            StringBuilder sb = new StringBuilder();
            long fileTotalSize = 0;
            int fileIndex = 1;
            //get files from request
            HttpFileCollection files = HttpContext.Current.Request.Files;
            await Task.Run(() =>
            {
                foreach (var f in files.AllKeys)
                {
                    HttpPostedFile file = files[f];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileLocalFullName = newRoot + file.FileName;
                        file.SaveAs(fileLocalFullName);
                        fileNameList.Add($"Files/Files/{file.FileName}");
                        FileInfo fileInfo = new FileInfo(fileLocalFullName);
                        fileTotalSize += fileInfo.Length;
                        sb.Append($" #{fileIndex} Uploaded file: {file.FileName} ({ fileInfo.Length} bytes)");
                        fileIndex++;
                        Trace.WriteLine("1 file copied , filePath=" + fileLocalFullName);
                    }
                }
            });
            return Json(Return_Helper.Success_Msg_Data_DCount_HttpCode($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully!   Details -> {sb.ToString()}", fileNameList, fileNameList.Count));
        }
    }
}

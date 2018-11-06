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
using System.Web.Http;
using WebApiSample.Helpers;

namespace WebApiSample.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PicturesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //Get : api/Pictures
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(RootPathMvc + @"Files/Pictures");
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
        //POST : api/Pictures
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new Exception_DG("unsupported media type", 2005);
            CreateDirectoryIfNotExist(RootPathMvc + "/temp");
            var provider = new MultipartFormDataStreamProvider(RootPathMvc + "/temp");
            // Read the form data. 
            await Request.Content.ReadAsMultipartAsync(provider);
            List<string> fileNameList = new List<string>();
            StringBuilder sb = new StringBuilder();
            long fileTotalSize = 0;
            int fileIndex = 1;
            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                //new folder
                string newRoot = RootPathMvc + @"Files/Pictures";
                CreateDirectoryIfNotExist(newRoot);
                if (File.Exists(file.LocalFileName))
                {
                    //new fileName
                    string fileName = file.Headers.ContentDisposition.FileName.Substring(1, file.Headers.ContentDisposition.FileName.Length - 2);
                    string newFileName = Guid.NewGuid() + "." + fileName.Split('.')[1];
                    string newFullFileName = newRoot + "/" + newFileName;
                    fileNameList.Add($"Files/Pictures/{newFileName}");
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    fileTotalSize += fileInfo.Length;
                    sb.Append($" #{fileIndex} Uploaded file: {newFileName} ({ fileInfo.Length} bytes)");
                    fileIndex++;
                    File.Move(file.LocalFileName, newFullFileName);
                    Trace.WriteLine("1 file copied , filePath=" + newFullFileName);
                }
            }
            return Json(Return_Helper.Success_Msg_Data_DCount_HttpCode($"{fileNameList.Count} file(s) /{fileTotalSize} bytes uploaded successfully!   Details -> {sb.ToString()}", fileNameList, fileNameList.Count));
        }

        /// <summary>
        /// 
        /// </summary>
        public static string RootPathMvc = System.Web.HttpContext.Current.Server.MapPath("~");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CreateDirectoryIfNotExist(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return true;
        }
    }
}

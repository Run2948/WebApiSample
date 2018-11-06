using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiSample.Helpers
{
    public abstract class IO_Helper
    {
        public static string RootPath_MVC => System.Web.HttpContext.Current.Server.MapPath("~");

        public static bool CopyFile(string sourceFilePath, string newFilePath, bool allowCoverSameNameFiles = true)
        {
            File.Copy(sourceFilePath, newFilePath, allowCoverSameNameFiles);//允许覆盖同名文件
            return true;
        }

        //create Directory
        public static bool CreateDirectoryIfNotExist(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return true;
        }

        public static string DeskTopPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); }
        }

        public static void OpenDirectory(string directoryPath)
        {
            System.Diagnostics.Process.Start("Explorer.exe", directoryPath);
        }
    }
}
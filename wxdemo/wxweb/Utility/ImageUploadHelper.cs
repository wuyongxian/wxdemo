using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace wxweb
{
    public class ImageUploadHelper
    {
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="server"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetTempFilePhysicsPath(HttpServerUtilityBase server, string fileName, string directory)
        {
            var path = server.MapPath("~/Content/" + directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, fileName);//先存入临时文件夹
        }

        /// <summary>
        /// 获取Image的虚拟路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetTempFileVisualPath(UrlHelper url, string fileName, string directory)
        {
            return url.Content("~/Content/" + directory + "/" + fileName);
        }

        /// <summary>
        /// 将文件移动到指定文件夹
        /// </summary>
        /// <param name="filePathtemp"></param>
        /// <param name="filePathFormal"></param>
        /// <returns></returns>
        public static void MoveFileToFormalDirectory(string filePathtemp, string filePathFormal) {

            if (!Directory.Exists(Path.GetDirectoryName(filePathFormal)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePathFormal));
            }
            File.Copy(filePathtemp, filePathFormal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace wxweb
{
    public class FileHelper
    {
        /// <summary>
        /// 转换文件名 Fix IE下面的FileName与chrome firefox浏览器不同
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string TransferFileName(string fileName)
        {
            if (fileName.LastIndexOf(@"/") > 0)
            {
                return fileName.Substring(fileName.LastIndexOf(@"/") + 1);
            }
            return fileName;
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExtensionName(string fileName)
        {
            if (fileName.LastIndexOf("\\", StringComparison.Ordinal) > -1)//在不同浏览器下，filename有时候指的是文件名，有时候指的是全路径，所有这里要要统一。
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);//IndexOf 有时候会受到特殊字符的影响而判断错误。加上这个就纠正了。
            }
            return Path.GetExtension(fileName.ToLower());
        }
    }
}

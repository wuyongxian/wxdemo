using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace common
{
    public class filehelper
    {
        public void WriteTxt(string filename)
        {
            string txtPath = System.Web.HttpContext.Current.Server.MapPath("~\\Public\\AttInfo\\") + "Test.txt";
            StreamWriter sw = new StreamWriter(txtPath, false, System.Text.Encoding.Default);
            sw.WriteLine("Hello World");
            sw.WriteLine(""); //输出空行 
            sw.WriteLine("ASP.NET网络编程 - 脚本之家！");
            sw.Close();
        }

        public void SaveTxt()
        {

        }


        public void ReadData()
        {
            //C#读取TXT文件之建立  FileStream 的对象,说白了告诉程序,     
            //文件在那里,对文件如何 处理,对文件内容采取的处理方式     
            System.Text.Encoding code = System.Text.Encoding.GetEncoding("gb2312");
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("./wxlm/huo.txt"), FileMode.Open, FileAccess.Read);
            //仅 对文本 执行  读写操作     
            StreamReader sr = new StreamReader(fs, code);
            //定位操作点,begin 是一个参考点     
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            //读一下，看看文件内有没有内容，为下一步循环 提供判断依据     
            //sr.ReadLine() 这里是 StreamReader的要领  可不是 console 中的~      
            string str = sr.ReadToEnd();//假如  文件有内容     

            //C#读取TXT文件之关上文件，留心顺序，先对文件内部执行 关上，然后才是文件~     
            sr.Close();
            fs.Close();
            System.Web.HttpContext.Current.Response.Write(str);
        }



        /// <summary>
        /// 读文本文件信息
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static List<string> ReadTxtFile(string FilePath)
        {
            List<string> strList = new List<string>();

            System.IO.FileStream FS = null;
            System.IO.StreamReader SR = null;
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    if (strList == null)
                        strList = new List<string>();

                    //using自动释放资源
                    using (FS = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
                    {
                        SR = new System.IO.StreamReader(FS, System.Text.Encoding.Default);
                        string Line = null;
                        System.Drawing.Color color = new System.Drawing.Color();
                        string[] rgb = new string[4];

                        for (Line = SR.ReadLine(); Line != null; Line = SR.ReadLine())
                        {
                            if (!(Line.Trim() == ""))
                            {
                                strList.Add(Line);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (SR != null)
                    {
                        SR.Dispose();
                        SR.Close();
                    }
                    if (FS != null)
                    {
                        FS.Dispose();
                        FS.Close();
                    }
                }
            }
            else
            {
                strList = null;
            }
            return strList;
        }


        /// <summary>
        /// 保存文本文件信息 
        /// </summary>
        /// <param name="psList">要写入文件的信息</param>
        /// <param name="filepath">存放文件的绝对路径</param>
        /// <returns></returns>
        public static bool SaveToTxtFile(List<string> psList, string filepath)
        {
            bool bResult = false;
            System.IO.FileStream FS = null;
            System.IO.StreamWriter SW = null;
            try
            {
                //新建文件流
                FS = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //建立文件对应的输入流
                SW = new System.IO.StreamWriter(FS);

                foreach (string ps in psList)
                {
                    SW.Write(ps + "\r\n");
                }
                bResult = true;
            }
            catch
            {
                bResult = false;
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
                if (FS != null)
                {
                    FS.Close();
                }
            }
            return bResult;
        }

    }
}

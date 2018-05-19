using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;

namespace wxweb
{
    public class ConvertToImageHelper
    {
        public void Convert2Image() { 
            
        }

        public void GetImage()
        {
            Stream s = File.Open("33.jpg", FileMode.Open);
            int leng = 0;
            if (s.Length < Int32.MaxValue)
                leng = (int)s.Length;
            byte[] by = new byte[leng];
            s.Read(by, 0, leng);//把图片读到字节数组中
            s.Close();

            string str = Convert.ToBase64String(by);//把字节数组转换成字符串
            StreamWriter sw = File.CreateText("11.txt");//存入11.txt文件
            sw.Write(str);
            sw.Close();
            sw.Dispose();
        }

        //把字符串还原成图片
        public void CreateImg()
        {
            StreamReader sr = new StreamReader("11.txt");
            string s = sr.ReadToEnd();
            sr.Close();
            byte[] buf = Convert.FromBase64String(s);//把字符串读到字节数组中

            MemoryStream ms = new MemoryStream(buf);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save("12.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Close();
            ms.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace YTDInfo.XJTLU.Donate.Common.Utility
{
    public class Imagehelper
    {
        public static void GetPic(string dsource, string dFile, int smallWidth, int smallHeight)
        {
            //读取大图
            Image image = Image.FromFile(dsource);
            //按比例缩小 长高设定一个值
            if (image.Width > image.Height)
            {
                smallWidth = smallHeight * image.Width / image.Height;
            }
            else
            {
                smallHeight = smallWidth * image.Height / image.Width;
            }
            //缩略图
            Bitmap bitmap = new Bitmap(smallWidth, smallHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(image, 0, 0, smallWidth, smallHeight);
            //保存图片 第二个参数根据自己的图片格式选择
            if (dsource.Substring(dsource.LastIndexOf(".") + 1, dsource.Length - (dsource.LastIndexOf(".")+1)).ToLower() == "png")
            {
                bitmap.Save(dFile, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                bitmap.Save(dFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            g.Dispose();
            bitmap.Dispose();
            image.Dispose();

        }

    }
}

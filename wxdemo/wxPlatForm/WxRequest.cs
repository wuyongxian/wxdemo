using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace wxPlatForm
{
    public class WxRequest
    {

        /// <summary>
        /// 请求微信接口
        /// </summary>
        /// <param name="posturl">请求的地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public static string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = null;
            if (postData.Length > 0) //有值代表创建菜单
            {
                data = encoding.GetBytes(postData);
            }

            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                if (postData.Length > 0)
                {
                    request.Method = "POST"; //创建菜单
                }
                else
                {
                    request.Method = "GET"; //删除菜单
                }

                request.ContentType = "application/x-www-form-urlencoded";

                if (postData.Length > 0) //有值代表创建菜单
                {
                    request.ContentLength = data.Length;
                    outstream = request.GetRequestStream();
                    outstream.Write(data, 0, data.Length);
                    outstream.Close();
                }

                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //Response.Write(err);
               // Response.End();
                return string.Empty;
            }
        }

        public static BaseMessage Load(EnterParam param, bool bug = true)
        {
            string postStr = "";
            Stream s = VqiRequest.GetInputStream();//此方法是对System.Web.HttpContext.Current.Request.InputStream的封装，可直接代码
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = Encoding.UTF8.GetString(b);//获取微信服务器推送过来的字符串
            var timestamp = VqiRequest.GetQueryString("timestamp");
            var nonce = VqiRequest.GetQueryString("nonce");
            var msg_signature = VqiRequest.GetQueryString("msg_signature");
            var encrypt_type = VqiRequest.GetQueryString("encrypt_type");
            string data = "";
            if (encrypt_type == "aes")//加密模式处理
            {
                //param.IsAes = true;
                //var ret = new MsgCrypt(param.token, param.EncodingAESKey, param.appid);
                //int r = ret.DecryptMsg(msg_signature, timestamp, nonce, postStr, ref data);
                //if (r != 0)
                //{
                //    wxPlatForm.Base.WriteBug("消息解密失败");
                //    return null;

                //}
            }
            else
            {
                param.IsAes = false;
                data = postStr;
            }
            if (bug)
            {
                Utils.WriteTxt(data);
            }
            return MessageFactory.CreateMessage(data);
        }
    }
}

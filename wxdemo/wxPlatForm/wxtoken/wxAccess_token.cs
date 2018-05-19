using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace wxPlatForm
{
    public class wxAccess_token
    {

        /// <summary>
        /// 根据当前日期 判断Access_Token 是否超期  如果超期返回新的Access_Token   否则返回之前的Access_Token
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string IsExistAccess_Token()
        {
            string Token = string.Empty;
            DateTime YouXRQ;
            // 读取XML文件中的数据，并显示出来 ，注意文件路径
            string filepath = HttpContext.Current.Server.MapPath("\\Files\\WXFiles\\AccessToken.xml") ;
            XmlDocument xml = common.xmlHelper.getXML(filepath);
           
            Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);


            //TimeSpan st1 = new TimeSpan(YouXRQ.Ticks); //最后刷新的时间
            //TimeSpan st2 = new TimeSpan(DateTime.Now.Ticks); //当前时间
            //TimeSpan st = st2 - st1; //两者相差时间
            if (DateTime.Now > YouXRQ)
            {
                DateTime _youxrq = DateTime.Now;

               string appid = xml.SelectSingleNode("xml").SelectSingleNode("appid").InnerText;
               string secret = xml.SelectSingleNode("xml").SelectSingleNode("secret").InnerText;

                Access_token mode = GetAccess_token(appid, secret);
                xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.access_token;
                _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
                xml.Save(filepath);
                Token = mode.access_token;
            }
            return Token;
        }


        /// <summary>
        /// 获取Access_token
        /// </summary>
        /// <param name="appid">微信公众号appid</param>
        /// <param name="secret">微信公众号appsecret</param>
        /// <returns></returns>
        private static Access_token GetAccess_token(string appid, string secret)
        {
            string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            Access_token mode = new Access_token();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);  //用GET形式请求指定的地址 
            req.Method = "GET";

            using (WebResponse wr = req.GetResponse())
            {
                //HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();  
                StreamReader reader = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                //在这里对Access_token 赋值  
                Access_token token = new Access_token();
                token = common.JsonHelper.ParseFromJson<Access_token>(content);
                mode.access_token = token.access_token;
                mode.expires_in = token.expires_in;
            }
            return mode;
        }
    }
}
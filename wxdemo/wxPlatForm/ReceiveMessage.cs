using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace wxPlatForm
{
    public class ReceiveMessage
    {
        #region 接收的类型
       

        public static void GetText(TextMessage item)
        {
            common.CommonMethod.WriteTxt(item.Content);//打印接收的文本消息
          
            DataTable dtwxMsg = DAL.wxDAL.wxMsgDAL.Get_wxmsmResponse_key(item.Content);
             WriteXML(item, dtwxMsg);
        }


        public static void GetEvent(EventMessage item)
        {
            common.CommonMethod.WriteTxt(item.EventKey);//打印接收的文本消息
         
            DataTable dtwxMsg = DAL.wxDAL.wxMsgDAL.Get_wxmsmResponse_key(item.EventKey);
             WriteXML(item, dtwxMsg);
        }

        public static void WriteXML(BaseMessage item,DataTable dtwxMsg) {
           
            if (dtwxMsg == null || dtwxMsg.Rows.Count == 0)
            {
                dtwxMsg = DAL.wxDAL.wxMsgDAL.Get_wxmsmResponse_key("key_query");
            }
            string resMessageType = dtwxMsg.Rows[0]["resMessageType"].ToString();
            string XML = "";
            switch (resMessageType)
            {
                case "text":
                    XML = ResponseMessage.ReText(item.FromUserName, item.ToUserName, dtwxMsg);
                    break;
                case "news":
                    XML = ResponseMessage.ReArticle(item.FromUserName, item.ToUserName, dtwxMsg);
                    break;
                case "image":

                    break;
                case "voice":

                    break;
                case "video":

                    break;
                case "music":

                    break;
                default:

                    break;
            }
            HttpContext.Current.Response.Write(XML);
            HttpContext.Current.Response.End();
        }

        public static void WriteXML_Text(BaseMessage item, string content)
        {

            string XML = ResponseMessage.ReText(item.FromUserName, item.ToUserName, content); ;
          
            HttpContext.Current.Response.Write(XML);
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 未关注扫描带参数二维码
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="EventKey"></param>
        /// <returns></returns>
        public static string SubScanQrcode(string FromUserName, string ToUserName, string EventKey)
        {
            return "";
        }

        /// <summary>
        /// 已关注扫描带参数二维码
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="EventKey"></param>
        /// <returns></returns>
        public static string ScanQrcode(string FromUserName, string ToUserName, string EventKey)
        {
            return "";
        }
        #endregion
    }
}

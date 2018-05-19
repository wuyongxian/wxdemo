using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace wxPlatForm
{
    public class ResponseMessage
    {
        

        #region 回复方式
        public static void ResponseNull()
        {
            //Utils.ResponseWrite("");
        }
        /// <summary>
        /// 回复文本
        /// </summary>
        /// <param name="FromUserName">发送给谁(openid)</param>
        /// <param name="ToUserName">来自谁(公众账号ID)</param>
        /// <param name="Content">回复类型文本</param>
        /// <returns>拼凑的XML</returns>
        public static string ReText(string FromUserName, string ToUserName,string Content)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + common.CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[text]]></MsgType>";//回复类型文本
            XML += "<Content><![CDATA["+ Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";//回复内容 FuncFlag设置为1的时候，自动星标刚才接收到的消息，适合活动统计使用
            return XML;
        }

        public static string ReText(string FromUserName, string ToUserName,DataTable dtMsg)
        {
            string content = "";
            for (int i = 0; i < dtMsg.Rows.Count; i++) {
                if (i == 0)
                {
                    content = dtMsg.Rows[i]["msgContent"].ToString();
                }
                else {
                    content +="\n"+ dtMsg.Rows[i]["msgContent"].ToString();
                }
            }
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + common.CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[text]]></MsgType>";//回复类型文本
            XML += "<Content><![CDATA[" + content + "]]></Content><FuncFlag>0</FuncFlag></xml>";//回复内容 FuncFlag设置为1的时候，自动星标刚才接收到的消息，适合活动统计使用
            return XML;
        }



        /// <summary>
        /// 单/多图文回复
        /// </summary>
        /// <param name="FromUserName">发送给谁(openid)</param>
        /// <param name="ToUserName">来自谁(公众账号ID)</param>
        /// <param name="ArticleCount">图文数量</param>
        /// <param name="dtArticle"></param>
        /// <returns></returns>
        public static string ReArticle(string FromUserName, string ToUserName, System.Data.DataTable dtArticle)
        {
            string XML = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>";//发送给谁(openid)，来自谁(公众账号ID)
            XML += "<CreateTime>" + common.CommonMethod.ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";//回复时间戳
            XML += "<MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + dtArticle.Rows.Count + "</ArticleCount><Articles>";
            string wxdna = ConfigurationManager.AppSettings["wxdna"];
            
           // PicUrl += @"Files\wxnews\1.jpg";
            foreach (System.Data.DataRow Item in dtArticle.Rows)
            {
                string PicUrl = Item["PicUrl"].ToString();
                PicUrl= PicUrl.Replace("\\","/");
                PicUrl = wxdna + PicUrl;
               common.CommonMethod.WriteTxt(PicUrl);
                XML += "<item><Title><![CDATA[" + Item["Title"] + "]]></Title><Description><![CDATA[" + Item["msgDescription"] + "]]></Description><PicUrl><![CDATA[" + PicUrl + "]]></PicUrl><Url><![CDATA[" + Item["Url"] + "]]></Url></item>";
            }
            XML += "</Articles><FuncFlag>0</FuncFlag></xml>";
            return XML;
        }

        /// <summary>
        /// 回复消息(音乐)
        /// </summary>
        public static void ResMusic(EnterParam param, Music mu)
        {


        }
        public static void ResVideo(EnterParam param, Video v)
        {
        }

        /// <summary>
        /// 回复消息(图片)
        /// </summary>
        public static void ResPicture(EnterParam param, Picture pic, string domain)
        {

        }

        #endregion


    }
}

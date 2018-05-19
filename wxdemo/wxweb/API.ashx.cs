using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using wxPlatForm;
using System.IO;
using System.Text;
using System.Xml;


namespace wxweb
{
    /// <summary>
    /// API 的摘要说明
    /// </summary>
    public class API : IHttpHandler, IRequiresSessionState
    {
        const string access_token = "Wuyx201801";
        public void ProcessRequest(HttpContext context)
        {          
            context.Response.ContentType = "text/plain";
            if (context.Request.HttpMethod.ToLower() == "post")
            {
                //回复消息的时候也需要验证消息，这个很多开发者没有注意这个，存在安全隐患  
                //微信中 谁都可以获取信息 所以 关系不大 对于普通用户 但是对于某些涉及到验证信息的开发非常有必要
                if (wxPlatForm.wxtoken.checkWX.CheckSignature(access_token))
                {
                    //接收消息
                    ReceiveXml();
                }
                else
                {
                    HttpContext.Current.Response.Write("消息并非来自微信");
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                wxPlatForm.wxtoken.checkWX.CheckWechat(access_token);
            }
        }

      

        #region 接收消息
        /// <summary>
        /// 接收微信发送的XML消息并且解析
        /// </summary>
        private void ReceiveXml()
        {
            try
            {
                //BaseMessage bm = wxPlatForm.WxRequest.Load(new EnterParam { IsAes = false, token = "", appid = "", EncodingAESKey = "" });

                Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
                byte[] requestByte = new byte[requestStream.Length];
                requestStream.Read(requestByte, 0, (int)requestStream.Length);
                string requestStr = Encoding.UTF8.GetString(requestByte);
                BaseMessage bm = MessageFactory.CreateMessage(requestStr);
                
                if (bm.MsgType == MsgType.TEXT)
                {
                    TextMessage tm = (TextMessage)bm;
                    ReceiveMessage.GetText(tm);
                }
                else if (bm.MsgType == MsgType.LOCATION)
                {
                    ReceiveMessage.WriteXML_Text(bm, "我们不接收地理位置消息！");
                    //xml = ReceiveMessage.GetText(bm.FromUserName, bm.ToUserName, "this is location info");
                }
                else if (bm.MsgType == MsgType.IMAGE)
                {
                    ReceiveMessage.WriteXML_Text(bm, "我们不接收图片消息！");
                    //xml = ReceiveMessage.GetText(bm.FromUserName, bm.ToUserName, "this is image info");
                }
                else if (bm.MsgType == MsgType.EVENT)
                {
                    EventMessage eventmessage = (EventMessage)bm;
                    eventFunc.loadFunc(eventmessage);
                }
            
            }
            catch (Exception ex) {
                common.CommonMethod.WriteTxt(ex.Message);//记录错误信息
            }
        }
        #endregion

      

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
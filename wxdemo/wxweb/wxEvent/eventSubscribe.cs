using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DAL;
using wxPlatForm;

namespace wxweb
{
    
    public class eventSubscribe
    {
        PHWXEntities wxdb = new PHWXEntities();
        public  void Create(EventMessage em) {
            string openid = em.FromUserName;
            //获取病人信息
            Dictionary<string, object> respDic = GetUserList(openid);
            DAL.wxDAL.subscribeDAL.subscribe_Add(respDic);

        }


        /// <summary>
        /// 用openid换取用户信息
        /// </summary>
        /// <param name="openid">微信标识id</param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserList(string openid)
        {
            JavaScriptSerializer Jss = new JavaScriptSerializer();
            string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            string wxpost = WxRequest.GetPage(url, "");
            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(wxpost);
            return respDic;
           
        }
    }
}
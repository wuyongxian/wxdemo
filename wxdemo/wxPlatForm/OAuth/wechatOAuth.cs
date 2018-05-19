using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace wxPlatForm.OAuth
{
    public class wechatOAuth
    {
        /// <summary>  
        /// 公众号的唯一标识  
        /// </summary>  
        public string appid;
        /// <summary>  
        /// 公众号的appsecret  
        /// </summary>  
        public string secret;

        public OAuthAccess_Token o_token;

        public OAuthUser o_user;

        private string code;
        
        /// <summary>
        /// 获取code
        /// </summary>
        public void get_code() {
            try
            {
                code = HttpContext.Current.Request.QueryString["Code"];
                
                if (code != ""&&code!=null) {
                    get_access_token();
                }
            }
            catch (Exception ex)
            {
                common.CommonMethod.WriteTxt(ex.Message);
            }
        }

        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        public void get_access_token() {
            Dictionary<string, string> obj = new Dictionary<string, string>();
            var client = new System.Net.WebClient();
            var serializer = new JavaScriptSerializer();
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            client.Encoding = System.Text.Encoding.UTF8;
            string dataaccess = "";
            try
            {
                dataaccess = client.DownloadString(url);
                //获取字典
                obj = serializer.Deserialize<Dictionary<string, string>>(dataaccess);
                string accessToken = "";
                if (obj.TryGetValue("access_token", out accessToken))  //判断access_Token是否存在
                {
                     o_token =new OAuthAccess_Token {
                        access_token=obj["access_token"],
                        expires_in =Convert.ToInt32( obj["expires_in"]),
                        refresh_token = obj["refresh_token"],
                        openid = obj["openid"],
                        scope = obj["scope"]
                    };
                    if (o_token.scope == "snsapi_userinfo") {
                        get_userinfo(o_token.access_token, o_token.openid);
                    }
                }
                else  //access_Token 失效时重新发送。
                {
                    //存log方法
                    common.CommonMethod.WriteTxt("access_token 获取失败，time："+DateTime.Now.ToLongTimeString());
                }
            }
            catch (Exception e)
            {
                //存log方法
                common.CommonMethod.WriteTxt(e.Message);
            }
           
          
        }


       
        /// <summary>
        /// 拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="access_token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="access_token">	用户的唯一标识</param>
        public void get_userinfo(string access_token,string openid) {
            //JavaScriptSerializer Jss = new JavaScriptSerializer();

            //string url2 = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            //string wxpost = WxRequest.GetPage(url2, "");
            //Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(wxpost);
        


            Dictionary<string, object> obj = new Dictionary<string, object>();
            var client = new System.Net.WebClient();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            client.Encoding = System.Text.Encoding.UTF8;
            string dataaccess = "";
            try
            {
                
                dataaccess = client.DownloadString(url);
             
               // dataaccess = "{\"openid\":\"o15u_007vgfUqAEFFDJt - kNnV_dg\",\"nickname\":\"一叶知秋\"}";
                obj = serializer.Deserialize<Dictionary<string, object>>(dataaccess);
                object user_openid = "";
                if (obj.TryGetValue("openid", out user_openid))  //判断access_Token是否存在
                {
                     o_user = new OAuthUser
                    {
                        openid = obj["openid"].ToString(),
                        nickname = obj["nickname"].ToString(),
                        sex =Convert.ToInt32( obj["sex"]),
                        province = obj["province"].ToString(),
                        city = obj["city"].ToString(),
                        country = obj["country"].ToString(),
                        headimgurl = obj["headimgurl"].ToString(),
                        privilege =obj["privilege"].ToString(),
                        unionid =""
                     };
                   
                }
                else  //access_Token 失效时重新发送。
                {
                    //存log方法
                    common.CommonMethod.WriteTxt("用户信息 获取失败，time：" + DateTime.Now.ToLongTimeString());
                }
            }
            catch (Exception e)
            {
                //存log方法
                common.CommonMethod.WriteTxt(e.Message);
            }
        }

        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>  
        /// <param name="refresh_token">填写通过access_token获取到的refresh_token参数</param>
        public void refresh_access_token(string refresh_token) {

            Dictionary<string, string> obj = new Dictionary<string, string>();
            var client = new System.Net.WebClient();
            var serializer = new JavaScriptSerializer();
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", this.appid, refresh_token);
            client.Encoding = System.Text.Encoding.UTF8;
            string dataaccess = "";
            try
            {
                dataaccess = client.DownloadString(url);
                //获取字典
                obj = serializer.Deserialize<Dictionary<string, string>>(dataaccess);
                string accessToken = "";
                if (obj.TryGetValue("access_token", out accessToken))  //判断access_Token是否存在
                {
                    OAuthAccess_Token o_token = new OAuthAccess_Token
                    {
                        access_token = obj["access_token"],
                        expires_in = Convert.ToInt32(obj["expires_in"]),
                        refresh_token = obj["refresh_token"],
                        openid = obj["openid"],
                        scope = obj["scope"]
                    };
                    if (o_token.scope == "snsapi_userinfo")
                    {
                        get_userinfo(o_token.access_token, o_token.openid);
                    }
                }
                else  //access_Token 失效时重新发送。
                {
                    //存log方法
                    common.CommonMethod.WriteTxt("access_token 获取失败，time：" + DateTime.Now.ToLongTimeString());
                }
            }
            catch (Exception e)
            {
                //存log方法
                common.CommonMethod.WriteTxt(e.Message);
            }


        }


        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        public void check_access_token()
        {

        }

    }
}

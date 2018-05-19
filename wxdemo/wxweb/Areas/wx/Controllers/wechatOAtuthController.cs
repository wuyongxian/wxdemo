using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace wxweb.Areas.wx.Controllers
{
    [LoginCheckFilter(IsCheck = false)]//当前页面不进行登录校验
    public class wechatOAtuthController : Controller
    {
        // GET: wx/wechatOAtuth
        public ActionResult Index()
        {
            wxPlatForm.OAuth.wechatOAuth oauth = new wxPlatForm.OAuth.wechatOAuth();
            string filepath = Server.MapPath("\\Files\\WXFiles\\AccessToken.xml");
            XmlDocument xml = common.xmlHelper.getXML(filepath);
            oauth.appid = xml.SelectSingleNode("xml").SelectSingleNode("appid").InnerText;
            oauth.secret = xml.SelectSingleNode("xml").SelectSingleNode("secret").InnerText;
            oauth.get_code();//获取token及userinfo信息
            if (oauth.o_user != null)
            {
                 System.Web.HttpContext.Current.Session["o_user"] = oauth.o_user;
                //ViewBag.o_user = oauth.o_user;
                return RedirectToAction("Success", "wechatOAtuthUser");
                //return View(oauth.o_user);
            }
            else
            {
                return RedirectToAction("Error", "wechatOAtuthUser");
            }
        }


        public ActionResult user()
        {
            return View();
        }

        public ActionResult check()
        {
            return View();
        }


        public ActionResult report()
        {
            return View();
        }

        public ActionResult survey()
        {
            return View();
        }
    }
}
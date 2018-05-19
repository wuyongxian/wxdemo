using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.wx.Controllers
{
    [LoginCheckFilter(IsCheck = false)]//当前页面不进行登录校验
    public class wechatOAtuthUserController : Controller
    {
        // GET: wx/wechatOAtuthUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            var item = System.Web.HttpContext.Current.Session["o_user"];
            return View(item);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
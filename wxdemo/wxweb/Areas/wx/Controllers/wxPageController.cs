using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.wx.Controllers
{
    [LoginCheckFilter(IsCheck =false)]//当前页面不进行登录校验
    public class wxPageController : Controller
    {
        
        // GET: wx/wxPage
        public ActionResult Index()
        {
            wxPlatForm.wxOAuth.GetOpenID("http://18701917173.xicp.net/wx/wxPage/Index");
            return View();
        }
    }
}
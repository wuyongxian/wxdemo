using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.wx.Controllers
{
    [LoginCheckFilter(IsCheck = false)]//当前页面不进行登录校验
    public class wechatDiseaseController : Controller
    {
        // GET: wx/wechatDisease
        public ActionResult Index()
        {
            return View();
        }
    }
}
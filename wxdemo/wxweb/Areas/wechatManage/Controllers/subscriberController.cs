using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace wxweb.Areas.wechatManage.Controllers
{
    public class subscriberController : Controller
    {
        PHWXEntities db = new PHWXEntities();
        // GET: Admin/subscriber
        public ActionResult Index()
        {
            var items = db.subscribers.OrderBy(p => p.subscribe_time);
            return View(items);
        }

        [HttpPost]
        public JsonResult getItem(string openid)
        {
            //var pt = db.UserGroups.Find(id);
            var item = db.subscribers.Find(openid);
            return Json(item);
            //return View(pt);
        }
    }
}
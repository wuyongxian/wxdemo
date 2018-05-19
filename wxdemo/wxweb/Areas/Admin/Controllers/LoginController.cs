using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        PHWXEntities db = new PHWXEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(FormCollection fc)
        {
            string sLoginName = "";
            string sPassword = "";

            for (int i = 0; i < fc.Count; i++)
            {
                string key = fc.AllKeys[i];
                string value = fc[i];
                if (key == "sLoginName")
                {
                    sLoginName = value;
                }
                if (key == "sPassword")
                {
                    sPassword = value;
                }
            }
            var item = db.Users.Where(p => p.sLoginName == sLoginName);
            if (item.Count() == 0) {
                var msg = new { state = false, msg = "该登陆名不存在!" };
                return Json(msg);
            }
            //item = item.Where(p => p.sPassword == Encryption.GetMD5_32(sPassword));
            string encPassword = Encryption.GetMD5_32(sPassword);
            item = from n in db.Users
                   where n.sLoginName == sLoginName
                   where n.sPassword == encPassword
                   select n;
            if (item.ToList().Count == 0)
            {
                var msg = new { state = false, msg = "密码错误!" };
                return Json(msg);
            }
            else {
                User user = (User)item.ToList()[0];
                var menuAuth = db.UserMenuAuthorities.Where(p => p.UserGroupID == user.UserGroupID);
                //添加缓存
                System.Web.HttpContext.Current.Session["user"] = user;
                HttpContext.Session["user"] = user;
                HttpCookie cookie = new HttpCookie("UserID");
                DateTime dtNow = DateTime.Now;
                TimeSpan ts = new TimeSpan(1, 0, 0, 0);//设置cookie的保存时间为一天
                cookie.Expires = dtNow.Add(ts);
                cookie.Value = user.ID;
                System.Web.HttpContext.Current.Response.Cookies.Set(cookie);

                var msg = new { state = true, msg = "登陆成功!",user=Json(user),menuAuth=Json(menuAuth) };
                return Json(msg);
            }

        }

        [HttpPost]
        public JsonResult UserLoginOut() {
            //action
            System.Web.HttpContext.Current.Session.Remove("user");
            System.Web.HttpContext.Current.Response.Cookies.Remove("UserID");
            var msg = new { state = true, msg = "退出成功!" };
            return Json(msg);
        }
    }
}
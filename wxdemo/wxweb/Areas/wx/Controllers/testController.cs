using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using wxPlatForm;

namespace wxweb.Areas.wx.Controllers
{
    public class testController : Controller
    {
        // GET: wx/test
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getToken() {
            try
            {
                string token = wxAccess_token.IsExistAccess_Token();
                var msg = new { state = true, msg = "OK",token=token};
                return Json(msg);
            }
            catch(Exception ex){
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }

        [HttpPost]
        public JsonResult getServerIP()
        {
            try
            {
                string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
                string wxpost = WxRequest.GetPage("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=" + access_token, "");
                var msg = new { state = true, msg = wxpost };
                return Json(msg);
            }
            catch (Exception ex)
            {
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }



        [HttpPost]
        public JsonResult GetCustomMenu()
        {
            try
            {
                string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
                string wxpost = WxRequest.GetPage("https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + access_token, "");
                var msg = new { state = true, msg ="查询菜单结果:" +wxpost };
                return Json(msg);
            }
            catch (Exception ex)
            {
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }

        [HttpPost]
        public JsonResult CreateCustomMenu()
        {
            try
            {
                //FileStream fs = new FileStream(Server.MapPath(".") + "\\Files\\WXFiles\\menu.txt", FileMode.Open);
                FileStream fs = new FileStream(Server.MapPath("\\Files\\WXFiles\\menu.txt"), FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GBK"));
                string menu = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                sr.Dispose();
                fs.Dispose();

                string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
                string wxpost = WxRequest.GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token, menu);
                var msg = new { state = true, msg = "创建菜单结果:" + wxpost };
                return Json(msg);
            }
            catch (Exception ex)
            {
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }

        [HttpPost]
        public JsonResult DeleteCustomMenu()
        {
            try
            {
                string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
                string wxpost = WxRequest.GetPage("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + access_token, "");
                var msg = new { state = true, msg = "删除菜单结果:" + wxpost };
                return Json(msg);
            }
            catch (Exception ex)
            {
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }




        [HttpPost]
        public JsonResult get_current_autoreply()
        {
            try
            {
                string access_token = wxAccess_token.IsExistAccess_Token(); //获取access_token
                string wxpost = WxRequest.GetPage("https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token=" + access_token, "");
                var msg = new { state = true, msg = "自动回复规则结果:" + wxpost };
                return Json(msg);
            }
            catch (Exception ex)
            {
                var msg = new { state = false, msg = ex.Message };
                return Json(msg);
            }
        }
    }
}
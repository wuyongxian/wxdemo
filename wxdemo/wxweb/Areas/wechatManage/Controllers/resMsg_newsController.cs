using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.wechatManage.Controllers
{
    public class resMsg_newsController : Controller
    {
        PHWXEntities db = new PHWXEntities();
        // GET: Admin/UserGroup
        public ActionResult Index()
        {
            var items = db.wxmsmResponses.OrderBy(p => p.sort);
            return View(items);
        }

        /// <summary>
        /// 回复消息列表
        /// </summary>
        /// <param name="eventType">触发动作类型（subscribe:订阅；text:关键字;click；点击事件）</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getItemList(string eventType)
        {
            string resMessageType = "news";//回复消息形式为:news
            var items = (from msg in db.wxmsmResponses
                         where msg.eventType == eventType
                         where msg.resMessageType == resMessageType
                         orderby msg.eventKey, msg.sort

                         select msg);
            //select new {
            //    ID=msg.ID,
            //    eventKey=msg.eventKey,
            //    msgContent=msg.msgContent,
            //    sort=msg.sort,
            //    numstate=msg.numstate

            //});
            return Json(items);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult Create(wxmsmResponse item)
        {

            // var producttype = db.productType.Find(pt.name);//Find：根据ID查询
            //var itemQ = db.wxmsmResponses.Where(p => p.sName == item.sName);


            //if (itemQ.Count() != 0)
            //{
            //    var msg_error = new { state = false, msg = "已有相同的类型名称，不可以重复添加!" };
            //    return Json(msg_error);
            //}
            if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
            {
                item.numstate = false;
            }
            else
            {
                item.numstate = true;
            }
            if (item.eventType == "subscribe")
            {
                item.eventKey = "subscribe";
            }
            item.ID = RandomHelper.getGUID();
            item.resMessageType = "news";//默认回复的为文字消息
            db.wxmsmResponses.Add(item);
            db.SaveChanges();

            var msg = new { state = true, msg = "OK" };
            return Json(msg);
            //return View();
            //return View(RedirectToAction("Index"));
        }

        [HttpPost]
        public JsonResult getItem(string id)
        {
            var item = db.wxmsmResponses.Find(id);
            return Json(item);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult EditItem(wxmsmResponse para)
        {
            // var pt = db.UserGroups.Find(id);
            try
            {
                wxmsmResponse item = db.wxmsmResponses.Find(para.ID);

                if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
                {
                    item.numstate = false;
                }
                else
                {
                    item.numstate = true;
                }
                if (para.eventType == "subscribe")
                {
                    item.eventKey = "subscribe";
                }
                item.eventKey = para.eventKey;
                item.Title = para.Title;
                item.msgDescription = para.msgDescription;
                if (para.PicUrl != "")
                {
                    item.PicUrl = para.PicUrl;
                }
                
                item.Url = para.Url;
                item.sort = para.sort;
                


                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var msg = new { state = true, msg = "更新数据成功!" };
                return Json(msg);
            }
            catch
            {
                var msg = new { state = false, msg = "更新数据失败!" };
                return Json(msg);
            }
            //return View(pt);
        }


        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {

                wxmsmResponse item = db.wxmsmResponses.Find(id);
                db.wxmsmResponses.Remove(item);
                db.SaveChanges();
                var msg = new { state = true, msg = "删除项目成功!" };
                return Json(msg);
            }
            catch
            {
                var msg = new { state = false, msg = "删除项目失败!" };
                return Json(msg);
            }


        }
    }
}
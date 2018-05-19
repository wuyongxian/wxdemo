using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace wxweb.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {

        PHWXEntities db = new PHWXEntities();
        // GET: Admin/UserGroup
        public ActionResult Index()
        {
            var items = db.UserGroups.OrderBy(p=>p.sort);
            return View(items);
        }



        [HttpPost]
        public JsonResult Create(UserGroup item)
        {

            // var producttype = db.productType.Find(pt.name);//Find：根据ID查询
            var itemQ = db.UserGroups.Where(p => p.sName == item.sName);


            if (itemQ.Count() != 0)
            {
                var msg_error = new { state = false, msg = "已有相同的类型名称，不可以重复添加!" };
                return Json(msg_error);
            }
            if (Request.Form["numstate"] == null|| Request.Form["numstate"].ToString()!="on")
            {
                item.numstate = false;
            }
            else {
                item.numstate = true;
            }
            item.ID = RandomHelper.getGUID();
            item.dtCreate = DateTime.Now;
            item.dtUpdate = DateTime.Now;
            db.UserGroups.Add(item);
            db.SaveChanges();

            var msg = new { state = true, msg = "OK" };
            return Json(msg);
            //return View();
            //return View(RedirectToAction("Index"));
        }

        [HttpPost]
        public JsonResult getItem(string id)
        {
            var item = db.UserGroups.Find(id);     
            return Json(item);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult EditItem(UserGroup para)
        {
            // var pt = db.UserGroups.Find(id);
            try
            {
                UserGroup item = db.UserGroups.Find(para.ID);
               
                if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
                {
                    item.numstate = false;
                }
                else
                {
                    item.numstate = true;
                }
                item.sName = para.sName;
                item.sCode = para.sCode;
                item.sort = para.sort;
                item.remark = para.remark;
                item.dtUpdate = DateTime.Now;

                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var msg = new { state = true, msg = "更新数据成功!" };
                return Json(msg);
            }
            catch{
                var msg = new { state = false, msg = "更新数据失败!" };
                return Json(msg);
            }
            //return View(pt);
        }


        //[HttpPost]
        //public ActionResult Edit(UserGroup ug)
        //{
        //    db.Entry(ug).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {

                UserGroup item = db.UserGroups.Find(id);
                db.UserGroups.Remove(item);
                db.SaveChanges();
                var msg = new { state = true, msg = "删除项目成功!" };
                return Json(msg);
            }
            catch {
                var msg = new { state = false, msg = "删除项目失败!" };
                return Json(msg);
            }
            

        }
    }
}
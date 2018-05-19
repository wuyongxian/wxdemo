using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;


namespace wxweb.Areas.Admin.Controllers
{
    public class UserMenuController : Controller
    {
        PHWXEntities db = new PHWXEntities();
        // GET: Admin/UserMenu
        public ActionResult Index()
        {
            var items = db.UserMenus;
            return View(items);
        }


        [HttpPost]
        public JsonResult Create(UserMenu item)
        {

            // var producttype = db.productType.Find(pt.name);//Find：根据ID查询
            var itemQ = db.UserMenus.Where(p => p.sName == item.sName);


            if (itemQ.Count() != 0)
            {
                var msg_error = new { state = false, msg = "已有相同的类型名称，不可以重复添加!" };
                return Json(msg_error);
            }
            if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
            {
                item.numstate = 0;
            }
            else
            {
                item.numstate = 1;
            }
            item.ID = RandomHelper.getGUID();
            if (item.ParentID == null) {
                item.ParentID = "";
            }
            db.UserMenus.Add(item);
            db.SaveChanges();

            var msg = new { state = true, msg = "OK" };
            return Json(msg);
            //return View();
            //return View(RedirectToAction("Index"));
        }

        [HttpPost]
        public JsonResult Update(UserMenu para)
        {
            // var pt = db.UserMenus.Find(id);
            try
            {
                UserMenu item = db.UserMenus.Find(para.ID);

                if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
                {
                    item.numstate = 0;
                }
                else
                {
                    item.numstate = 1;
                }
                item.sName = para.sName;
                item.sTag = para.sTag;
                item.sUrl = para.sUrl;
                item.sIconUrl = para.sIconUrl;
                item.sName = para.sName;
                item.sort = para.sort;
                item.remark = para.remark;

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
        public JsonResult getItem(string id)
        {
            var item = db.UserMenus.Find(id);
            return Json(item);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            try
            {

                UserMenu item = db.UserMenus.Find(id);
                var itemC = db.UserMenus.Where(p => p.ParentID == item.ID);
                if (itemC.Count() != 0)
                {
                    var msg = new { state = false, msg = "该菜单下存在子菜单，不可以删除!" };
                    return Json(msg);
                }
                else
                {
                    db.UserMenus.Remove(item);
                    db.SaveChanges();
                    var msg = new { state = true, msg = "删除项目成功!" };
                    return Json(msg);
                }
            }
            catch
            {
                var msg = new { state = false, msg = "删除项目失败!" };
                return Json(msg);
            }


        }
       

        [HttpPost]
        public JsonResult getMenuPath(string id)
        {
            var item = db.UserMenus.Find(id);
            if (item == null) {
                var msg = new { state = false, msg = "查询菜单失败!" };
                return Json(msg);
            }
            List<UserMenu> menulist = new List<UserMenu>();
            menulist.Add(item);
            addMenuPath(item.ParentID, menulist);
            menulist.Reverse();//倒叙排列
            return Json(menulist);
            //return View(pt);
        }

        void addMenuPath(string ParentID, List<UserMenu> menulist) {
            var items = db.UserMenus.Where(p => p.ID == ParentID);
            if (items.Count() != 0)
            {
               
                UserMenu item = (UserMenu)items.ToList()[0];
                menulist.Add(item);
                addMenuPath(item.ParentID, menulist);
            }
        }

        [HttpPost]
        public JsonResult getList(string id)
        {
            var items = db.UserMenus.OrderBy(p=>p.sort);
            return Json(items);
            //return View(pt);
        }


        /// <summary>
        /// 获取UserGroup的menu列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getLAMenu(string userGroupID)
        {
            var items = (from uma in db.UserMenuAuthorities
                         join um in db.UserMenus on uma.UserMenuID equals um.ID
                         where uma.UserGroupID == userGroupID
                         orderby um.sort
                         select new
                         {
                             UserMenuID=um.ID,
                             ParentID=um.ParentID,
                             sName=um.sName,
                             sTag=um.sTag,
                             sUrl=um.sUrl,
                             sIconUrl=um.sIconUrl,
                             sAuthority=uma.sAuthority,
                             isDefaultForm=uma.isDefaultForm
                         }
                       );
            return Json(items);
            //return View(pt);
        }

        /// <summary>
        /// 获取UserGroup的menu权限列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getUserGroupMenuAuthority(string userGroupID)
        {
            var items = db.UserMenuAuthorities.Where(p => p.UserGroupID == userGroupID);
            return Json(items);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult updateMenuAuthority(FormCollection fc)
        {
            //key:menuID,value:SAUDO列表
            string userGroupID = "";
            Dictionary<string, string> dicMenuList = new Dictionary<string, string>();
            for (int i = 0; i < fc.Count; i++) {
                string key = fc.AllKeys[i];
                string value = fc[i];
                if (key == "UserGroupID") {
                    userGroupID = value;
                }
                if (key.Contains("chk")) {
                    string menuID = key.Substring(key.IndexOf('_')+1);
                    string menuType = key.Substring(key.IndexOf('_') - 1, 1);
                    if (dicMenuList.Keys.Contains(menuID))
                    {
                        dicMenuList[menuID] = dicMenuList[menuID] + menuType;
                    }
                    else
                    {
                        dicMenuList[menuID] = menuType;
                    }
                }
               
               
            }
            DAL.Admin.UserMenuDAL umdal = new DAL.Admin.UserMenuDAL();
            if (umdal.updateUserGroupMenu(userGroupID, dicMenuList))
            {
                var msg = new { state = true, msg = "操作成功!" };
                return Json(msg);
            }
            else {
                var msg = new { state = false, msg = "操作失败!" };
                return Json(msg);
            }
           
        }


    }
}
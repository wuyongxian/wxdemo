using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using wxweb.Models;

namespace wxweb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        DAL.PHWXEntities db = new DAL.PHWXEntities();
        // GET: Admin/UserGroup
        public ActionResult Index()
        {
            //var userGroups = db.UserGroups;
            //var userGroupList = new List<UserGroup>();
            List<UserGroup> seriesList = db.UserGroups.ToList();
            SelectList ugList = new SelectList(seriesList, "ID", "sName");
            ViewBag.ugList = ugList.AsEnumerable();
            //for (var i = 0; i < userGroups.Count(); i++) {
            //    userGroupList.Add(new SelectListItem(ugID=userGroups[i]))

            //}

            //var items = db.Users.OrderBy(p => p.dtCreate);
            var items = (from u in db.Users
                        join ug in db.UserGroups on u.UserGroupID equals ug.ID
                        orderby u.dtCreate
                        select new 
                    {
                        ID=u.ID,
                        UserGroupID=ug.sName,
                        sRealName=u.sRealName,
                        sLoginName=u.sLoginName,
                        sPhoneNO=u.sPhoneNO,
                        sAddress=u.sAddress,
                        sGender=u.sGender,
                        numstate=u.numstate,
                        dtCreate=u.dtCreate,
                    });
            List<dynamic> itemlist = new List<dynamic>();
            foreach (var one in items.ToList()) {
                dynamic dyObject = new ExpandoObject();
                dyObject.ID = one.ID;
                dyObject.UserGroupID = one.UserGroupID;
                dyObject.sRealName = one.sRealName;
                dyObject.sLoginName = one.sLoginName;
                dyObject.sPhoneNO = one.sPhoneNO;
                switch (one.sGender) {
                    case 1:
                        dyObject.sGender = "男";break;
                    case 2:
                        dyObject.sGender = "女"; break;
                    default:
                        dyObject.sGender = ""; break;
                }
              
                dyObject.numstate = one.numstate;
                dyObject.dtCreate = ((DateTime)one.dtCreate).ToShortDateString();
                itemlist.Add(dyObject);
            }
            ViewBag.ulist = itemlist;
            return View();
        }



        [HttpPost]
        public JsonResult Create(User item)
        {
            var user = db.Users.Where(p => p.sLoginName == item.sLoginName);           
            if (user.Count() != 0)
            {
                var msg_error = new { state = false, msg = "已有相同的登陆名名称，不可以重复添加!" };
                return Json(msg_error);
            }

            //string guid = RandomHelper.getGUID();
            //JsonResult jsonr_file = UploadHeaderPhoto(guid);
            //string str_file = new JavaScriptSerializer().Serialize(jsonr_file.Data);
            //fileUpload fileUpload= JsonConvert.DeserializeObject<fileUpload>(str_file);
            ////List<fileUpload> fileuploadList= JsonConvert.DeserializeObject<List<fileUpload>>(str_file);
            //if (fileUpload.Result)
            //{
            //    item.sHeaderPhoto = fileUpload.filePath;//取上传头像路径
            //}

        

            if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
            {
                item.numstate = false;
            }
            else
            {
                item.numstate = true;
            }
            //item.ID = guid;
            item.sPassword = Encryption.GetMD5_32(item.sPassword);
            item.dtCreate = DateTime.Now;
            item.dtUpdate = DateTime.Now;
            db.Users.Add(item);
            db.SaveChanges();

            var returnmsg = new { state = true, msg = "OK" };
            return Json(returnmsg);
        }


    

        [HttpPost]
        public JsonResult getItem(string id)
        {
            var pt = db.Users.Find(id);
            return Json(pt);
            //return View(pt);
        }

        [HttpPost]
        public JsonResult EditItem(User para)
        {
            // var pt = db.UserGroups.Find(id);
            try
            {
                User item = db.Users.Find(para.ID);

                if (Request.Form["numstate"] == null || Request.Form["numstate"].ToString() != "on")
                {
                    item.numstate = false;
                }
                else
                {
                    item.numstate = true;
                }
                if (para.sHeaderPhoto != ""&&para.sHeaderPhoto!=null) {
                    item.sHeaderPhoto = para.sHeaderPhoto;
                }
                item.sRealName = para.sRealName;
                item.sLoginName = para.sLoginName;
                if (!string.IsNullOrEmpty( para.sPassword)) {
                    item.sPassword = Encryption.GetMD5_32(para.sPassword);
                }
              
                item.sPhoneNO = para.sPhoneNO;
                item.sAddress = para.sAddress;
                item.sGender = para.sGender;
                item.remark = para.remark;
                item.UserGroupID = para.UserGroupID;

                item.dtUpdate = DateTime.Now;

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

                User item = db.Users.Find(id);
                db.Users.Remove(item);
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
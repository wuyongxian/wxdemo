using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.Admin.Controllers
{
    public class FileController : Controller
    {
        // GET: Admin/File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadHeaderPhoto()
        {
            string relativePath = "\\Files\\HeaderPhoto";
            return UploadFile(relativePath);
        }

        [HttpPost]
        public JsonResult Upload_wxnewsPic()
        {
            string relativePath = "\\Files\\wxnews";
            return UploadFile(relativePath);
        }

        public JsonResult UploadFile(string relativePath)
        {
          
            bool isSavedSuccessfully = true;
            int count = 0;
            string msg = "";

            string fileName = "";
            string fileExtension = "";
            string filePath = "";
            string fileNewName = "";

            //这里是获取隐藏域中的数据
            //int albumId = string.IsNullOrEmpty(Request.Params["hidAlbumId"]) ?
            //    0 : int.Parse(Request.Params["hidAlbumId"]);
            List<wxweb.Models.fileUpload> listFile = new List<Models.fileUpload>();


            try
            {
              
                string directoryPath = ((System.Web.HttpServerUtilityWrapper)Server).MapPath(relativePath);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                foreach (string f in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[f];

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = file.FileName;
                        fileExtension = Path.GetExtension(fileName);
                        string guid = RandomHelper.getGUID();
                        fileNewName = guid + fileExtension;
                        filePath = Path.Combine(relativePath, fileNewName);
                        string savefilePath = Path.Combine(directoryPath, fileNewName);
                        file.SaveAs(savefilePath);

                        count++;
                        listFile.Add(new Models.fileUpload
                        {
                            Result = isSavedSuccessfully,
                            guid = guid,
                            filePath = filePath,
                            Count = count,
                            Message = msg
                        });
                       
                    }
                }
                return Json(listFile);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
                listFile.Add(new Models.fileUpload
                {
                    Result = isSavedSuccessfully,
                    guid = "",
                    filePath = filePath,
                    Count = count,
                    Message = msg
                });
                return Json(listFile);

            }


        }
    }
}
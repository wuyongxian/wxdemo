using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb
{
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 检测是否登录全局过滤器 原理：Action过滤器
        /// </summary>
        public bool IsCheck { get; set; }//IsCheck用于不需要检测的界面
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheck)
            {
                // string url = HttpContext.Current.Request.Url.ToString();
                //string userLoginUrl = "/Admin/Login/UserLogin";
                //string loginUrl = "/Admin/Login/Index";
                string AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                List<string> loginUrlList = new List<string>();
                loginUrlList.Add("/Admin/Login/UserLogin");
                loginUrlList.Add("/Admin/Login/Index");
                if (!loginUrlList.Contains(AbsolutePath))
                {
                    DAL.Admin.UserDAL udal = new DAL.Admin.UserDAL();
                    if (!udal.IsLogin())
                    {
                        filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
                    }
                   
                }
               
                ////检测用户是否登录
                //if (filterContext.HttpContext.Session["user"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
                //}
            }

        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;

namespace DAL.Admin
{

    public class UserDAL
    {
        PHWXEntities wx = new PHWXEntities();
       

        /// <summary>
        /// 判断Session超时
        /// </summary>
        public  bool IsLogin()
        {
            //object um = HttpContext.Current.Session["user"];// 查询ID的Session值是否存在
            object um = System.Web.HttpContext.Current.Session["user"];
            object cook = System.Web.HttpContext.Current.Request.Cookies["UserID"];
        
            bool flag = false;
            try
            {
                if (um != null)
                {
                    flag = true;
                }
                else//Cookie重新写入Session  
                {
                    if (cook != null)
                    {
                        string UserID = ((System.Web.HttpCookie)cook).Value;
                        var user = wx.Users.Where(p => p.ID == UserID);
                        if (user.Count() != 0)
                        {
                            System.Web.HttpContext.Current.Session["user"] = user.ToList()[0];
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else {
                        flag = false;
                    }
                }
            }
            catch (SystemException ex)
            {
                // HyDebug.WriteToDoc("110:" + ex.Message);//HyDebug可以用直接写入文本的方式在网站发布之后查看我们的调试信息
                flag = false;
            }
            return flag;

        }




    }
}

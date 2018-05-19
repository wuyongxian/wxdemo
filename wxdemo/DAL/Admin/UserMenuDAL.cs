using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Admin
{
    public  class UserMenuDAL
    {
        PHWXEntities wxdb = new PHWXEntities();
       public  string  getMenuUserAll()
        {

            // var items = UserMenuDAL.wxdb.UserMenus.Where(p => p.ParentID == "");
            var items = wxdb.UserMenus.Where(p => p.ParentID == "");//取父级菜单
            foreach (var item in items) {
                string sName = item.sName;
            }

            return "";
        }

        public bool updateUserGroupMenu(string userGroupID,Dictionary<string,string> dicmenu) {

            string sqlDel = string.Format(" delete from UserMenuAuthority where UserGroupID='{0}'",userGroupID);
            StringBuilder sqlAdd = new StringBuilder();
            foreach (var item in dicmenu)
            {
                sqlAdd.Append("   insert into UserMenuAuthority(ID,UserGroupID,UserID,UserMenuID,sAuthority,isDefaultForm) ");
                sqlAdd.Append(string.Format(" values(NEWID(),'{0}','','{1}','{2}',0)", userGroupID, item.Key, item.Value));
            }

            //添加事务处理
            using (SqlTransaction tran = SQLHelper.BeginTransaction(SQLHelper.connectionString))
            {
                try
                {
                    SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlDel.ToString());
                    SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sqlAdd.ToString());
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
            
        }

       


    }
}

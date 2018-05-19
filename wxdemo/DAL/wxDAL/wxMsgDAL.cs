using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.wxDAL
{
    public class wxMsgDAL
    {
        public static DataTable GetSubscribe()
        {
            DataTable dt = SQLHelper.ExecuteDataTable("select * from Users");
            return dt;
        }

        public static DataTable Get_wxmsmResponse_key(string eventKey)
        {
            DataTable dt = SQLHelper.ExecuteDataTable(string.Format("select * from wxmsmResponse where eventKey='{0}' and numstate=1 order by sort", eventKey));
            return dt;
        }
    }
}

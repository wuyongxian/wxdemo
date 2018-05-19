using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.wxDAL
{
    public class subscribeDAL
    {
        public static DataTable   GetSubscribe() {
            DataTable dt = SQLHelper.ExecuteDataTable("select * from Users");
            return dt;
        }

        public static DataTable GetSubscribeByOpenID(string openid) {
            string sql = string.Format(" select * from subscriber where openid='{0}'", openid);
            return SQLHelper.ExecuteDataTable(sql);
        }

        public static bool subscribe_Add(Dictionary<string, object> respDic) {
            DataTable dtSub = GetSubscribeByOpenID(respDic["openid"].ToString());
            if (dtSub == null || dtSub.Rows.Count == 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into subscriber(");
                strSql.Append("openid,subscribe,nickname,sex,city,country,province,strlanguage,headimgurl,subscribe_time,unionid,groupid,tagid_list,subscribe_scene,qr_scene,qr_scene_str,remark)");
                strSql.Append(" values (");
                strSql.Append("@openid,@subscribe,@nickname,@sex,@city,@country,@province,@strlanguage,@headimgurl,@subscribe_time,@unionid,@groupid,@tagid_list,@subscribe_scene,@qr_scene,@qr_scene_str,@remark)");
                SqlParameter[] parameters = {
                    new SqlParameter("@openid", SqlDbType.NVarChar,36),
                    new SqlParameter("@subscribe", SqlDbType.Bit,1),
                    new SqlParameter("@nickname", SqlDbType.NVarChar,50),
                    new SqlParameter("@sex", SqlDbType.NVarChar,50),
                    new SqlParameter("@city", SqlDbType.NVarChar,50),
                    new SqlParameter("@country", SqlDbType.NVarChar,50),
                    new SqlParameter("@province", SqlDbType.NVarChar,50),
                    new SqlParameter("@strlanguage", SqlDbType.NVarChar,50),
                    new SqlParameter("@headimgurl", SqlDbType.NVarChar,200),
                    new SqlParameter("@subscribe_time", SqlDbType.NVarChar,50),
                    new SqlParameter("@unionid", SqlDbType.NVarChar,50),
                    new SqlParameter("@groupid", SqlDbType.NVarChar,50),
                    new SqlParameter("@tagid_list", SqlDbType.NVarChar,50),
                    new SqlParameter("@subscribe_scene", SqlDbType.NVarChar,50),
                    new SqlParameter("@qr_scene", SqlDbType.NVarChar,50),
                    new SqlParameter("@qr_scene_str", SqlDbType.NVarChar,50),
                    new SqlParameter("@remark", SqlDbType.NVarChar,200)};
                parameters[0].Value = respDic["openid"].ToString();
                parameters[1].Value = Convert.ToBoolean(respDic["subscribe"]);
                parameters[2].Value = respDic["nickname"].ToString();
                parameters[3].Value = respDic["sex"].ToString();
                parameters[4].Value = respDic["city"].ToString();
                parameters[5].Value = respDic["country"].ToString();
                parameters[6].Value = respDic["province"].ToString();
                parameters[7].Value = "zh_CN";
                parameters[8].Value = respDic["headimgurl"].ToString();
                parameters[9].Value = respDic["subscribe_time"].ToString();
                parameters[10].Value = "";
                parameters[11].Value = respDic["groupid"].ToString();
                parameters[12].Value = respDic["tagid_list"].ToString();
                parameters[13].Value = respDic["subscribe_scene"].ToString();
                parameters[14].Value = respDic["qr_scene"].ToString();
                parameters[15].Value = respDic["qr_scene_str"].ToString();
                parameters[16].Value = respDic["remark"].ToString();

                int rows = SQLHelper.ExecuteNonQuery(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
            
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace wxweb
{
    public class SqlHelper
    {
        /// <summary>
        /// 批量操作每批次记录数
        /// </summary>
        public static int BatchSize = 2000;

        /// <summary>
        /// 超时时间
        /// </summary>
        public static int CommandTimeOut = 600;


        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (WebConfigurationManager.ConnectionStrings["DBPath"] == null)
                {
                    return ConfigurationManager.AppSettings["ConnectionStrings"];
                }
                else
                {
                    return WebConfigurationManager.ConnectionStrings["DBPath"].ConnectionString;
                }
            }
        }

        /// <summary>
        /// 是否记录log日志 追踪sql
        /// </summary>
        public static bool IsTraceSql
        {
            get
            {
                if (ConfigurationManager.AppSettings["TraceSql"] != null)
                {
                    return ConfigurationManager.AppSettings["TraceSql"].ToBoolean().Value;
                }
                return false;
            }
        }

        #region 实例方法

        //#region ExecuteNonQuery

        ///// <summary>
        ///// 执行SQL语句,返回影响的行数
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回影响的行数</returns>
        //public int ExecuteNonQuery(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteNonQuery(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回影响的行数
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回影响的行数</returns>
        //public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteNonQuery(ConnectionString, commandType, commandText, parms);
        //}

        //#endregion ExecuteNonQuery

        //#region ExecuteScalar

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一行第一列
        ///// </summary>
        ///// <typeparam name="T">返回对象类型</typeparam>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一行第一列</returns>
        //public T ExecuteScalar<T>(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteScalar<T>(ConnectionString, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一行第一列
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一行第一列</returns>
        //public object ExecuteScalar(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteScalar(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一行第一列
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一行第一列</returns>
        //public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteScalar(ConnectionString, commandType, commandText, parms);
        //}

        //#endregion ExecuteScalar

        //#region ExecuteDataReader

        ///// <summary>
        ///// 执行SQL语句,返回只读数据集
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回只读数据集</returns>
        //private SqlDataReader ExecuteDataReader(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataReader(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回只读数据集
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回只读数据集</returns>
        //private SqlDataReader ExecuteDataReader(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataReader(ConnectionString, commandType, commandText, parms);
        //}
        //#endregion

        //#region ExecuteDataRow

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一行
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一行</returns>
        //public DataRow ExecuteDataRow(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataRow(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一行
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一行</returns>
        //public DataRow ExecuteDataRow(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataRow(ConnectionString, commandType, commandText, parms);
        //}

        //#endregion ExecuteDataRow

        //#region ExecuteDataTable

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一个数据表
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一个数据表</returns>
        //public DataTable ExecuteDataTable(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataTable(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回结果集中的第一个数据表
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集中的第一个数据表</returns>
        //public DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataSet(ConnectionString, commandType, commandText, parms).Tables[0];
        //}

        //#endregion ExecuteDataTable

        //#region ExecuteDataSet

        ///// <summary>
        ///// 执行SQL语句,返回结果集
        ///// </summary>
        ///// <param name="commandText">SQL语句</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集</returns>
        //public DataSet ExecuteDataSet(string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataSet(ConnectionString, CommandType.Text, commandText, parms);
        //}

        ///// <summary>
        ///// 执行SQL语句,返回结果集
        ///// </summary>
        ///// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        ///// <param name="commandText">SQL语句或存储过程名称</param>
        ///// <param name="parms">查询参数</param>
        ///// <returns>返回结果集</returns>
        //public DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] parms)
        //{
        //    return ExecuteDataSet(ConnectionString, commandType, commandText, parms);
        //}

        //#endregion ExecuteDataSet

        //#region 批量操作

        ///// <summary>
        ///// 大批量数据插入
        ///// </summary>
        ///// <param name="table">数据表</param>
        //public void BulkInsert(DataTable table)
        //{
        //    BulkInsert(ConnectionString, table);
        //}

        ///// <summary>
        ///// 使用MySqlDataAdapter批量更新数据
        ///// </summary>
        ///// <param name="table">数据表</param>
        //public void BatchUpdate(DataTable table)
        //{
        //    BatchUpdate(ConnectionString, table);
        //}

        ///// <summary>
        ///// 分批次批量删除数据
        ///// </summary>
        ///// <param name="sql">SQL语句</param>
        ///// <param name="batchSize">每批次删除记录行数</param>
        ///// <param name="interval">批次执行间隔(秒)</param>
        //public void BatchDelete(string sql, int batchSize = 1000, int interval = 1)
        //{
        //    BatchDelete(ConnectionString, sql, batchSize, interval);
        //}

        ///// <summary>
        ///// 分批次批量更新数据
        ///// </summary>
        ///// <param name="sql">SQL语句</param>
        ///// <param name="batchSize">每批次更新记录行数</param>
        ///// <param name="interval">批次执行间隔(秒)</param>
        //public void BatchUpdate(string sql, int batchSize = 1000, int interval = 1)
        //{
        //    BatchUpdate(ConnectionString, sql, batchSize, interval);
        //}

        //#endregion 批量操作

        #endregion 实例方法

        #region 静态方法

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] parms)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            command.Connection = connection;
            command.CommandTimeout = CommandTimeOut;
            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;
            // 分配事务
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            // 设置命令类型.
            command.CommandType = commandType;
            //记录log日志
            if (IsTraceSql)
            {
                SqlLogHelper.Info("SqlHelper exectute sql-->" + commandText);
            }
            var parameterAppned = new StringBuilder();
            if (parms != null && parms.Length > 0)
            {
                //预处理SqlParameter参数数组，将为NULL的参数赋值为DBNull.Value;
                foreach (SqlParameter parameter in parms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    if (IsTraceSql)
                    {
                        parameterAppned.Append("@" + parameter.ParameterName + ":" + parameter.Value).AppendLine();
                    }
                }
                command.Parameters.AddRange(parms);
                if (IsTraceSql)
                {
                    //记录参数log
                    SqlLogHelper.Info("SqlHelper exectute sql paremeter-->" + parameterAppned);
                }
            }
        }

        #region ExecuteNonQuery

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteNonQuery(connection, CommandType.Text, commandText, parms);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteNonQuery(connection, commandType, commandText, parms);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(connection, null, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(transaction.Connection, transaction, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回影响的行数
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回影响的行数</returns>
        private static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, parms);
            int retval = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return retval;
        }

        #endregion ExecuteNonQuery

        #region ExecuteScalar

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static T ExecuteScalar<T>(string commandText, params SqlParameter[] parms)
        {
            object result = ExecuteScalar(commandText, parms);
            if (result != null)
            {
                return (T)Convert.ChangeType(result, typeof(T)); ;
            }
            return default(T);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteScalar(CommandType.Text, commandText, parms);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteScalar(connection, commandType, commandText, parms);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteScalar(connection, null, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteScalar(transaction.Connection, transaction, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        private static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, parms);
            object retval = command.ExecuteScalar();
            command.Parameters.Clear();
            return retval;
        }

        #endregion ExecuteScalar

        #region ExecuteDataTable

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        public static DataTable ExecuteDataTable(string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(CommandType.Text, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(commandType, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(connection, commandType, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(transaction, commandType, commandText, parms).Tables[0];
        }

        /// <summary>
        /// 获取空表结构
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns>返回结果集中的第一个数据表</returns>
        public static DataTable ExecuteEmptyDataTable(string tableName)
        {
            return ExecuteDataSet(CommandType.Text, string.Format("select * from {0} where 1=-1", tableName)).Tables[0];
        }

        /// <summary>
        /// 执行SQL语句,返回结果集中的第一行第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static int ExecuteCount(string commandText, params SqlParameter[] parms)
        {
            var pageRows = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT COUNT(1) FROM (" + commandText + ") as pagerrows";
                var obj = ExecuteScalar(CommandType.Text, sql, parms);
                pageRows = Convert.ToInt32(obj);
            }
            return pageRows;
        }

        /// <summary>
        ///  执行SQL语句,返回结果集中的第一个数据表
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="order">排序SQL,如"ORDER BY ID DESC"</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="parms">查询参数</param>      
        /// <param name="query">查询SQL</param>
        /// <param name="cte">CTE表达式</param>
        /// <returns></returns>
        public static DataTable ExecutePageDataTable(string sql, string order, int pageSize, int pageIndex, SqlParameter[] parms = null, string query = null, string cte = null)
        {
            string psql = string.Format(@"
                                        {3}
                                        SELECT  *
                                        FROM    (
                                                 SELECT ROW_NUMBER() OVER (ORDER BY {1}) RowNumber,*
                                                 FROM   (
                                                         {0}
                                                        ) t
                                                 WHERE  1 = 1 {2}
                                                ) t
                                        WHERE   RowNumber BETWEEN @RowNumber_Begin
                                                          AND     @RowNumber_End", sql, order, query, cte);

            List<SqlParameter> paramlist = new List<SqlParameter>()
            {
                new SqlParameter("@RowNumber_Begin", SqlDbType.Int){ Value = (pageIndex - 1) * pageSize + 1 },
                new SqlParameter("@RowNumber_End", SqlDbType.Int){ Value = pageIndex * pageSize }
            };
            if (parms != null) paramlist.AddRange(parms);
            return ExecuteDataTable(psql, paramlist.ToArray());
        }

        #endregion ExecuteDataTable

        #region ExecuteDataSet

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        public static DataSet ExecuteDataSet(string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(CommandType.Text, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return ExecuteDataSet(connection, commandType, commandText, parms);
            }
        }

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        public static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(connection, null, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            return ExecuteDataSet(transaction.Connection, transaction, commandType, commandText, parms);
        }

        /// <summary>
        /// 执行SQL语句,返回结果集
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">SQL语句或存储过程名称</param>
        /// <param name="parms">查询参数</param>
        /// <returns>返回结果集</returns>
        private static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parms)
        {
            SqlCommand command = new SqlCommand();

            PrepareCommand(command, connection, transaction, commandType, commandText, parms);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            if (commandText.IndexOf("@") > 0)
            {
                commandText = commandText.ToLower();
                int index = commandText.IndexOf("where ");
                if (index < 0)
                {
                    index = commandText.IndexOf("\nwhere");
                }
                if (index > 0)
                {
                    ds.ExtendedProperties.Add("SQL", commandText.Substring(0, index - 1));  //将获取的语句保存在表的一个附属数组里，方便更新时生成CommandBuilder
                }
                else
                {
                    ds.ExtendedProperties.Add("SQL", commandText);  //将获取的语句保存在表的一个附属数组里，方便更新时生成CommandBuilder
                }
            }
            else
            {
                ds.ExtendedProperties.Add("SQL", commandText);  //将获取的语句保存在表的一个附属数组里，方便更新时生成CommandBuilder
            }

            foreach (DataTable dt in ds.Tables)
            {
                dt.ExtendedProperties.Add("SQL", ds.ExtendedProperties["SQL"]);
            }

            command.Parameters.Clear();
            return ds;
        }

        #endregion ExecuteDataSet

        #region RunProcedure

        /// <summary>
        /// 构建 OleDbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, params IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }

        #endregion

        #region 包装参数

        /// <summary>
        /// 获取SqlParameter
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <returns>SqlParameter</returns>
        public static SqlParameter GetSqlParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        /// <summary>
        /// 获取SQLParameter的List
        /// </summary>
        /// <returns></returns>
        public static List<SqlParameter> GetSqlParamterList()
        {
            return new List<SqlParameter>();
        }

        #endregion

        #region 开始日期和结束日期的包装

        /// <summary>
        /// 开始日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string TransferBeginDate(string date)
        {
            return date;
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string TransferEndDate(string date)
        {
            return date + " 23:59";
        }

        #endregion

        #endregion 静态方法
    }
}

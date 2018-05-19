using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxweb
{
    public class SqlLogHelper
    {
        #region 属性

        private static ILog _fiLog = log4net.LogManager.GetLogger("applicationLog");

        #endregion

        #region INFO
        /// <summary>
        /// 往文件写入信息级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Info(object message)
        {
            _fiLog.Info(message);
        }

        /// <summary>
        /// 往文件写入信息级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public static void Info(object message, Exception exception)
        {
            _fiLog.Info(message, exception);
        }

        #endregion
    }
}

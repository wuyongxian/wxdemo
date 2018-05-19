using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxweb
{
    public class FrontendLogHelper
    {
        #region 属性

        private static ILog _fiLog = log4net.LogManager.GetLogger("frontendLog");

        #endregion

        #region ERROR
        /// <summary>
        /// 往文件写入错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Error(object message)
        {
            _fiLog.Error(message);
        }

        /// <summary>
        /// 往文件写入错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public static void Error(object message, Exception exception)
        {
            _fiLog.Error(message, exception);
        }
        #endregion

        #region Warning
        /// <summary>
        /// 往文件写入警告级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Waring(object message)
        {
            _fiLog.Warn(message);
        }

        /// <summary>
        /// 往文件写入警告级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public static void Waring(object message, Exception exception)
        {
            _fiLog.Warn(message, exception);
        }
        #endregion

        #region Debug
        /// <summary>
        /// 往文件写入调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Debug(object message)
        {
            _fiLog.Debug(message);
        }

        /// <summary>
        /// 往文件写入调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public static void Debug(object message, Exception exception)
        {
            _fiLog.Debug(message, exception);
        }
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

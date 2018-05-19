using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxPlatForm
{
    //public class EventMessage
    //{
    //    /// <summary>
    //    /// 开发者微信号
    //    /// </summary>
    //    public string ToUserName { get; set; }
    //    /// <summary>
    //    /// 发送方帐号（一个OpenID）
    //    /// </summary>
    //    public string FromUserName { get; set; }
    //    /// <summary>
    //    /// 消息创建时间 （整型）
    //    /// </summary>
    //    public string CreateTime { get; set; }

    //    ///// <summary>
    //    ///// 消息内容
    //    ///// </summary>
    //    //public string EventKey { get; set; }
    //}

    public class EventMessage:BaseMessage
    {
        public EventType EventType { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string EventKey { get; set; }

        public EventMessage(string tousername, string fromusername, string createtime, MsgType msgtype, EventType eventtype, string eventkey) {

            this.ToUserName = tousername;
            this.FromUserName = fromusername;
            this.CreateTime = createtime;
            this.MsgType = msgtype;
            this.EventType = eventtype;
            this.EventKey = eventkey;
        }
    }
}

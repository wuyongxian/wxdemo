using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxPlatForm
{
    public abstract  class BaseMessage
    {
        //public BaseMessage(string tousername,string fromusername,string createtime,MsgType msgtype) {
        //    this.ToUserName = tousername;
        //    this.FromUserName = fromusername;
        //    this.CreateTime = createtime;
        //    this.MsgType = msgtype;
        //}

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

       

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxPlatForm
{

    ///// <summary>
    ///// 文本 实体
    ///// </summary>
    //public class EventMessage : BaseMessage
    //{
        
    //    /// <summary>
    //    /// 消息内容
    //    /// </summary>
    //    public string EventKey { get; set; }



    //}

    /// <summary>
    /// 文本 实体
    /// </summary>
    public class TextMessage : BaseMessage
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        


    }

    /// <summary>
    /// 图片实体
    /// </summary>
    public class ImgMessage : BaseMessage
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicUrl { get; set; }
       
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }



    }

    /// <summary>
    /// 语音实体
    /// </summary>
    public class VoiceMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        public string Recognition { get; set; }


    }

    /// <summary>
    /// 视频实体
    /// </summary>
    public class VideoMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }


    }

    /// <summary>
    /// 链接实体
    /// </summary>
    public class LinkMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }


    }

    /// <summary>
    /// 地理位置实体
    /// </summary>
    public class LocationMessage : BaseMessage
    {
        /// <summary>
        /// 地理位置ID
        /// </summary>
        public string MsgId { get; set; }
        

        /// <summary>
        /// X坐标
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// Scale
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// CDATA[位置信息]
        /// </summary>
        public string Label { get; set; }
    }
}

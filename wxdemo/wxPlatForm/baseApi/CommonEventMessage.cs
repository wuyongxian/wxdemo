using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxPlatForm
{
    ///// <summary>
    ///// 订阅/取消订阅事件
    ///// </summary>
    //public class SubEventMessage : EventMessage
    //{
    //    private string _eventkey;
    //    /// <summary>
    //    /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值（已去掉前缀，可以直接使用）
    //    /// </summary>
    //    public string EventKey
    //    {
    //        get { return _eventkey; }
    //        set { _eventkey = value.Replace("qrscene_", ""); }
    //    }
    //    /// <summary>
    //    /// 二维码的ticket，可用来换取二维码图片
    //    /// </summary>
    //    public string Ticket { get; set; }

    //}

    ///// <summary>
    ///// 扫描带参数的二维码实体
    ///// </summary>
    //public class ScanEventMessage : EventMessage
    //{

    //    /// <summary>
    //    /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
    //    /// </summary>
    //    public string EventKey { get; set; }
    //    /// <summary>
    //    /// 二维码的ticket，可用来换取二维码图片
    //    /// </summary>
    //    public string Ticket { get; set; }

    //}

    ///// <summary>
    ///// 上报地理位置实体
    ///// </summary>
    //public class LocationEventMessage : EventMessage
    //{

    //    /// <summary>
    //    /// 地理位置纬度
    //    /// </summary>
    //    public string Latitude { get; set; }
    //    /// <summary>
    //    /// 地理位置经度
    //    /// </summary>
    //    public string Longitude { get; set; }
    //    /// <summary>
    //    /// 地理位置精度
    //    /// </summary>
    //    public string Precision { get; set; }

    //}

    ///// <summary>
    ///// 普通菜单事件，包括click和view
    ///// </summary>
    //public class NormalMenuEventMessage : EventMessage
    //{

    //    /// <summary>
    //    /// 事件KEY值，设置的跳转URL
    //    /// </summary>
    //    public string EventKey { get; set; }


    //}

    ///// <summary>
    ///// 菜单扫描事件
    ///// </summary>
    //public class ScanMenuEventMessage : EventMessage
    //{

    //    /// <summary>
    //    /// 事件KEY值
    //    /// </summary>
    //    public string EventKey { get; set; }
    //    /// <summary>
    //    /// 扫码类型。qrcode是二维码，其他的是条码
    //    /// </summary>
    //    public string ScanType { get; set; }
    //    /// <summary>
    //    /// 扫描结果
    //    /// </summary>
    //    public string ScanResult { get; set; }
    //}


}

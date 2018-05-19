using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wxPlatForm
{
    public class Picture
    {
        #region 属性
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQPicUrl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}

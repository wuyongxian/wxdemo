﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wxPlatForm.OAuth
{
    /// <summary>  
    /// 授权之后获取用户基本信息  
    /// </summary>  
    public class OAuthUser
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        /// <summary>  
        /// 用户特权信息，json 数组  
        /// </summary>  
        //public JArray privilege { get; set; }
        public string privilege { get; set; }
        public string unionid { get; set; }
    }
}

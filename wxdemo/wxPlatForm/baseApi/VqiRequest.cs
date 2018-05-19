using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace wxPlatForm
{
   public  class VqiRequest
    {
        public static Stream GetInputStream() {
            return System.Web.HttpContext.Current.Request.InputStream;
        }

        public static object GetQueryString(string key) {
            return System.Web.HttpContext.Current.Request.QueryString[key];
        }
    }
}

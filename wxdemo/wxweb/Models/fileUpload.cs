using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wxweb.Models
{
    public class fileUpload
    {
        public bool Result { get; set; }
        public int Count { get; set; }
        public string filePath { get; set; }
        public string Message { get; set; }
        public string guid { get; set; }
    }
}
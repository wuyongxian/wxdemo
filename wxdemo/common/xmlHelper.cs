﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace common
{
    public class xmlHelper
    {
        public static XmlDocument getXML(string filepath)
        {

            StreamReader str = new StreamReader(filepath, Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            return xml;
        }
    }
}
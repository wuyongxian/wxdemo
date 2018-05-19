using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace wxweb
{
    /// <summary>
    /// 转换Helper
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        ///     值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this String value)
        {
            if (value == null || String.IsNullOrEmpty(value.Trim()) || Equals(value.Trim().ToLower(), "system.object"))
                return null;
            if (Equals(value.Trim(), "1"))
                return true;
            if (Equals(value.Trim().ToLower(), "true"))
                return true;
            if (Equals(value.Trim(), "0"))
                return false;
            if (Equals(value.Trim().ToLower(), "false"))
                return false;
            return null;
        }

        /// <summary>
        ///     值为1返回true其他返回false
        /// </summary>
        /// <param name="value">整型值</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this int value)
        {
            if (value == 1)
                return true;
            return ToBoolean(value.ToString());
        }

        /// <summary>
        ///     值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this object value)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return null;
            return ToBoolean(value.ToString());
        }

        /// <summary>
        /// 值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean ToBooleanNull(this object value)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return false;
            if (Equals(value.ToString().Trim(), "1"))
                return true;
            if (Equals(value.ToString().Trim().ToLower(), "true"))
                return true;
            return false;
        }

        /// <summary>
        /// object类型转化为string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串值</returns>
        public static String ToStringEx(this object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;

            return obj.ToString().Trim();
        }

        /// <summary>
        ///     截取strFormer中长度为subLength的字符串，如果strFormer本身的长度小于subLength,返回strFormer
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="subLength">长度</param>
        /// <returns>字符串</returns>
        public static string ToSubString(this object obj, int subLength)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;

            if (obj.ToString().Trim().Length <= subLength)
                return obj.ToString().Trim();
            return obj.ToString().Trim().Substring(0, subLength) + "..";
            // return obj.ToString().Trim();
        }

        /// <summary>
        ///     转换为整型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this object obj)
        {
            if (obj == null)
                return null;
            return ToInt32(obj.ToString());
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this bool? obj)
        {
            switch (obj)
            {
                case true:
                    return 1;
                case false:
                    return 0;

            }
            return null;
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this String obj)
        {
            int i;
            if (Int32.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this float obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this double obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this UInt32 obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">long对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this long obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">decimal对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this decimal obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     转换为长整型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this object obj)
        {
            if (obj == null)
                return null;

            return ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this String obj)
        {
            if (obj == null)
                return null;

            long i;
            if (Int64.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">int对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this int obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this float obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this double obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this UInt32 obj)
        {
            return Convert.ToInt64(obj);
        }


        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空decimal</returns>
        public static decimal? ToDecimal(this object obj)
        {
            if (obj == null)
                return null;
            return ToDecimal(obj.ToString());
        }


        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空decimal</returns>
        public static decimal? ToDecimal(this String obj)
        {
            if (obj == null || obj == "")
                return null;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空decimal</returns>
        public static decimal ToDecimalNullDefaultZero(this String obj)
        {
            if (obj == null || obj == "")
                return 0;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal ToDecimalNotNull(this String obj)
        {
            decimal i;
            if (!string.IsNullOrEmpty(obj) && decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal? ToDecimalZeroDefultNull(this String obj)
        {
            if (obj == null || obj == "")
                return null;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                if (i == 0)
                {
                    return null;
                }
                else
                {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal? ToDecimalZeroDefultNull(this Decimal obj)
        {
            if (obj == 0)
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static int? ToDecimalZeroDefultNull(this int obj)
        {
            if (obj == 0)
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">Int对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this int obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this float obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this double obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this UInt32 obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为3位用逗号隔开的方式
        /// </summary>
        /// <param name="amount">金额额</param>
        /// <returns>显示值</returns>
        public static string ToAmount(this object amount, int precision = 2)
        {
            string strAmount = amount.ToStringEx();
            if (string.IsNullOrWhiteSpace(strAmount))
            {
                return string.Empty;
            }
            //转换
            double doubleAmount;
            bool isDouble = double.TryParse(strAmount, out doubleAmount);
            if (isDouble == false)
            {
                return string.Empty;
            }
            return string.Format("{0:N" + precision + "}", doubleAmount);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">long对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this long obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为3位用逗号隔开的方式
        /// </summary>
        /// <param name="amount">金额额</param>
        /// <returns>显示值</returns>
        public static string ToAmountNotShowZero(this object amount, int precision = 0)
        {
            string strAmount = amount.ToStringEx();
            if (string.IsNullOrWhiteSpace(strAmount))
            {
                return string.Empty;
            }
            //转换
            double doubleAmount;
            bool isDouble = double.TryParse(strAmount, out doubleAmount);
            if (isDouble == false)
            {
                return string.Empty;
            }
            //不显示0
            if (doubleAmount == 0)
            {
                return string.Empty;
            }
            return string.Format("{0:N" + precision + "}", doubleAmount);
        }

        /// <summary>
        ///     转换为日期型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空DateTime</returns>
        public static DateTime? ToDateTime(this object obj)
        {
            if (obj == null)
                return null;
            return ToDateTime(obj.ToString());
        }

        /// <summary>
        ///  转换为日期型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空DateTime</returns>
        public static DateTime? ToDateTime(this String obj)
        {
            if (obj == null)
                return null;
            DateTime i;
            if (DateTime.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateString(this object obj, string language = "zh")
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                if (language == "zh")
                {
                    return date.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    return date.Value.ToString("dd-MM-yyyy");
                }
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateStringEx(this object obj, string language = "zh")
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                if (language == "zh")
                {
                    return date.Value.ToString("yyyyMMdd");
                }
                else
                {
                    return date.Value.ToString("ddMMyyyy");
                }
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>
        ///     如果日期是1753/01/01 00:00:00（数据库最小时间）则返回空字符串；
        ///     其他情况返回yyyy-MM-dd HH:mm:ss格式日期字符串
        /// </returns>
        public static String ToDateTimeString(this object obj, string language = "zh")
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                if (language == "zh")
                {
                    return date.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return date.Value.ToString("ss:mm:HH dd-MM-yyyy");
                }
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>
        ///     如果日期是1753/01/01 00:00:00（数据库最小时间）则返回空字符串；
        ///     其他情况返回yyyy-MM-dd HH:mm:ss格式日期字符串
        /// </returns>
        public static String ToDateTimeStringEx(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyyMMddHHmmss");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">DateTime对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateString(this DateTime obj, string language = "zh")
        {
            if (language == "zh")
            {
                return obj.ToString("yyyy-MM-dd");
            }
            else
            {
                return obj.ToString("dd-MM-yyyy");
            }
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd HH:mm:ss格式日期字符串</returns>
        public static String ToYearMonthDateString(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">DateTime对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToYearMonthDateString(this DateTime obj)
        {
            return obj.ToString("yyyy-MM");
        }

        /// <summary>
        /// 金额大写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MoneyToChinese(this string value)
        {
            #region 转换方法
            string LowerMoney = value;
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }


        /// <summary>
        /// 生成Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

    }
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace wxweb
{
    /// <summary>
    /// ���ú���
    /// </summary>
    public class Fun
    {

         #region  ���ݹ���
        /// <summary>
        /// ����Σ���ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeData(string str)//�����ݹ���
        {
            string tempStr = str;
            if (tempStr != null)
            {
                tempStr = tempStr.Replace("<", "");
                tempStr = tempStr.Replace(">", "");
                tempStr = tempStr.Replace("'", "��");
                tempStr = tempStr.Replace("\"", "");
            }
            return tempStr;
        }
        /// <summary>
        /// ����Σ���ַ���HTML
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeDataHTML(string str)//�����ݹ���
        {
            string tempStr = str;
            if (tempStr != null)
            {
                tempStr = tempStr.Replace("'", "��");
            }
            return tempStr;
        }

        /// <summary>
        /// �ַ�����ȡ
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutString(object input, int len)
        {
            if (input == null)
                return "";

            string inputString = input.ToString();
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //����ع�����ϰ��ʡ�Ժ�   
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";

            return tempString;
        }

        /// <summary>
        /// ��ȥ�ַ����Ŀ�ͷ�ͽ�β���ַ� 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CutString_Head(object obj)
        {
            obj = obj.ToString().Substring(1);
            obj = obj.ToString().Substring(0, obj.ToString().Length - 1);
            return obj.ToString();
        }
        #endregion 

         #region ����������֤
        /// <summary>
        /// ��֤���ݹ����Ĳ����Ƿ�������
        /// </summary>
        /// <param name="objName"></param>
        /// <returns>trueΪ����</returns>
        public static bool RequestIsNumber(string objName)
        {
            object obj = System.Web.HttpContext.Current.Request.QueryString[objName];
            if (obj != null && IsNumberR(obj.ToString()))
                return true;
            else
                return false;
        }
        /// <summary>
        /// ���obj�Ƿ�Ϊ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            if (obj == null)
                return true;
            else
                return false;
        }

        public static bool IsNumber(string lstr)
        {
            bool isDecimal = false;
            for (int i = 0; i < lstr.Length; i++)
            {
                char ochar = lstr[i];
                if (i == 0 && ochar == '-')
                    continue;
                if (ochar == '.' && !isDecimal)
                {
                    isDecimal = true;
                    continue;
                }
                if (ochar < '0' || ochar > '9')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsInt(string str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumberR(string lstr)
        {
            bool IsNum = Regex.IsMatch(lstr, @"^\d+$");
            return IsNum;
        }

        public static bool IsDateTime(string str)
        {
            try
            {
                Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
 
         #region ��ȡҳ�洫�ݲ���
        /// <summary>
        /// ��ȡget��ʽ�ύ������url�ύ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Query(string key)
        {
            if (HttpContext.Current.Request.QueryString[key] == null)
                return "";
            else
                return HttpContext.Current.Request.QueryString[key].ToString();
        }

        /// <summary>
        /// ��ȡpost��ʽ�ύ�Ĳ��� 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Form(string key)
        {
            if (HttpContext.Current.Request.Form[key] == null)
            {
                return "";
            }
            else {
                return HttpContext.Current.Request.Form[key].ToString();
            }
        }
        #endregion

         #region MD5
        /// <summary>
        ///32λ md5����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5_32(string str)
        {
            byte[] arrHashInput;
            byte[] arrHashOutput;
            //MD5CryptoServiceProvider objMD5  = new MD5CryptoServiceProvider();
            MD5 objMD5 = new MD5CryptoServiceProvider();
            arrHashInput = C2B(str);
            arrHashOutput = objMD5.ComputeHash(arrHashInput);
            return BitConverter.ToString(arrHashOutput);
        }
        protected static byte[] C2B(string str)
        {
            char[] arrChar;
            arrChar = str.ToCharArray();
            byte[] arrByte = new byte[arrChar.Length];
            for (int i = 0; i < arrChar.Length; i++)
            {
                arrByte[i] = Convert.ToByte(arrChar[i]);
            }
            return arrByte;
        }

        /// <summary>
        /// 16λ MD5����
        /// </summary>
        /// <param name="sInputString"></param>
        /// <returns></returns>
        public static string GetMD5_16(string sInputString)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            string sKey = GenerateKey();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }

        // ����Key
        public static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
        // �����ַ���
        public static string EncryptString(string sInputString, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        // �����ַ���
        public static string DecryptString(string sInputString, string sKey)
        {
            string[] sInput = sInputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
        #endregion

        #region ��ȡip��ַ
        public static string getIp()
        {
            try
            {
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                string ip2 = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch { }

            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        #endregion 

        #region �ؼ��༭����
        /// <summary>
        /// ΪDropDownList�����
        /// </summary>
        /// <param name="ddl">��ǰ�༭��DropDownList�ؼ�</param>
        /// <param name="dt">�󶨵�����Դ</param>
        /// <param name="strText">ListItem��Text</param>
        /// <param name="strValue">ListItem��Value</param>
        public static void SelectOptionAdd(DropDownList ddl, DataTable dt, string strText, string strValue) {
            if (dt != null && dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i][strText].ToString(), dt.Rows[i][strValue].ToString()));
                }
                ddl.SelectedIndex = 0;
            }
        }

        public static void SetOption(DropDownList ddl, int s, int e, string tip)
        {
            if (s > e)
            {
                for (int i = e; i <= s; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            else
            {
                for (int i = e; i >= s; i--)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            if (tip != null)
            {
                ddl.Items.Insert(0, new ListItem(tip, ""));
            }
        }
        #endregion

        /// <summary>
        /// ����������ת��Ϊstring����
        /// </summary>
        /// <param name="num">��Ҫת��������</param>
        /// <param name="strLength">Ҫת���ɵ��ַ�������</param>
        /// <returns></returns>
        public static string ConvertIntToString(int num, int strLength)
        {
            string strNum=num.ToString();
            string strReturn = "";
            for (int i = 0; i < strLength - strNum.Length; i++)
            {
                strReturn += "0";
            }

            return strReturn+strNum;
        }
       

        /// <summary>
        /// ��ȡ�ļ�������
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExName(string fileName)
        {
            int dotPos = fileName.LastIndexOf(".");
            return fileName.Substring(dotPos);
        }

        /// <summary>
        /// ʱ��ת����SN
        /// </summary>
        /// <returns></returns>
        public static string GetSN()
        {
            string _sn = (DateTime.Now).ToString();
            _sn = _sn.Replace("-", "");
            _sn = _sn.Replace(":", "");
            _sn = _sn.Replace(" ", "");
            _sn = _sn.Replace("/", "");
            return _sn;

        }


        public static string getGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString();
            return str;
        }

        /// <summary>
        /// ת������Ҵ�С���
        /// </summary>
        /// <param name="num">���</param>
        /// <returns>���ش�д��ʽ</returns>
        public static string ChangeToRMB(decimal num)
        {
            string str1 = "��Ҽ��������½��ƾ�";            //0-9����Ӧ�ĺ���
            string str2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�"; //����λ����Ӧ�ĺ���
            string str3 = "";    //��ԭnumֵ��ȡ����ֵ
            string str4 = "";    //���ֵ��ַ�����ʽ
            string str5 = "";  //����Ҵ�д�����ʽ
            int i;    //ѭ������
            int j;    //num��ֵ����100���ַ�������
            string ch1 = "";    //���ֵĺ������
            string ch2 = "";    //����λ�ĺ��ֶ���
            int nzero = 0;  //����������������ֵ�Ǽ���
            int temp;            //��ԭnumֵ��ȡ����ֵ

            num = Math.Round(Math.Abs(num), 2);    //��numȡ����ֵ����������ȡ2λС��
            str4 = ((long)(num * 100)).ToString();        //��num��100��ת�����ַ�����ʽ
            j = str4.Length;      //�ҳ����λ
            if (j > 15) { return "���"; }
            str2 = str2.Substring(15 - j);   //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����str2=��ʰԪ�Ƿ�

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //ȡ����ת����ĳһλ��ֵ
                temp = Convert.ToInt32(str3);      //ת��Ϊ����
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "��" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "��" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //�����λ����λ��Ԫλ�������д��
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //���һλ���֣�Ϊ0ʱ�����ϡ�����
                    str5 = str5 + '��';
                }
            }
            if (num == 0)
            {
                str5 = "��Ԫ��";
            }
            return str5;
        }

        /**/
        /// <summary>
        /// һ�����أ����ַ�����ת���������ڵ���CmycurD(decimal num)
        /// </summary>
        /// <param name="num">�û�����Ľ��ַ�����ʽδת��decimal</param>
        /// <returns></returns>
        public static string ChangeToRMB(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return ChangeToRMB(num);
            }
            catch
            {
                return "��������ʽ��";
            }
        }

    }
}

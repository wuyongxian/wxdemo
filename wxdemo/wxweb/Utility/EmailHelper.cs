using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;


namespace wxweb
{
    public class EmailHelper
    {
        public const string BACKEND_USERNAME = "[backend:username]";
        public const string BACKEND_USERCODE = "[backend:usercode]";
        public const string BACKEND_SITEURL = "[backend:siteurl]";
        public const string BACKEND_NEWPASSWORD = "[backend:newpassword]";

        public const string SITE_USERNAME = "[site:username]";
        public const string SITE_USERCODE = "[site:usercode]";
        public const string SITE_SITEURL = "[site:siteurl]";
        // 新密码--订单金额
        public const string DONATE_ORDERAMOUNT = "[donate:orderamount]";
        /// 新密码--订单号
        public const string DONATE_ORDERNO = "[donate:orderno]";
        /// 新密码--前台用
        public const string SITE_NEWPASSWORD = "[site:newpassword]";
        //订单用户邮箱
        public const string SITE_USEREMAIL = "[site:useremail]";
        //订单用户手机号码
        public const string SITE_USERMOBILE = "[site:usermobile]";
        //订单支付方式
        public const string DONATE_PAYMETHOD = "[donate:paymethod]";
        //捐赠项目
        public const string DONATE_DONATIONNAME = "[donate:donationname]";
        //捐赠项目
        public const string DONATE_DONATIONNAME_EN = "[donate:donationname_en]";
        // 留言内容
        public const string DONATE_MESSAGECONTENT = "[donate:messagecontent]";
        //回复内容
        public const string DONATE_REPLYMESSAGE = "[donate:replymessage]";
        //捐赠项目进度
        public const string DONATE_DONATIONPROGRESS = "[donate:donationprogress]";
        //时间
        public const string DONATE_DATE = "[donate:Date]";
        //英文时间
        public const string DONATE_DATE_EN = "[donate:Date_en]";
        //收据抬头
        public const string DONATE_RECEIPTTITLE = "[donate:ReceiptTitle]";
        //大写金额
        public const string DONATE_ORDERAMOUNTCAPITAL = "[donate:orderamountCapital]";

        public const string URLHREF = "<a href=\"{0}\">{0}</a>";
        public const string URLREPLCE = "{0}";
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="_to">_to</param>
        /// <param name="_subject">_subject</param>
        /// <param name="_body">_body</param>
        /// <returns>bool</returns>
        public static bool sendMail(string _to, string _subject, string _body, string _cc, string attachment1 = "", string attachment2 = "")
        {
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            //string _from = settings.Smtp.From;
            //string _server = settings.Smtp.Network.Host;
            //string _frompassword = settings.Smtp.Network.Password;
            //string _username = settings.Smtp.Network.UserName;

            string _from = ConfigurationManager.AppSettings["from"];
            string _server = ConfigurationManager.AppSettings["host"];
            string _frompassword = ConfigurationManager.AppSettings["password"];
            string _username = ConfigurationManager.AppSettings["userName"];
            try
            {
                MailMessage mailObj = new MailMessage(_from, _to);
                SmtpClient smtp1 = new SmtpClient(_server);
                smtp1.Host = _server;
                smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp1.Credentials = new System.Net.NetworkCredential(_username, _frompassword);
                mailObj.Subject = _subject;
                mailObj.Body = _body;
                mailObj.BodyEncoding = System.Text.Encoding.UTF8;
                mailObj.IsBodyHtml = true;
                mailObj.Priority = MailPriority.High;
                if (_cc != "")
                {
                    mailObj.CC.Add(_cc);
                }
                if (!string.IsNullOrWhiteSpace(attachment1))
                {
                    if (File.Exists(attachment1))
                    {
                        mailObj.Attachments.Add(new Attachment(attachment1));
                    }
                }
                if (!string.IsNullOrWhiteSpace(attachment2))
                {
                    if (File.Exists(attachment2))
                    {
                        mailObj.Attachments.Add(new Attachment(attachment2));
                    }
                }
                //string GetCurrentDirectory = Directory.GetCurrentDirectory();
                //string str3 = GetCurrentDirectory + @"\file\c.docx";
                //string str4 = GetCurrentDirectory + @"\file\f.docx";   
                //mailObj.Attachments.Add(new Attachment(str3));
                //mailObj.Attachments.Add(new Attachment(str4));
                smtp1.Send(mailObj);
                return true;
            }
            catch (Exception ex)
            {
                ///记入LOG
               // BackendLogHelper.Error("邮件异常-->" + ex.Message + ex.InnerException.Message);
                return false;
            }
        }
    }
}

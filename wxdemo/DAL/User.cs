//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public string ID { get; set; }
        public string UserGroupID { get; set; }
        public string sRealName { get; set; }
        public string sLoginName { get; set; }
        public string sPassword { get; set; }
        public string sHeaderPhoto { get; set; }
        public string sPhoneNO { get; set; }
        public string sAddress { get; set; }
        public Nullable<int> sGender { get; set; }
        public string remark { get; set; }
        public bool numstate { get; set; }
        public Nullable<System.DateTime> dtCreate { get; set; }
        public Nullable<System.DateTime> dtUpdate { get; set; }
    }
}
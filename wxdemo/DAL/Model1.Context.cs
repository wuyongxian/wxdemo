﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PHWXEntities : DbContext
    {
        public PHWXEntities()
            : base("name=PHWXEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<subscriber> subscribers { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserMenu> UserMenus { get; set; }
        public virtual DbSet<UserMenuAuthority> UserMenuAuthorities { get; set; }
        public virtual DbSet<UserOperateLog> UserOperateLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<wxmsmResponse> wxmsmResponses { get; set; }
    }
}
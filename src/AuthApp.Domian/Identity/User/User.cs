using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VCrisp.Domain.Abstractions;

namespace AuthApp.Domian.Identity
{
    public class User : Entity<int>, IAggregateRoot
    {
        public User() { }

        public User(string loginName, string loginPWD)
        {
            UserLoginName = loginName;
            UserLoginPWD = loginPWD;
            UserRealName = UserLoginName;
            UserStatus = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            LastErrTime = DateTime.Now;
            ErrorCount = 0;
            Name = "";

        }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserLoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserLoginPWD { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int UserStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string UserRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime LastErrTime { get; set; } = DateTime.Now;

        /// <summary>
        ///错误次数 
        /// </summary>
        public int ErrorCount { get; set; }


        /// <summary>
        /// 登录账号
        /// </summary>
        public string Name { get; set; }

        // 性别
        public int? Sex { get; set; } = 0;
        // 年龄
        public int? Age { get; set; }
        // 生日
        public DateTime? Birth { get; set; } = DateTime.Now;
        // 地址
        public string Addr { get; set; }

        public bool? TdIsDelete { get; set; }


        [NotMapped]
        public List<string> RoleNames { get; set; }
    }
}

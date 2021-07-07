using AuthApp.Domian.Identity;
using System;
using System.Collections.Generic;

namespace AuthApp.Users.Dto
{
    /// <summary>
    /// 输出DTO:用户信息
    /// </summary>
    public class UserOutputDto 
    {
        /// <summary>
        /// 初始化一个<see cref="UserOutputDto"/>类型的新实例
        /// </summary>
        public UserOutputDto()
        { }

        /// <summary>
        /// 初始化一个<see cref="UserOutputDto"/>类型的新实例
        /// </summary>
        public UserOutputDto(User u)
        {
            Id = u.Id;
            UserLoginName = u.UserLoginName;
            UserLoginPWD = u.UserLoginPWD;
            UserRealName = u.UserRealName;
            UserStatus = u.UserStatus;
            UserRemark = u.UserRemark;
            ErrorCount = u.ErrorCount;
            Name = u.Name;
            Sex = u.Sex;
            Age = u.Age;
            Birth = u.Birth;
            Addr = u.Addr;
            TdIsDelete = u.TdIsDelete;
        }
        public int Id { get; set; }
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
        public DateTime? Birth { get; set; }
        // 地址
        public string Addr { get; set; }

        public bool? TdIsDelete { get; set; }
        public List<int> RIDs { get; set; }
        public List<string> RoleNames { get; set; }

    }
}
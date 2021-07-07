
using System;
using System.Collections.Generic;

namespace AuthApp.Users.Dto
{
    /// <summary>
    /// 输入DTO：用户信息
    /// </summary>
    public class UserInputDto 
    {
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
        public int Sex { get; set; } = 0;
        // 年龄
        public int Age { get; set; }
        // 生日
        public DateTime Birth { get; set; }
        // 地址
        public string Addr { get; set; }

        public bool TdIsDelete { get; set; }
        public List<int> RIDs { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
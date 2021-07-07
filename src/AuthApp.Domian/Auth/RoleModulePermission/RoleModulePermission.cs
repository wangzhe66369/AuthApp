using AuthApp.Domian.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCrisp.Domain.Abstractions;

namespace AuthApp.Domian.Auth
{
    public class RoleModulePermission : Entity<int>,IAggregateRoot
    {

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public int ModuleId { get; set; }
        /// <summary>
        /// api ID
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 创建ID
        /// </summary>
        public int? CreateId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改ID
        /// </summary>
        public int? ModifyId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; } = DateTime.Now;

        // 下边三个实体参数，只是做传参作用，所以忽略下
        [NotMapped]
        public Role Role { get; set; }
        [NotMapped]
        public ModulePermission Module { get; set; }
        [NotMapped]
        public Permission Permission { get; set; }

    }
}

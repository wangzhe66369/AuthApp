using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AuthApp.Roles.Dto
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        
        //[Required]
        //[StringLength(64)]
        //public string DisplayName { get; set; }

        //public string NormalizedName { get; set; }
        
        //[StringLength(5000)]
        //public string Description { get; set; }

        //public List<string> GrantedPermissions { get; set; }

        //public CreateRoleDto()
        //{
        //    GrantedPermissions = new List<string>();
        //}
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Domian;
using AuthApp.Identity.Roles;
using AuthApp.MessageDtos;
using AuthApp.Roles.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VCrisp.Extensions.BaseType;

namespace AuthApp.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public  async Task<MessageDto<RoleDto>> CreateAsync(CreateRoleDto input)
        {
            MessageDto<RoleDto> messageDto = new MessageDto<RoleDto>();
            Role role = new Role
            {
                Name = input.Name
            };

            //将角色保存在AspNetRoles表中
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                messageDto.response = _mapper.Map<RoleDto>(role);
                messageDto.success = true;
                messageDto.msg = "添加成功";
            }else
            {
                messageDto.response = _mapper.Map<RoleDto>(role);
                messageDto.success = false;
                messageDto.msg = result.Errors.Select((IdentityError err) => err.Description).JoinAsString();
            }

            return messageDto;
        }
    }
}


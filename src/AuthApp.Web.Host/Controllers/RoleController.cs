using AuthApp.Authorization.Roles;
using AuthApp.Domian;
using AuthApp.MessageDtos;
using AuthApp.Roles;
using AuthApp.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _iRoleService;
        public RoleController(IRoleService iRoleService)
        {
            _iRoleService = iRoleService;
        }


        //public async Task<MessageDto<RoleDto>> CreateAsync([FromBody] CreateRoleDto input)
        //{
        //    return await _iRoleService.CreateAsync(input);
        //}


        // GET: api/<RolesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RolesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

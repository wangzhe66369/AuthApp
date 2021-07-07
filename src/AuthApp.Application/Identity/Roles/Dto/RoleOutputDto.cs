// -----------------------------------------------------------------------
//  <copyright file="RoleOutputDto.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-27 4:44</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;


namespace AuthApp.Roles.Dto
{
    /// <summary>
    /// 角色输出DTO
    /// </summary>
    public class RoleOutputDto 
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool Enabled { get; set; }
       
    }
}
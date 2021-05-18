﻿using AuthApp.Authors;
using AuthApp.Authors.Dto;
using AuthApp.Configuration;
using AuthApp.Domian;
using AuthApp.Domian.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/authors")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        public AuthorController(IOptionsMonitor<JwtConfig> optionsMonitor, IAuthorRepository authorRepository, IMapper mapper, IAuthorService authorService)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _authorService = authorService;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost()]
        public async Task<ActionResult> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto)
        {
            var author = _mapper.Map<Author>(authorForCreationDto);

            _authorRepository.Create(author);
            var result = await _authorRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源author失败");
            }

            var authorCreated = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute(nameof(GetAuthorAsync),
                new { authorId = authorCreated.Id },
                authorCreated);
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> DeleteAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.Delelte(author);
            var result = await _authorRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源author失败");
            }

            return NoContent();
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthorAsync))]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }

            var authorDto = _mapper.Map<AuthorDto>(author);
            return authorDto;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsAsync()
        {
            var pp = HttpContext.User;
            var authors = (await _authorRepository.GetAllAsync())
                .OrderBy(author => author.Name);
            
            var authorDtoList = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return authorDtoList.ToList();
        }
    }
}
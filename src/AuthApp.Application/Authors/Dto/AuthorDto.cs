﻿using System;

namespace AuthApp.Authors.Dto
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string BirthPlace { get; set; }
        public string Email { get; set; }

    }
}
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.Core.Const;
using System.ComponentModel.DataAnnotations;

namespace MyBook.Core.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Author : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyBook.Core.Const;
using System.ComponentModel.DataAnnotations;

namespace MyBook.Core.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = Error.MaxLength), Display(Name = "Category")]
        [Remote("IsAllowed", null, AdditionalFields = "Id", ErrorMessage = Error.Duplicated)]
        public string? Name { get; set; }

    }
}

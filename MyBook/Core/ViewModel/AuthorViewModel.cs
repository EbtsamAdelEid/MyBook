using Microsoft.AspNetCore.Mvc;
using MyBook.Core.Const;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyBook.Core.ViewModel
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = Error.MaxLength), Display(Name = "Author")]
        [Remote("IsAllowed", null, AdditionalFields = "Id", ErrorMessage = Error.Duplicated)]
        public string? Name { get; set; }
    }
}

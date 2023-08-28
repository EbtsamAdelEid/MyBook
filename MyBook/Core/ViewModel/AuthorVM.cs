using System.ComponentModel.DataAnnotations;

namespace MyBook.Core.ViewModel
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}

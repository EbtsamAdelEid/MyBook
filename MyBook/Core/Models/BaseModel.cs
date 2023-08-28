namespace MyBook.Core.Models
{
    public class BaseModel
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}

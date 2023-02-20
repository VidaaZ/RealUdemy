using System.ComponentModel.DataAnnotations;

namespace RealPractice.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Test";
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}

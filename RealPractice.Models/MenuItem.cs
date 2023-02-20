using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RealPractice.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(1, 1000, ErrorMessage = "Price should be between $1 and $1000")]
        public double Price { get; set; }
        [Display(Name= "Food Type")]
        public int FoodTypeId { get; set; } //foreignkey of FoodType
        [ForeignKey("FoodTypeId")]
        public FoodType FoodType { get; set; } //we create a foodtype obj here(foodtype is in the foodtype table)-->be in navigation property migan
        [Display(Name = "Category")]
        public int CategoryId { get; set; } //foreignkey Of Category
        public Category Category { get; set; } //we create a category obj here--> be in navigation property migan

    }
}

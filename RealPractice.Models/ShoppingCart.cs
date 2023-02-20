using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPractice.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }
        public int MenuItemId { get; set; } //in property baraye in tarif kardim ke moshakhas kone kodom menuitem user be shopping cart ezafe karde
        [ForeignKey("MenuItemId")]
        
        [ValidateNever]
        public MenuItem MenuItem { get; set; } //navigation property
        [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; } //az noe string tarif kardim chon to table ASPNetUser az noe string
        [ForeignKey("ApplicationUserId")]
        
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } //navigation property
    }
}

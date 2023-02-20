﻿using System.ComponentModel.DataAnnotations;

namespace RealPractice.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Test";
       
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Hoodies
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public int Price { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public int Size { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }
    }
}
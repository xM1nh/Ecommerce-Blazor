﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required, Range(0.1, 99999.99)]
    public decimal Price { get; set; }

    [DisplayName("Product Image")]
    public string? Base64Img { get; set; }

    [Required, Range(0, 99999)]
    public int Quantity { get; set; }

    public bool Featured { get; set; } = false;

    public DateTime DateUploaded { get; set; } = DateTime.Now;

    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category? Category { get; set; }
}

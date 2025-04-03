﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PRN222.MilkTeaShop.Repository.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal? Price { get; set; }

    public int CategoryId { get; set; }

    public string ImageUrl { get; set; }

    public int? SoldCount { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductCombo> ProductComboCombos { get; set; } = new List<ProductCombo>();

    public virtual ICollection<ProductCombo> ProductComboProducts { get; set; } = new List<ProductCombo>();

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
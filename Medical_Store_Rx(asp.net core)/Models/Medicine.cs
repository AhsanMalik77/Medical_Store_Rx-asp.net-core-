using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Medicine
{
    public int MedId { get; set; }

    public int StoreId { get; set; }

    public string? Name { get; set; }

    public string? BaseName { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Medicalstore Store { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Medicine
{
    public int MedId { get; set; }

    public int StoreId { get; set; }

    public string Name { get; set; } = null!;

    public string BaseName { get; set; } = null!;

    public int? Price { get; set; }

    public string? Category { get; set; }

    public string? Strength { get; set; }

    public int? PillsPerPack { get; set; }

    public virtual ICollection<MedicineBatch> MedicineBatches { get; set; } = new List<MedicineBatch>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Medicalstore Store { get; set; } = null!;
}

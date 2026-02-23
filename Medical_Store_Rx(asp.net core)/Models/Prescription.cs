using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Prescription
{
    public int Id { get; set; }

    public int CustId { get; set; }

    public int Profileid { get; set; }

    public string? Location { get; set; }

    public string? RxImage { get; set; }

    public string? MedName { get; set; }

    public string? PotencyMl { get; set; }

    public int? Quantity { get; set; }

    public virtual Customer Cust { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Profile Profile { get; set; } = null!;
}

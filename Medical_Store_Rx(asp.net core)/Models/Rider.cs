using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Rider
{
    public int RiderId { get; set; }

    public int? MedId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public string? Vehinfo { get; set; }

    public string? Photo { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public int? TotalOrders { get; set; }

    public decimal? Rating { get; set; }

    public virtual Medicalstore? Med { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User RiderNavigation { get; set; } = null!;
}

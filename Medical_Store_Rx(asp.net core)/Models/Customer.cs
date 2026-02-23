using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Customer
{
    public int CId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Contact { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual User CIdNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}

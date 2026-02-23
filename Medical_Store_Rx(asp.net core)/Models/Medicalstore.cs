using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Medicalstore
{
    public int StoreId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Location { get; set; }

    public string? Images { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Rider> Riders { get; set; } = new List<Rider>();

    public virtual User Store { get; set; } = null!;
}

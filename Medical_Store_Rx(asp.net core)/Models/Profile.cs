using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Profile
{
    public int Id { get; set; }

    public int CusId { get; set; }

    public string? Fullname { get; set; }

    public string? Relation { get; set; }

    public string? Gender { get; set; }

    public string? Contact { get; set; }

    public int? Age { get; set; }

    public decimal? DefaultLat { get; set; }

    public decimal? DefaultLong { get; set; }

    public string? Addres { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual ICollection<Phr> Phrs { get; set; } = new List<Phr>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}

using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Profile
{
    public int Id { get; set; }

    public int CusId { get; set; }

    public string? Fullname { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Relation { get; set; }

    public string? Gender { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<CurrentmedPhr> CurrentmedPhrs { get; set; } = new List<CurrentmedPhr>();

    public virtual Customer Cus { get; set; } = null!;

    public virtual ICollection<DiseasesPhr> DiseasesPhrs { get; set; } = new List<DiseasesPhr>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}

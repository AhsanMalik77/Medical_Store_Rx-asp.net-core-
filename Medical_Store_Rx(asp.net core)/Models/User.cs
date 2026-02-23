using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Medicalstore? Medicalstore { get; set; }

    public virtual Rider? Rider { get; set; }
}

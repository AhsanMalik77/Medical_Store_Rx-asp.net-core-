using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class DiseasesPhr
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public string? Disease { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}

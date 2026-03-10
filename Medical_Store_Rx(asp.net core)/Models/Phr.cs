using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Phr
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public string EntryName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public virtual Profile Profile { get; set; } = null!;
}

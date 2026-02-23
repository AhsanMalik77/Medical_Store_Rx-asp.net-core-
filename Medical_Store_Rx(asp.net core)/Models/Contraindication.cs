using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Contraindication
{
    public int Id { get; set; }

    public string? BaseName { get; set; }

    public string? Disease { get; set; }

    public string? WithBase { get; set; }

    public string? Severity { get; set; }

    public string? Message { get; set; }
}

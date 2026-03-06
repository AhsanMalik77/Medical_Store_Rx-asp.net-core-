using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class MedicineBatch
{
    public int BatchId { get; set; }

    public int MedId { get; set; }

    public string? BatchNumber { get; set; }

    public int TotalPills { get; set; }

    public int RemainingPills { get; set; }

    public string? ExpiryDate { get; set; }

    public decimal? PurchasePricePerPack { get; set; }

    public virtual Medicine Med { get; set; } = null!;
}

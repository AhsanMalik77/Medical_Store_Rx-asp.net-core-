using System;
using System.Collections.Generic;

namespace Medical_Store_Rx_asp.net_core_.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustId { get; set; }

    public int StoreId { get; set; }

    public int? RiderId { get; set; }

    public int? PrespId { get; set; }

    public int? MedicineId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? TotalBill { get; set; }

    public string? Status { get; set; }

    public string? Location { get; set; }

    public virtual Customer Cust { get; set; } = null!;

    public virtual Medicine? Medicine { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Prescription? Presp { get; set; }

    public virtual Rider? Rider { get; set; }

    public virtual Medicalstore Store { get; set; } = null!;
}

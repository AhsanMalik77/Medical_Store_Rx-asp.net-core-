namespace Medical_Store_Rx_asp.net_core_.DTOs.Store
{
    public class AddBatchDto
    {
      
            public int MedId { get; set; }
            public int Price { get; set; }
            public string ExpiryDate { get; set; }
            public int Quantity { get; set; } // Packs count
        
    }
}

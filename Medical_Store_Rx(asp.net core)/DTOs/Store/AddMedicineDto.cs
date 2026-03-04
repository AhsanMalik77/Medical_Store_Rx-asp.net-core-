namespace Medical_Store_Rx_asp.net_core_.DTOs.Medicine
{
    public class AddMedicineDto
    {
        public string Name { get; set; }
        public string BaseName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int PillsPerPack { get; set; }
        public int TotalPillsStock { get; set; }
        public string Category { get; set; }
        public string Strength { get; set; }
        public int StoreId { get; set; }
    }
}
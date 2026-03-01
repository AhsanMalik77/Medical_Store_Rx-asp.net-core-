namespace Medical_Store_Rx_asp.net_core_.DTOs.User
{
    public class CustomerDto
    {
 
        public string? Name { get; set; }
        public string? Email { get; set; }
       
   

        public string? password { get; set; }
        public string? Contact { get; set; }
        public DateOnly? Dob { get; set; }
    }
}

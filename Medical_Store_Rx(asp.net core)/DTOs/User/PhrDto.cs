namespace Medical_Store_Rx_asp.net_core_.DTOs.User
{
    public class PhrDto
    {

       
        public int ProfileId { get; set; }

   
        public List<string> Allergies { get; set; } = new List<string>();


        public List<string> PastDiseases { get; set; } = new List<string>();


        public List<string> AlreadyTakingMedicines { get; set; } = new List<string>();
    }
}

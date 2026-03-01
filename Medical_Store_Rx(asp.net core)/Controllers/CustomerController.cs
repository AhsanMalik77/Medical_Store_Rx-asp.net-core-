using Microsoft.AspNetCore.Mvc;
using Medical_Store_Rx_asp.net_core_.Models;
using System.Linq;
using System.Threading.Tasks;
using Medical_Store_Rx_asp.net_core_.DTOs.User;

namespace Medical_Store_Rx_asp.net_core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CustomerController(AppDbContext db)
        {
            _db = db;
        }

        // Signup customer
        [HttpPost("Signup")]
        public async Task<IActionResult> SignupCustomer([FromBody] CustomerDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.password))
                return BadRequest("Email and Password are required");

            bool emailExists =  _db.Users.Any(u => u.Email == dto.Email);
            if (emailExists)
                return BadRequest("Email already exists");

            var user = new User
            {
                Email = dto.Email,
                Password = dto.password,
                Role = "user"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var customer = new Customer
            {
                CId = user.Id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.password,
                
                Contact = dto.Contact,
                Dob = dto.Dob
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Customer Registered Successfully",
            
            });
        }
        // Add profile
        [HttpPost("AddProfile")]
        public async Task<IActionResult> AddProfile([FromBody] Profile profile)
        {
            if (profile == null)
                return BadRequest("Invalid data");

            bool customerExists = _db.Customers.Any(c => c.CId == profile.CusId);
            if (!customerExists)
                return BadRequest("Customer not found");

            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();
            return Ok("Profile added successfully");
        }

        // Add disease to profile
        [HttpPost("AddDiseasePhr")]
        public async Task<IActionResult> AddDiseasePhr([FromBody] DiseasesPhr dp)
        {
            if (dp == null)
                return BadRequest("Invalid data");

            bool profileExists = _db.Profiles.Any(p => p.Id == dp.ProfileId);
            if (!profileExists)
                return BadRequest("Profile not found");

            _db.DiseasesPhrs.Add(dp);
            await _db.SaveChangesAsync();
            return Ok("Disease phr added successfully");
        }

        // Add current medicine
        [HttpPost("AddCurrentMedPhr")]
        public async Task<IActionResult> AddCurrentMedPhr([FromBody] CurrentmedPhr cp)
        {
            if (cp == null)
                return BadRequest("Invalid data");

            bool profileExists = _db.Profiles.Any(p => p.Id == cp.ProfileId);
            if (!profileExists)
                return BadRequest("Profile not found");

            _db.CurrentmedPhrs.Add(cp);
            await _db.SaveChangesAsync();
            return Ok("Current medicine phr added successfully");
        }

        // Add prescription
        [HttpPost("AddPrescription")]
        public async Task<IActionResult> AddPrescription([FromBody] Prescription pre)
        {
            if (pre == null)
                return BadRequest("Invalid data");

            bool customerExists = _db.Customers.Any(c => c.CId == pre.CustId);
            bool profileExists = _db.Profiles.Any(p => p.Id == pre.Profileid);

            if (!customerExists || !profileExists)
                return BadRequest("Customer or Profile not found");

            _db.Prescriptions.Add(pre);
            await _db.SaveChangesAsync();

            return Ok("Prescription added successfully");
        }
    }
}

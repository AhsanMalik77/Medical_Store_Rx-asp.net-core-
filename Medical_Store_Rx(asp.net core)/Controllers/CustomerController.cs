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
        public async Task<IActionResult> AddProfile([FromBody] AddMemberDto member)
        {
            if (member == null)
                return BadRequest("Invalid data");

            bool customerExists = _db.Customers.Any(c => c.CId == member.cus_id);
            if (!customerExists)
                return BadRequest("Customer not found");

            try
            {


                var Profile = new Profile
                {
                    CusId = member.cus_id,
                    Fullname = member.fname,
                    Relation = member.relation,
                    Gender = member.gender,
                    Contact = member.contact,
                    Age = member.age,
                    DefaultLat = member.lat,
                    DefaultLong = member.lng,
                    Addres = member.address



                };
                var check =_db.Profiles.Add(Profile);
                await _db.SaveChangesAsync();

                if(check == null)
                {
                    return BadRequest("Failed to add profile");
                }
                else
                {
                    var profileId = Profile.Id;

                    if(profileId <= 0)
                    {
                        return BadRequest("Invalid Profile ID");
                    }

                    foreach (var allergy in member.Allergies)
                    {

                        _db.Phrs.Add(new Phr
                        {
                            ProfileId = profileId,
                            EntryName = allergy,
                            Category = "Allergy"
                        });
                    }
                    foreach (var disease in member.PastDiseases)
                    {
                        _db.Phrs.Add(new Phr
                        {
                            ProfileId= profileId,
                            EntryName = disease,
                            Category = "PastDisease"
                        });
                        
                        
                    }
                    foreach (var medicine in member.AlreadyTakingMedicines)
                    {
                        _db.Phrs.Add(new Phr
                        {
                            ProfileId = profileId,
                            EntryName = medicine,
                            Category = "AlreadyTakingMedicine"
                        });
                    }

                    await _db.SaveChangesAsync();


                    return Ok("Profile added successfully With Phr");
                }

                  




            }
            catch(Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
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
        [HttpGet("salamuser")]
        public async Task<IActionResult> salamuser(int id)
        {
            if (id == 0)
            {
                return BadRequest("Wrong id");
            }

            bool customerExists = _db.Customers.Any(c => c.CId == id);
            if (!customerExists)
            {
                return BadRequest("Customer not found");
            }
            var data=_db.Customers.FirstOrDefault(c=>c.CId == id);
            return Ok(new
            {
              
                name=data.Name

            });

        }
    }
}

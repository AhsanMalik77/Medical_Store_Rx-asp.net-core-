
using Medical_Store_Rx_asp.net_core_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Medical_Store_Rx_asp.net_core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SigninController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Signin/Login?email=abc&password=123
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return BadRequest("Email and Password required");

                var role = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (role == null)
                    return BadRequest("Email or password is incorrect");

                if (role.Role == "customer")
                {
                    var res = _db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
                    if (res == null)
                        return NotFound("Customer not found");

                    return Ok(new
                    {
                        id = res.CId,
                        namee = res.Name,
                        role= role.Role 

                    });
                }

                if (role.Role == "Rider")
                {
                    var res = _db.Riders.FirstOrDefault(r => r.Email == email && r.Password == password);
                    if (res == null)
                        return NotFound("Rider not found");

                    var store = _db.Medicalstores.FirstOrDefault(s => s.StoreId == res.MedId);
                    if (store == null)
                        return NotFound("Store not found");

                    return Ok(new
                    {
                        id = res.RiderId,
                        medid = store.StoreId,
                        nam = res.Name,
                        role= role.Role
                    });
                }

                if (role.Role == "Store")
                {
                    var res = _db.Medicalstores.FirstOrDefault(s => s.Email == email && s.Password == password);
                    if (res == null)
                        return NotFound("Store not found");

                    return Ok(new
                    {
                        id = res.StoreId,
                        nam = res.Name,
                        role= role.Role
                    });
                }

                return BadRequest("Something went wrong");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

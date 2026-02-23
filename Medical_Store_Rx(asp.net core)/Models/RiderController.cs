using Microsoft.AspNetCore.Mvc;
using Medical_Store_Rx_asp.net_core_.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medical_Store_Rx_asp.net_core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RiderController(AppDbContext db)
        {
            _db = db;
        }

        // Register Rider
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterRider(Rider r)
        {
            if (r == null)
                return BadRequest("Invalid data");

            if (string.IsNullOrEmpty(r.Email) || string.IsNullOrEmpty(r.Password))
                return BadRequest("Email and Password are required");

            bool emailExists = _db.Users.Any(u => u.Email == r.Email);
            if (emailExists)
                return BadRequest("Email already exists");

            // Create user record
            var userObj = new User
            {
                Email = r.Email,
                Password = r.Password,
                Role = "Rider"
            };

            _db.Users.Add(userObj);
            await _db.SaveChangesAsync();

            // Validate medical store
            var store = await _db.Medicalstores.FindAsync(r.MedId);
            if (store == null)
                return BadRequest("Medical Store not found");

            r.RiderId = userObj.Id;

            _db.Riders.Add(r);
            await _db.SaveChangesAsync();

            return Ok("Rider registered successfully");
        }

        // Get Rider Orders (pending)
        [HttpGet("Orders")]
        public IActionResult GetRiderOrders(int riderId)
        {
            var rider = _db.Riders.FirstOrDefault(r => r.RiderId == riderId);
            if (rider == null)
                return BadRequest("Rider not found");

            var data = _db.Orders
                .Where(o => o.StoreId == rider.MedId && o.Status == "pending")
                .Select(o => new
                {
                    CustomerName = o.Cust.Name,
                    MedicineName = o.Medicine.Name,
                    TotalBill = o.TotalBill,
                    Status = o.Status,
                    Location = o.Location
                })
                .ToList();

            return Ok(data);
        }

        // Accept Order
        [HttpPost("AcceptOrder")]
        public async Task<IActionResult> AcceptOrder(int riderId, int orderId)
        {
            var rider = await _db.Riders.FindAsync(riderId);
            if (rider == null)
                return BadRequest("Rider not found");

            var order = _db.Orders.FirstOrDefault(o => o.OrderId == orderId && o.StoreId == rider.MedId);
            if (order == null)
                return BadRequest("Order not found");

            order.Status = "Accept";
            order.RiderId = riderId;

            await _db.SaveChangesAsync();

            return Ok("Order accepted successfully");
        }
    }
}

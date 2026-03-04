using Microsoft.AspNetCore.Mvc;
using Medical_Store_Rx_asp.net_core_.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Medical_Store_Rx_asp.net_core_.DTOs;        // Add this for DTOs
using Medical_Store_Rx_asp.net_core_.DTOs.Store;
using Medical_Store_Rx_asp.net_core_.DTOs.Medicine;

namespace Medical_Store_Rx_asp.net_core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StoreController(AppDbContext db)
        {
            _db = db;
        }

        // Store Signup
        [HttpPost("StoreSignup")]
        public async Task<IActionResult> StoreSignup(StoreSignupDto store)
        {
            if (store == null)
                return BadRequest("Invalid data");

            if (string.IsNullOrEmpty(store.Email) || string.IsNullOrEmpty(store.Password))
                return BadRequest("Email and Password are required");

            bool emailExists = _db.Users.Any(u => u.Email == store.Email);
            if (emailExists)
                return BadRequest("Email already exists");

            // Create user
            var userObj = new User
            {
                Email = store.Email,
                Password = store.Password,
                Role = "Store"
            };

            _db.Users.Add(userObj);
            await _db.SaveChangesAsync();

            var Medicalstore = new Medicalstore
            {
                StoreId=userObj.Id,
                Name=store.Name,
                Email=store.Email,
                Location=store.Location,
                Password=store.Password

            };

            _db.Medicalstores.Add(Medicalstore);
            await _db.SaveChangesAsync();

            return Ok("MedicalStore Registered");
        }

        //// Add Medicine
        //[HttpPost("AddMedicine")]
        //public async Task<IActionResult> AddMedicine(AddMedicineDto request)
        //{
        //    if (request == null)
        //        return BadRequest("Invalid medicine data");

        //    if (string.IsNullOrWhiteSpace(request.Name))
        //        return BadRequest("Medicine name is required");

        //    if (request.Price < 0) // Price hoga, Dice nahi
        //    {
        //        return BadRequest("Price should be greater than or equal to 0");
        //    }

        //    // Baqi logic yahan likho
        //    // _context.Medicines.Add(medicine);
        //    // await _context.SaveChangesAsync();

        //    return Ok("Medicine added successfully");
        //}
        //// Update Store Profile
        //[HttpPut("UpdateStore")]
        //public async Task<IActionResult> UpdateStore(Medicalstore store)
        //{
        //    if (store == null)
        //        return BadRequest("Invalid data");

        //    var existingStore = await _db.Medicalstores.FindAsync(store.StoreId);
        //    if (existingStore == null)
        //        return NotFound("Store not found");

        //    if (!string.IsNullOrWhiteSpace(store.Name))
        //        existingStore.Name = store.Name;

        //    if (!string.IsNullOrWhiteSpace(store.Location))
        //        existingStore.Location = store.Location;

        //    if (!string.IsNullOrWhiteSpace(store.Email))
        //    {
        //        bool emailExists = _db.Medicalstores.Any(s => s.Email == store.Email && s.StoreId != store.StoreId);
        //        if (emailExists)
        //            return BadRequest("Email already in use by another store");

        //        existingStore.Email = store.Email;
        //    }

        //    if (!string.IsNullOrWhiteSpace(store.Password))
        //        existingStore.Password = store.Password;

        //    await _db.SaveChangesAsync();
        //    return Ok("Store updated successfully");
        //}

        // Update Stock
        [HttpPut("UpdateStock")]
        public async Task<IActionResult> UpdateStock(int storeId, int medId, int quantity)
        {
            if (storeId <= 0 || medId <= 0)
                return BadRequest("Invalid Store ID or Medicine ID");

            var store = await _db.Medicalstores.FindAsync(storeId);
            if (store == null)
                return NotFound("Store not found");

            var medicine = _db.Medicines.FirstOrDefault(m => m.MedId == medId && m.StoreId == storeId);
            if (medicine == null)
                return NotFound("Medicine not found");

            if (quantity < 0)
                return BadRequest("Quantity cannot be negative");

            medicine.Quantity += quantity;
            await _db.SaveChangesAsync();

            return Ok("Stock updated successfully");
        }
    }
}

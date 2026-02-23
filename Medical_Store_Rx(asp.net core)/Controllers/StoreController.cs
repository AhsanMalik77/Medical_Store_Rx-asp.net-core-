using Microsoft.AspNetCore.Mvc;
using Medical_Store_Rx_asp.net_core_.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> StoreSignup(Medicalstore store)
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

            store.StoreId = userObj.Id;

            _db.Medicalstores.Add(store);
            await _db.SaveChangesAsync();

            return Ok("MedicalStore Registered");
        }

        // Add Medicine
        [HttpPost("AddMedicine")]
        public async Task<IActionResult> AddMedicine(Medicine request)
        {
            if (request == null)
                return BadRequest("Invalid medicine data");

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Medicine name is required");

            if (request.Price <= 0)
                return BadRequest("Price must be greater than 0");

            if (request.StoreId <= 0)
                return BadRequest("Valid Store ID is required");

            var storeExists = _db.Medicalstores.Any(s => s.StoreId == request.StoreId);
            if (!storeExists)
                return BadRequest("Store not found");

            bool medicineExists = _db.Medicines
                .Any(m => m.StoreId == request.StoreId &&
                          m.Name.ToLower() == request.Name.ToLower());
            if (medicineExists)
                return BadRequest("Medicine with this name already exists in your store");

            var newMedicine = new Medicine
            {
                StoreId = request.StoreId,
                Name = request.Name,
                BaseName = request.BaseName,
                Price = request.Price,
                Quantity = request.Quantity,
                ExpiryDate = request.ExpiryDate
            };

            _db.Medicines.Add(newMedicine);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                Status = "SUCCESS",
                Message = "Medicine added successfully",
                MedicineId = newMedicine.MedId,
                StoreId = newMedicine.StoreId,
                MedicineName = newMedicine.Name,
                BaseName = newMedicine.BaseName,
                Price = newMedicine.Price,
                Quantity = newMedicine.Quantity,
                ExpiryDate = newMedicine.ExpiryDate
            });
        }

        // Update Store Profile
        [HttpPut("UpdateStore")]
        public async Task<IActionResult> UpdateStore(Medicalstore store)
        {
            if (store == null)
                return BadRequest("Invalid data");

            var existingStore = await _db.Medicalstores.FindAsync(store.StoreId);
            if (existingStore == null)
                return NotFound("Store not found");

            if (!string.IsNullOrWhiteSpace(store.Name))
                existingStore.Name = store.Name;

            if (!string.IsNullOrWhiteSpace(store.Location))
                existingStore.Location = store.Location;

            if (!string.IsNullOrWhiteSpace(store.Email))
            {
                bool emailExists = _db.Medicalstores.Any(s => s.Email == store.Email && s.StoreId != store.StoreId);
                if (emailExists)
                    return BadRequest("Email already in use by another store");

                existingStore.Email = store.Email;
            }

            if (!string.IsNullOrWhiteSpace(store.Password))
                existingStore.Password = store.Password;

            await _db.SaveChangesAsync();
            return Ok("Store updated successfully");
        }

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

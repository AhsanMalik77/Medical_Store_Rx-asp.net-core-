using Microsoft.AspNetCore.Mvc;
using Medical_Store_Rx_asp.net_core_.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medical_Store_Rx_asp.net_core_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrderController(AppDbContext db)
        {
            _db = db;
        }

        // Place Order
        [HttpPost("Place")]
        public async Task<IActionResult> PlaceOrder(int custId, int storeId, int prespId, int medId, int quantity)
        {
            try
            {
                // Validate
                var customer = await _db.Customers.FindAsync(custId);
                var store = await _db.Medicalstores.FindAsync(storeId);
                var medicine = _db.Medicines.FirstOrDefault(m => m.MedId == medId && m.StoreId == storeId);
                var prescription = await _db.Prescriptions.FindAsync(prespId);

                if (customer == null || store == null || medicine == null || prescription == null)
                    return BadRequest("Customer / Store / Medicine / Prescription not found");

                // 1️⃣ Create Order
                var newOrder = new Order
                {
                    CustId = custId,
                    StoreId = storeId,
                    PrespId = prespId,
                    MedicineId = medId,
                    OrderDate = DateTime.Now,
                    Status = "pending",
                    Location = prescription.Location
                };
                _db.Orders.Add(newOrder);
                await _db.SaveChangesAsync();

                // 2️⃣ Create Order Item
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.OrderId,
                    MedicineId = medicine.MedId,
                    Quantity = quantity,
                    UnitPrice = medicine.Price ?? 0,
                    MedName = medicine.Name,
                    CreatedAt = DateTime.Now
                };

                medicine.Quantity -= quantity;

                _db.OrderItems.Add(orderItem);
                await _db.SaveChangesAsync();

                // 3️⃣ Calculate total bill
                int totalBill = CalculateBill(newOrder.OrderId);

                // 4️⃣ Update order total bill
                newOrder.TotalBill = totalBill;
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Order placed successfully",
                    orderId = newOrder.OrderId,
                    totalBill = totalBill
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Bill calculation method
        private int CalculateBill(int orderId)
        {
            decimal total = _db.OrderItems
                .Where(o => o.OrderId == orderId)
                .Sum(o => o.UnitPrice * o.Quantity);

            return Convert.ToInt32(total);
        }
    }
}

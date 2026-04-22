using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Testing.Pages
{
    public class AddSellerModel : PageModel
    {
        private readonly string connectionString =
            "server=localhost;database=DropZoneDB;user=root;password=YOUR_PASSWORD";

        public IActionResult OnPost()
        {
          
            var buyerName = Request.Form["BuyerName"];
            var dateDropped = Request.Form["DateDropped"];
            var price = Request.Form["Price"];
            var holdingFee = Request.Form["HoldingFee"];
            var datePickedUp = Request.Form["DatePickedUp"];
            var itemStatus = Request.Form["ItemStatus"];
            var dateCashOut = Request.Form["DateCashOut"];
            var placed = Request.Form["Placed"];

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                INSERT INTO NameOfSeller 
                (BuyerName, DateDropped, Price, HoldingFee, DatePickedUp, ItemStatus, DateCashOut, Placed)
                VALUES
                (@BuyerName, @DateDropped, @Price, @HoldingFee, @DatePickedUp, @ItemStatus, @DateCashOut, @Placed)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BuyerName", buyerName);
                    cmd.Parameters.AddWithValue("@DateDropped", dateDropped);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@HoldingFee", holdingFee);
                    cmd.Parameters.AddWithValue("@DatePickedUp", datePickedUp);
                    cmd.Parameters.AddWithValue("@ItemStatus", itemStatus);
                    cmd.Parameters.AddWithValue("@DateCashOut", dateCashOut);
                    cmd.Parameters.AddWithValue("@Placed", placed);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/Sellers");
        }
    }
}
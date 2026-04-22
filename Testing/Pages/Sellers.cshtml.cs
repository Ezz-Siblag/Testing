using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Testing.Pages
{
    public class SellersModel : PageModel
    {
        private readonly string connectionString =
            "server=localhost;database=DropZoneDB;user=root;password=; port = 1108";

        public List<Seller> SellerList { get; set; } = new();

        public void OnGet(string searchTerm)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM NameOfSeller";

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE BuyerName LIKE @search";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SellerList.Add(new Seller
                            {
                                ItemNumber = reader.GetInt32("ItemNumber"),
                                BuyerName = reader.GetString("BuyerName"),
                                DateDropped = reader.GetString("DateDropped"),
                                Price = reader.GetInt32("Price"),
                                HoldingFee = reader.GetInt32("HoldingFee"),
                                DatePickedUp = reader.GetString("DatePickedUp"),
                                ItemStatus = reader.GetString("ItemStatus"),
                                DateCashOut = reader.GetString("DateCashOut"),
                                Placed = reader.GetString("Placed")
                            });
                        }
                    }
                }
            }
        }
    }

    public class Seller
    {
        public int ItemNumber { get; set; }
        public string? BuyerName { get; set; }
        public string? DateDropped { get; set; }
        public int Price { get; set; }
        public int HoldingFee { get; set; }
        public string? DatePickedUp { get; set; }
        public string? ItemStatus { get; set; }
        public string? DateCashOut { get; set; }
        public string? Placed { get; set; }
    }
}
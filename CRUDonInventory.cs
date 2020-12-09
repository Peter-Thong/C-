using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace Assignment2
{
    class CRUDonInventory
    {
        public static void PrintInventories()
        {
            string cs = Program.GetConnectionString();
            string query = "Select ID, vehicleID, numberOnHand, price, cost from Inventories";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine($"{"id",5} {"vehicleID",-15} {"numberOnHand",-20} {"price",-15} {"cost",10}\n");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int vehicleID = reader.GetInt32(1);
                    int numberOnHand = reader.GetInt32(2);
                    decimal price = reader.GetDecimal(3);
                    decimal cost = reader.GetDecimal(4);

                    Console.WriteLine($"{id,5} {vehicleID,-15} {numberOnHand,-20} {price,-15} {cost,10}");
                }
            }
        }

        public static void PrintInventoriesByVehicleID(int vehicleId)
        {
            string cs = Program.GetConnectionString();
            string query = "Select ID, vehicleID, numberOnHand, price, cost from Inventories WHERE vehicleID=@vehicleID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("vehicleID", vehicleId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine($"{"id",5} {"vehicleID",-15} {"numberOnHand",-20} {"price",-15} {"cost",10}\n");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int vehicleID = reader.GetInt32(1);
                    int numberOnHand = reader.GetInt32(2);
                    decimal price = reader.GetDecimal(3);
                    decimal cost = reader.GetDecimal(4);

                    Console.WriteLine($"{id,5} {vehicleID,-15} {numberOnHand,-20} {price,-15} {cost,10}");
                }
            }
        }

        public static void InsertInventories(int vehicleID, int numberOnHand, decimal price, decimal cost)
        {
            string cs = Program.GetConnectionString();
            string query = "INSERT INTO Inventories(vehicleID, numberOnHand, price, cost) VALUES (@vehicleID, @numberOnHand, @price, @cost)";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("vehicleID", vehicleID);
                cmd.Parameters.AddWithValue("numberOnHand", numberOnHand);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("cost", cost);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Inventories inserted");
                else
                    Console.WriteLine("Inventories not inserted");
            }
        }

        public static void UpdateInventories(int id, int vehicleID, int numberOnHand, decimal price, decimal cost)
        {
            string cs = Program.GetConnectionString();
            string query = "UPDATE Inventories SET vehicleID = @vehicleID, numberOnHand = @numberOnHand, price = @price, cost = @cost WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.Parameters.AddWithValue("vehicleID", vehicleID);
                cmd.Parameters.AddWithValue("numberOnHand", numberOnHand);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("cost", cost);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Inventories updated");
                else
                    Console.WriteLine("Inventories not updated");
            }
        }

        public static void DeleteInventories(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "DELETE FROM Inventories WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                conn.Open();
                int result = 0;
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("can not delete the item because it have reference in repair table.");
                }

                if (result == 1)
                    Console.WriteLine("Inventories deleted");
                else
                    Console.WriteLine("Inventories not deleted");
            }
        }

        public static int CheckInventoryId(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "SELECT Count(*) FROM Inventories WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                conn.Open();

                int num = (int)cmd.ExecuteScalar();
                return num;
            }
        }
    }
}

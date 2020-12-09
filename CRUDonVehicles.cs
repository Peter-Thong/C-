using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
namespace Assignment2
{
    class CRUDonVehicles
    {
        public static void PrintVehicles()
        {
            string cs = Program.GetConnectionString();
            string query = "Select ID, make, model, year, [new/used] from Vehicles";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine($"{"id",5} {"make",-10} {"model",-15} {"year",-15} {"new/Used",15}\n");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string make = reader.GetString(1);
                    string model = reader.GetString(2);
                    string year = reader.GetString(3);
                    string newOrUsed = reader.GetString(4);
                    Console.WriteLine($"{id,5} {make,-10} {model,-15} {year,-15} {newOrUsed,15}");
                }
            }
        }

        public static void InsertVehicles(string make, string model, string year, string newOrUsed)
        {
            string cs = Program.GetConnectionString();
            string query = "INSERT INTO Vehicles(make, model, year, [new/used]) VALUES (@make, @model, @year, @newOrUsed)";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("make", make);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("year", year);
                cmd.Parameters.AddWithValue("newOrUsed", newOrUsed);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("vehicle inserted");
                else
                    Console.WriteLine("vehicle not inserted");
            }
        }

        public static void UpdateVehicles(int id, string make, string model, string year, string newOrUsed)
        {
            string cs = Program.GetConnectionString();
            string query = "UPDATE Vehicles SET make = @make, model = @model, year = @year, [new/used] = @newOrUsed WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.Parameters.AddWithValue("make", make);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("year", year);
                cmd.Parameters.AddWithValue("newOrUsed", newOrUsed);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("vehicle updated");
                else
                    Console.WriteLine("vehicle not updated");
            }
        }

        public static void DeleteVehicles(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "DELETE FROM Vehicles WHERE ID = @ID";
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
                    Console.WriteLine("can not delete the item because it have reference in inventory table.");
                }

                if (result == 1)
                    Console.WriteLine("vehicle deleted");
                else
                    Console.WriteLine("vehicle not deleted");
            }
        }

        public static int CheckVehicleId(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "SELECT Count(*) FROM Vehicles WHERE ID = @ID";
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

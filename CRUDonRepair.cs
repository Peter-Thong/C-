using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace Assignment2
{
    class CRUDonRepair
    {
        public static void PrintRepairs()
        {
            string cs = Program.GetConnectionString();
            string query = "Select ID, inventoryID, whatToRepair from Repairs";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine($"{"id",5} {"inventoryID",-20} {"whatToRepair",20}\n");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int inventoryID = reader.GetInt32(1);
                    string whatToRepair = reader.GetString(2);

                    Console.WriteLine($"{id,5} {inventoryID,-20} {whatToRepair,20}");
                }
            }
        }

        public static void InsertRepairs(int inventoryID, string whatToRepair)
        {
            string cs = Program.GetConnectionString();
            string query = "INSERT INTO Repairs(inventoryID, whatToRepair) VALUES (@inventoryID, @whatToRepair)";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("inventoryID", inventoryID);
                cmd.Parameters.AddWithValue("whatToRepair", whatToRepair);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Repairs inserted");
                else
                    Console.WriteLine("Repairs not inserted");
            }
        }

        public static void UpdateRepairs(int id, int inventoryID, string whatToRepair)
        {
            string cs = Program.GetConnectionString();
            string query = "UPDATE Repairs SET inventoryID = @inventoryID, whatToRepair = @whatToRepair WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.Parameters.AddWithValue("inventoryID", inventoryID);
                cmd.Parameters.AddWithValue("whatToRepair", whatToRepair);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Repairs updated");
                else
                    Console.WriteLine("Repairs not updated");
            }
        }

        public static void DeleteRepairs(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "DELETE FROM Repairs WHERE ID = @ID";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("ID", id);
                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("Repairs deleted");
                else
                    Console.WriteLine("Repairs not deleted");
            }
        }

        public static int CheckRepairId(int id)
        {
            string cs = Program.GetConnectionString();
            string query = "SELECT Count(*) FROM Repairs WHERE ID = @ID";
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

using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Assignment2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int input = 0;
            //loop to get user input
            do
            {
                //handling error when user type in invalid type input
                try
                {

                    input = int.Parse(GetInput("•Welcome, please choose a command:\n" +
                        "•Press 1 to modify vehicles\n" +
                        "•Press 2 to modify inventory\n" +
                        "•Press 3 to modify repair\n" +
                        "•Press 4 to exit program"));

                    //check user input 
                    switch (input)
                    {
                        case 1:
                            ModifyVehicles();
                            break;
                        case 2:
                            ModifyInventory();
                            break;
                        case 3:
                            ModifyRepair();
                            break;
                        case 4:
                            break;
                        default:
                            // when input out of bound.
                            Console.WriteLine("input must be a number between 1-4");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("input must be an integer");
                }

            } while (input != 4);
        }

        static void ModifyVehicles()
        {
            int input = 0;
            int id;
            string make;
            string model;
            string year;
            string newOrUsed;
            int validId;
            do
            {
                try
                {
                    input = int.Parse(GetInput("•Press 1 to list all vehicles\n" +
                                            "•Press 2 to add a new vehicle\n" +
                                            "•Press 3 to update vehicle\n" +
                                            "•Press 4 to delete vehicle\n" +
                                            "•Press 5 to return to main menu"));

                    switch (input)
                    {
                        case 1:
                            CRUDonVehicles.PrintVehicles();
                            break;
                        case 2:
                            
                            make = GetInput("Enter vehicle Make:");
                            model = GetInput("Enter vehicle Model:");
                            year = GetInput("Enter vehicle Year:");
                            newOrUsed = GetInput("Enter new/used type of vehicle:");
                            CRUDonVehicles.InsertVehicles(make, model, year, newOrUsed);

                            break;
                        case 3:
                            do
                            {
                                id = int.Parse(GetInput("Enter vehicle Id:"));
                                validId = CRUDonVehicles.CheckVehicleId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);
                            

                            make = GetInput("Enter vehicle Make:");
                            model = GetInput("Enter vehicle Model:");
                            year = GetInput("Enter vehicle Year:");
                            newOrUsed = GetInput("Enter new/used type of vehicle:");

                            CRUDonVehicles.UpdateVehicles(id, make, model, year, newOrUsed);

                            break;
                        case 4:

                            do
                            {
                                id = int.Parse(GetInput("Enter vehicle Id:"));
                                validId = CRUDonVehicles.CheckVehicleId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);

                            CRUDonVehicles.DeleteVehicles(id);
                            break;
                        case 5:
                            break;
                        default:
                            Console.WriteLine("input must be a number between 1-5");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("input must be an integer");
                }

            } while (input != 5);
        }

        static void ModifyInventory()
        {
            int input = 0;
            int id;
            int vehicleID;
            int numberOnHand;
            decimal price;
            decimal cost;
            int checkID;
            int validId;
            do
            {
                try
                {
                    input = int.Parse(GetInput("•Press 1 to list all inventory\n" +
                                            "•Press 2 to view inventory for a vehicle\n" +
                                            "•Press 3 to add a new inventory\n" +
                                            "•Press 4 to update inventory\n" +
                                            "•Press 5 to delete inventory\n" +
                                            "•Press 6 to return to main menu"));

                    switch (input)
                    {
                        case 1:
                            CRUDonInventory.PrintInventories();
                            break;
                        case 2:
                            do
                            {
                                id = int.Parse(GetInput("Enter vehicle Id:"));
                                validId = CRUDonVehicles.CheckVehicleId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);

                            CRUDonInventory.PrintInventoriesByVehicleID(id);

                            break;
                        case 3:
                            do {
                                checkID = int.Parse(GetInput("•Press 1 to list all vehicles to show ID for inserting\n" +
                                            "•Press 2 to continue\n"
                                            ));
                                switch (checkID)
                                {
                                    case 1:
                                        CRUDonVehicles.PrintVehicles();
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("input must be a number between 1-2");
                                        break;
                                }

                            } while (checkID != 2);


                            do
                            {
                                vehicleID = int.Parse(GetInput("Enter vehicle ID for inventory:"));
                                validId = CRUDonVehicles.CheckVehicleId(vehicleID);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);

                            numberOnHand = int.Parse(GetInput("Enter number On Hand:"));
                            price = decimal.Parse(GetInput("Enter price:"));
                            cost = decimal.Parse(GetInput("Enter cost:"));
                            CRUDonInventory.InsertInventories(vehicleID, numberOnHand, price, cost);

                            break;
                        case 4:
                            do
                            {
                                checkID = int.Parse(GetInput("•Press 1 to list all vehicles to show ID for updating\n" +
                                            "•Press 2 to continue\n"
                                            ));
                                switch (checkID)
                                {
                                    case 1:
                                        CRUDonVehicles.PrintVehicles();
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("input must be a number between 1-2");
                                        break;

                                }

                            } while (checkID != 2);

                            do
                            {
                                id = int.Parse(GetInput("Enter inventory Id:"));
                                validId = CRUDonInventory.CheckInventoryId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);


                            do
                            {
                                vehicleID = int.Parse(GetInput("Enter vehicle ID for inventory:"));
                                validId = CRUDonVehicles.CheckVehicleId(vehicleID);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);

                            numberOnHand = int.Parse(GetInput("Enter number On Hand:"));
                            price = decimal.Parse(GetInput("Enter price:"));
                            cost = decimal.Parse(GetInput("Enter cost:"));
                            CRUDonInventory.UpdateInventories(id, vehicleID, numberOnHand, price, cost);

                            break;
                        case 5:
                            do
                            {
                                id = int.Parse(GetInput("Enter inventory Id:"));
                                validId = CRUDonInventory.CheckInventoryId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);
                            CRUDonInventory.DeleteInventories(id);
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("input must be a number between 1-6");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("input must be a number");
                }

            } while (input != 6);
        }

        static void ModifyRepair()
        {
            int input = 0;
            int id;
            int inventoryID;
            string whatToRepair;
            int checkID;
            int validId;
            do
            {
                try
                {
                    input = int.Parse(GetInput("•Press 1 to list all repair\n" +
                                            "•Press 2 to add a new repair\n" +
                                            "•Press 3 to update repair\n" +
                                            "•Press 4 to delete repair\n" +
                                            "•Press 5 to return to main menu"));

                    switch (input)
                    {
                        case 1:
                            CRUDonRepair.PrintRepairs();
                            break;
                        case 2:
                            do
                            {
                                checkID = int.Parse(GetInput("•Press 1 to list all inventory to show ID for inserting\n" +
                                            "•Press 2 to continue\n"
                                            ));
                                switch (checkID)
                                {
                                    case 1:
                                        CRUDonInventory.PrintInventories();
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("input must be a number between 1-2");
                                        break;

                                }

                            } while (checkID != 2);

                            do
                            {
                                inventoryID = int.Parse(GetInput("Enter inventory ID for repair:"));
                                validId = CRUDonInventory.CheckInventoryId(inventoryID);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);

                            
                            whatToRepair = GetInput("Enter what To Repair:");
                            CRUDonRepair.InsertRepairs(inventoryID, whatToRepair);

                            break;
                        case 3:
                            do
                            {
                                checkID = int.Parse(GetInput("•Press 1 to list all inventory to show ID for updating\n" +
                                            "•Press 2 to continue\n"
                                            ));
                                switch (checkID)
                                {
                                    case 1:
                                        CRUDonInventory.PrintInventories();
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("input must be a number between 1-2");
                                        break;

                                }

                            } while (checkID != 2);

                            do
                            {
                                id = int.Parse(GetInput("Enter repair Id:"));
                                validId = CRUDonRepair.CheckRepairId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);
                            

                            do
                            {
                                inventoryID = int.Parse(GetInput("Enter inventory ID for repair:"));
                                validId = CRUDonInventory.CheckInventoryId(inventoryID);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);
                            whatToRepair = GetInput("Enter what To Repair:");

                            CRUDonRepair.UpdateRepairs(id, inventoryID, whatToRepair);

                            break;
                        case 4:
                            do
                            {
                                id = int.Parse(GetInput("Enter repair Id:"));
                                validId = CRUDonRepair.CheckRepairId(id);
                                if (validId == 0)
                                {
                                    Console.WriteLine("Id entered is not valid");
                                }
                            } while (validId == 0);
                            CRUDonRepair.DeleteRepairs(id);
                            break;
                        case 5:
                            break;
                        default:
                            Console.WriteLine("input must be a number between 1-5");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("input must be an integer");
                }

            } while (input != 5);
        }



        //method getting user input
        static string GetInput(string str)
        {
            Console.WriteLine(str);

            string result = Console.ReadLine();
            return result;
        }

        public static string GetConnectionString() { 
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()); 
            configurationBuilder.AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();
            return config["ConnectionStrings:CarRepairMdf"]; 
        }

    }
}

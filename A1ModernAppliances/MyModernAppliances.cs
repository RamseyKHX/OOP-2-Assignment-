using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ModernAppliances
{
    internal class MyModernAppliances : ModernAppliances
    {
        public override void Checkout()
        {
            Console.WriteLine("Enter the item number of an appliance: ");
            string userInput = Console.ReadLine();
            long itemNumber = Convert.ToInt64(userInput);

            Appliance foundAppliance = Appliances.FirstOrDefault(appliance => appliance.ItemNumber == itemNumber);

            if (foundAppliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
                return;
            }

            if (foundAppliance.Quantity > 0)
            {
                foundAppliance.Checkout();
                Console.WriteLine($"Appliance \"{foundAppliance.ItemNumber}\" has been checked out.");
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
        }

        public override void Find()
        {
            Console.WriteLine("Enter brand to search for:");
            string inputBrand = Console.ReadLine();

            var foundAppliances = Appliances.Where(appliance => appliance.Brand == inputBrand).ToList();

            DisplayAppliancesFromList(foundAppliances, 0);
        }

        public override void DisplayRefrigerators()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("2 - Double doors");
            Console.WriteLine("3 - Three doors");
            Console.WriteLine("4 - Four doors");
            Console.WriteLine("Enter number of doors: ");
            int doorsNum = Convert.ToInt32(Console.ReadLine());

            var foundAppliances = Appliances.OfType<Refrigerator>()
                                            .Where(refrigerator => doorsNum == 0 || refrigerator.Doors == doorsNum)
                                            .Cast<Appliance>()
                                            .ToList();

            DisplayAppliancesFromList(foundAppliances, 0);
        }

        public override void DisplayVacuums()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Residential");
            Console.WriteLine("2 - Commercial");
            Console.WriteLine("Enter grade:");
            string gradeInput = Console.ReadLine();

            string grade = gradeInput switch
            {
                "0" => "Any",
                "1" => "Residential",
                "2" => "Commercial",
                _ => throw new InvalidOperationException("Invalid option.")
            };

            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - 18 Volt");
            Console.WriteLine("2 - 24 Volt");
            Console.WriteLine("Enter voltage:");
            int voltage = Convert.ToInt32(Console.ReadLine());

            var found = Appliances.OfType<Vacuum>()
                                  .Where(vacuum => (grade == "Any" || grade == vacuum.Grade) &&
                                                   (voltage == 0 || voltage == vacuum.BatteryVoltage))
                                  .Cast<Appliance>()
                                  .ToList();

            DisplayAppliancesFromList(found, 0);
        }

        public override void DisplayMicrowaves()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - kitchen");
            Console.WriteLine("2 - Work site");
            Console.WriteLine("Enter room type:");
            char roomType = Console.ReadLine() switch
            {
                "0" => 'A',
                "1" => 'K',
                "2" => 'W',
                _ => throw new InvalidOperationException("Invalid option.")
            };

            var found = Appliances.OfType<Microwave>()
                                  .Where(microwave => roomType == 'A' || microwave.RoomType == roomType)
                                  .Cast<Appliance>()
                                  .ToList();

            DisplayAppliancesFromList(found, 0);
        }

        public override void DisplayDishwashers()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Quietest");
            Console.WriteLine("2 - Quieter");
            Console.WriteLine("3 - Quiet");
            Console.WriteLine("4 - Moderate");
            Console.WriteLine("Enter sound rating:");
            string soundInput = Console.ReadLine();

            string soundRating = soundInput switch
            {
                "0" => "Any",
                "1" => "Qt",
                "2" => "Qr",
                "3" => "Qu",
                "4" => "M",
                _ => throw new InvalidOperationException("Invalid option.")
            };

            var found = Appliances.OfType<Dishwasher>()
                                  .Where(dishwasher => soundRating == "Any" || dishwasher.SoundRating == soundRating)
                                  .Cast<Appliance>()
                                  .ToList();

            DisplayAppliancesFromList(found, 0);
        }

        public override void RandomList()
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Refrigerators");
            Console.WriteLine("2 - Vacuums");
            Console.WriteLine("3 - Microwaves");
            Console.WriteLine("4 - Dishwashers");

            Console.WriteLine("Enter type of appliance:");
            string applianceTypeInput = Console.ReadLine();
            Console.WriteLine("Enter number of appliances: ");
            int numOfApp = Convert.ToInt32(Console.ReadLine());

            var found = Appliances.Where(appliance => applianceTypeInput switch
            {
                "0" => true,
                "1" => appliance is Refrigerator,
                "2" => appliance is Vacuum,
                "3" => appliance is Microwave,
                "4" => appliance is Dishwasher,
                _ => false
            }).ToList();

            found.Sort(new RandomComparer());

            if (found.Count > numOfApp)
            {
                found = found.Take(numOfApp).ToList();
            }

            if (found.Count == 0)
            {
                Console.WriteLine("No appliance matches the criteria.");
            }
            else
            {
                DisplayAppliancesFromList(found, numOfApp);
            }
        }
    }
}

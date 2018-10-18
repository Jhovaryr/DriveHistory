using DriveHistory.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(args[0].Trim()))
            {
                Console.WriteLine("Cannot find file. Please try again.");
                Environment.Exit(0);
            }

            var motorVehicleSystem = new RootMotorVehicleSystem();

            // Processes file line by line.
            foreach (string line in File.ReadLines(args[0]))
            {
                string[] commandString = line.Split(' '); // Space demilited.

                // Checks if the commands given are valid.
                if (!IsStringCommandValid(commandString))
                {
                    continue;
                }

                CommandEnum command;

                // If command cannot parse to defined command list in the enum, it will skip line.
                if (!Enum.TryParse(commandString[0], out command))
                {
                    continue;
                }
                else
                {
                    try
                    {                        
                        switch (command)
                        {
                            case CommandEnum.Driver:
                                motorVehicleSystem.RegisterDriver(commandString[1]);
                                break;
                            case CommandEnum.Trip:
                                motorVehicleSystem.AddTrip(commandString[1], commandString[2], commandString[3], double.Parse(commandString[4]));
                                break;
                        }                      
                    }
                    catch
                    { 
                        //Errors encountered will be ignored.
                        continue;               
                    }
                }               
            }

            // Output report.
            foreach (IDriver driver in motorVehicleSystem.GetDriversForReport())
            {
                var report = string.Empty;

                if (driver.GetTotalMiles() == 0)
                {
                    report = string.Format("{0}: {1} miles", driver.Name, driver.GetTotalMiles());
                }
                else
                {
                    report = string.Format("{0}: {1} miles @ {2} mph.", driver.Name, driver.GetTotalMiles(), driver.GetAverageSpeed()); 
                } 

                Console.WriteLine(report);
            }

            Console.WriteLine();
            System.Console.WriteLine("Press <ENTER> to close the console.");
            System.Console.ReadLine();
        }

        private static bool IsStringCommandValid(string[] stringCommand)
        {
            if (stringCommand == null || stringCommand.Length < 2 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory.Models
{
    interface IMotorVehicleSystem
    {
        /// <summary>
        /// Registers driver in the system.
        /// </summary>
        /// <param name="driverName"></param>
        void RegisterDriver(string driverName);

        /// <summary>
        /// Adds trip to driver for given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="totalMiles"></param>
        void AddTrip(string name, string start, string end, double totalMiles);

        /// <summary>
        /// Retrieves all the drivers in the system.
        /// Converts to list to allow for LINQ syntax.
        /// </summary>
        /// <returns></returns>
        List<IDriver> GetAllDrivers();


        /// <summary>
        /// Retrieves all the drivers in the system for report. This list excludes drivers who average less than 5 mph or over 100mph.
        /// Converts to list to allow for LINQ syntax.
        /// </summary>
        /// <returns></returns>
        List<IDriver> GetDriversForReport();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriver
    {
        string Name {get; set;}

        /// <summary>
        /// Allow you to add a trip to the driver in context.
        /// </summary>
        /// <param name="start">Start time of the trip.</param>
        /// <param name="end">End time of the trip.</param>
        /// <param name="totalMiles">Total Miles driven.</param>
        void AddTrip(TimeSpan start, TimeSpan end, double totalMiles);

        /// <summary>
        /// Retrieves the average speed of the all trips for the driver in context.
        /// </summary>
        /// <returns></returns>
        int GetAverageSpeed();

        /// <summary>
        /// Retrieves the total number of miles driven across all trips.
        /// </summary>
        /// <returns></returns>
        int GetTotalMiles();
    }
}

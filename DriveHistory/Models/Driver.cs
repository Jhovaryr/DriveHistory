using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory.Models
{
    public class Driver : IDriver
    {   
        private List<Trip> TripHistory { get; set; }

        public string Name { get; set; }

        public Driver(string Name)
        {
            this.Name = Name;
            this.TripHistory = new List<Trip>();
        }

        public void AddTrip(TimeSpan start, TimeSpan end, double totalMiles)
        {
            var newTrip = new Trip(start, end, totalMiles);

            this.TripHistory.Add(newTrip);
        }

        public int GetAverageSpeed()
        {
            if (this.TripHistory.Count > 0)
            {
                var totalDistance = this.TripHistory.Sum(r => r.totalMilesRounded);
                var totalElapsetime = this.TripHistory.Sum(r => r.GetElapseTime().TotalHours);

                if (totalDistance == 0.0)
                {
                    return 0;
                }

                if (totalElapsetime == 0.0)
                {
                    throw new Exception("Total ElapseTime cannot be zero.");
                }

                double averageSpeed = totalDistance / totalElapsetime;

                int roundedAverageSpeed = (int)Math.Round(averageSpeed, MidpointRounding.AwayFromZero);

                return (int)roundedAverageSpeed;
            }
            else
            {
                return 0;
            } 
        }

        public int GetTotalMiles()
        {
            if (this.TripHistory.Count > 0)
            {
                double averageSpeedSum = this.TripHistory.Sum(r => r.totalMilesRounded);

                return (int)averageSpeedSum;
            }
            else
            {
                return 0;
            } 
        }
    }
}

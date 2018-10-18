using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DriveHistory.Models
{
    public class Trip : ITrip
    {
        public TimeSpan StartTime { get; private set; }

        public TimeSpan EndTime { get; private set; }

        public double totalMilesRounded { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="totalMiles"></param>
        public Trip(TimeSpan start, TimeSpan end, double totalMiles)
        {
            this.StartTime = start;
            this.EndTime = end;
            this.totalMilesRounded = Math.Round(totalMiles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetElapseTime()
        {
            if (this.StartTime > this.EndTime)
            {
                throw new Exception("Start Time cannot be after end time.");
            }

            TimeSpan elapseTime = this.EndTime - this.StartTime;

            return elapseTime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory.Models
{
    public interface ITrip
    {
        /// <summary>
        /// Retrieves the elapse time for trip in context.
        /// </summary>
        /// <returns></returns>
        TimeSpan GetElapseTime();
    }
}
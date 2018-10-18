using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveHistory.Models
{
    public class RootMotorVehicleSystem : IMotorVehicleSystem
    {
        private Dictionary<string, IDriver> Drivers { get; set; }

        public RootMotorVehicleSystem()
        {
            this.Drivers = new Dictionary<string, IDriver>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void RegisterDriver(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty when registering Driver.", "Name");
            }

            if (DoesDriverExist(name))
            {
                throw new Exception("Cannot register driver where the name already exists.");
            }
            else 
            {
                var newDriver = new Driver(name);
                Drivers.Add(name, newDriver);
            }

        }

        public void AddTrip(string name, string start, string end, double totalMiles)
        {
            TimeSpan startTimeSpan;
            TimeSpan endTimeSpan;

            TimeSpan.TryParse(start, CultureInfo.InvariantCulture, out startTimeSpan);
            TimeSpan.TryParse(end, CultureInfo.InvariantCulture, out endTimeSpan);

            if (DoesDriverExist(name))
            {
                this.Drivers[name].AddTrip(startTimeSpan, endTimeSpan, totalMiles);
            }
            else
            {
                throw new Exception("Driver is not registered.");
            }
        }

        public List<IDriver> GetAllDrivers()
        {
            return this.Drivers.Values.ToList();
        }
        
        public List<IDriver> GetDriversForReport()
        {
            return this.Drivers.Values.Where(x => x.GetAverageSpeed() >= 5 && x.GetAverageSpeed() <= 100 || x.GetAverageSpeed() == 0).OrderByDescending(t => t.GetTotalMiles()).ToList();
        }

        private bool DoesDriverExist(string name)
        {
            return this.Drivers.ContainsKey(name);
        }

    }
}

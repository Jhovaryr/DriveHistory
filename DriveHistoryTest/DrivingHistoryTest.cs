using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DriveHistory.Models;
using System.Globalization;
using System.Linq;

namespace DriveHistoryTest
{
    /// <summary>
    /// This class tests multiple classes for simplicity.
    /// </summary>
    [TestClass]
    public class DrivingHistoryTest
    {
        /// <summary>
        /// Tests an happy path scenario of registering a new driver.
        /// </summary>
        [TestMethod]
        public void RootMotorVehicleSystem_RegisterNewDriverTest()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();
            motorVehicleSystem.RegisterDriver("Janes");
            
            //Act 
            bool result = motorVehicleSystem.GetAllDrivers().Any(t => t.Name.Equals("Janes", StringComparison.OrdinalIgnoreCase));

            //Assert
            Assert.IsTrue(result, "Driver Janes should exist");
        }

        /// <summary>
        /// Cannot register driver with same name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RootMotorVehicleSystem_RegisterDriver_NameAlreadyExists_Throws()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();
            motorVehicleSystem.RegisterDriver("Janes");

            //Act 
            motorVehicleSystem.RegisterDriver("Janes");

            //Assert
            //Exception 
        }

        /// <summary>
        /// Cannot register driver with empty string name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RootMotorVehicleSystem_RegisterDriver_EmptyString_Throws()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();

            //Act 
            motorVehicleSystem.RegisterDriver(string.Empty);

            //Assert
            //Exception
        }
        
        /// <summary>
        /// Happy path test to add a trip to a registered driver.
        /// </summary>
        [TestMethod]
        public void RootMotorVehicleSystem_AddTrip_DriverRegistered()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();
            motorVehicleSystem.RegisterDriver("Janes");

            //Act 
            motorVehicleSystem.AddTrip("Janes", "11:35", "11:45", 234);

            //Assert
            bool result = motorVehicleSystem.GetAllDrivers().Any(t => t.Name.Equals("Janes", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(result, "Driver Janes should exist");
        }

        /// <summary>
        /// Cannot add a trip to a unregistered driver.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RootMotorVehicleSystem_AddTrip_DriverNotRegistered()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();

            //Act 
            motorVehicleSystem.AddTrip("Janes", "11:35", "11:45", 234);

            //Assert
            //Exception
        }
        
        /// <summary>
        /// Slow Drivers.
        /// </summary>
        [TestMethod]
        public void RootMotorVehicleSystem_GetListOfDrivers_ExcludeDriversLessThan5mph()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();
            motorVehicleSystem.RegisterDriver("Janes");

            //Act 
            motorVehicleSystem.AddTrip("Janes", "11:35", "12:35", 4);

            //Assert
            Assert.AreEqual(0, motorVehicleSystem.GetDriversForReport().Count);
        }

        /// <summary>
        /// Speed Demons.
        /// </summary>
        [TestMethod]
        public void RootMotorVehicleSystem_GetListOfDrivers_ExcludeDriversMoreThan100Mph()
        {
            //Arrange 
            var motorVehicleSystem = new RootMotorVehicleSystem();
            motorVehicleSystem.RegisterDriver("Janes");

            //Act 
            motorVehicleSystem.AddTrip("Janes", "11:35", "12:35", 250);

            //Assert
            Assert.AreEqual(0, motorVehicleSystem.GetDriversForReport().Count);
        }
        
        /// <summary>
        /// Total miles should be zero for a registered driver with no trips.
        /// </summary>
        [TestMethod]
        public void Driver_GetTotalMiles_NoAddedTrips()
        {
            //Arrange 
            var driver = new Driver("John");

            //Act 
            var totalMilesResult = driver.GetTotalMiles();

            //Assert
            Assert.AreEqual(totalMilesResult, 0);
        }

        /// <summary>
        /// Average speeds should be zero for a registered driver with no trips.
        /// </summary>
        [TestMethod]
        public void Driver_GetAverageSpeed_NoAddedTrips()
        {            
            //Arrange 
            var driver = new Driver("John");

            //Act 
            var totalMilesResult = driver.GetAverageSpeed();

            //Assert
            Assert.AreEqual(0, totalMilesResult);
        }

        /// <summary>
        /// Tests the average speed of driver with one trips.
        /// </summary>
        [TestMethod]
        public void Driver_GetAverageSpeed_OneTrip()
        {
            //Arrange 
            var driver = new Driver("John");
            var startTime = TimeSpan.ParseExact("12:01", "h\\:mm", CultureInfo.InvariantCulture);
            var endTime = TimeSpan.ParseExact("13:16", "h\\:mm", CultureInfo.InvariantCulture);
                        
            //Act 
            driver.AddTrip(startTime, endTime, 42.0);
            
            //Assert
            Assert.AreEqual(34, driver.GetAverageSpeed());
        }

        /// <summary>
        /// Tests the average speed of driver with two trips.
        /// </summary>
        [TestMethod]
        public void Driver_GetAverageSpeed_TwoTrips()
        {            
            //Arrange 
            var driver = new Driver("John");
            var startTimeTrip1 = TimeSpan.ParseExact("07:15", "h\\:mm", CultureInfo.InvariantCulture);
            var endTimeTrip1 = TimeSpan.ParseExact("07:45", "h\\:mm", CultureInfo.InvariantCulture);
            var startTimeTrip2 = TimeSpan.ParseExact("06:12", "h\\:mm", CultureInfo.InvariantCulture);
            var endTimeTrip2 = TimeSpan.ParseExact("06:32", "h\\:mm", CultureInfo.InvariantCulture);

            //Act 
            driver.AddTrip(startTimeTrip1, endTimeTrip1, 17.3);
            driver.AddTrip(startTimeTrip2, endTimeTrip2, 21.8);

            //Assert
            Assert.AreEqual(47, driver.GetAverageSpeed());
        }

        /// <summary>
        /// Verifies edge case where the distance is zero, it should result in 0 mph.
        /// </summary>
        [TestMethod]
        public void Driver_GetAverageSpeed_TotalMilesIsZero()
        {
            //Arrange 
            var driver = new Driver("John");
            var startTime = TimeSpan.ParseExact("11:35", "h\\:mm", CultureInfo.InvariantCulture);
            var endTime = TimeSpan.ParseExact("11:45", "h\\:mm", CultureInfo.InvariantCulture);

            //Act 
            driver.AddTrip(startTime, endTime, 0.0);

            //Assert
            Assert.AreEqual(0, driver.GetAverageSpeed());
        }

        /// <summary>
        /// When elapse time is zero, this should throw error.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Driver_GetAverageSpeed_ElapseTimeIsZero()
        {
            //Arrange 
            var driver = new Driver("John");
            var startTime = TimeSpan.ParseExact("11:35", "h\\:mm", CultureInfo.InvariantCulture);
            driver.AddTrip(startTime, startTime, 234);

            //Act 
            var averageSpeed = driver.GetAverageSpeed();
            //Assert
        }

        /// <summary>
        /// Multiple zero miles trips should still return 0 mph.
        /// </summary>
        [TestMethod]
        public void Driver_GetTotalMiles_TripsAllZero()
        {
            //Arrange 
            var driver = new Driver("John");
            var startTime = TimeSpan.ParseExact("11:35", "h\\:mm", CultureInfo.InvariantCulture);
            var endTime = TimeSpan.ParseExact("11:45", "h\\:mm", CultureInfo.InvariantCulture);

            //Act 
            driver.AddTrip(startTime, endTime, 0.0);
            driver.AddTrip(startTime, endTime, 0.0);

            //Assert
            Assert.AreEqual(0, driver.GetTotalMiles());
        }

        /// <summary>
        /// Tests if the start time is greater that the end time.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Trip_GetElapseTime_StartTimeGreaterThanEndTime_Throws()
        {
            //Arrange 
            var startTimeTrip1 = TimeSpan.ParseExact("07:15", "h\\:mm", CultureInfo.InvariantCulture);
            var endTimeTrip1 = TimeSpan.ParseExact("07:45", "h\\:mm", CultureInfo.InvariantCulture);
            var trip = new Trip(endTimeTrip1, startTimeTrip1, 345.3);

            //Act 
            var elapseTime = trip.GetElapseTime();

            //Assert
            //Throws exception
        }

        /// <summary>
        /// Tests the trip class when start time equals end time.
        /// </summary>
        [TestMethod]
        public void Trip_GetElapseTime_StartTimeEqualsEndTime()
        {
            //Arrange 
            var startTimeTrip1 = TimeSpan.ParseExact("07:15", "h\\:mm", CultureInfo.InvariantCulture);
            var endTimeTrip1 = TimeSpan.ParseExact("07:15", "h\\:mm", CultureInfo.InvariantCulture);
            var trip = new Trip(startTimeTrip1, endTimeTrip1, 345.3);

            //Act 
            var elapseTime = trip.GetElapseTime();

            //Assert
            Assert.AreEqual(TimeSpan.Zero, elapseTime);
        }
    }
}

namespace TheTankGame.Tests
{
    using NUnit.Framework;
    // using System;
    //
    // using TheTankGame.Core;
    // using TheTankGame.Core.Contracts;
    // using TheTankGame.Entities.Miscellaneous;
    // using TheTankGame.Entities.Vehicles;
    // using TheTankGame.Entities.Vehicles.Contracts;
    // using TheTankGame.Entities.Vehicles.Factories;

    [TestFixture]
    public class BaseVehicleTests
    {
        [Test]
        // ZeroParams;
        public void SuccessfullINitialization()
        {
            BaseVehicle vehicle = new Revenger("SS", 1000, 1000, 1000, 1000, 1000);
            Assert.AreEqual(vehicle.Model, "SS");
            Assert.AreEqual(vehicle.Weight, 1000);
            Assert.AreEqual(vehicle.HitPoints, 1000);
            Assert.AreEqual(vehicle.Model, "SS");
        }
    }
       
     
}
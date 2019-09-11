using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheTankGame.Entities.Miscellaneous;
using TheTankGame.Entities.Miscellaneous.Contracts;
using TheTankGame.Entities.Vehicles.Contracts;
using TheTankGame.Entities.Vehicles.Factories.Contracts;

namespace TheTankGame.Entities.Vehicles.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string vehicleType, string model, double weight, decimal price, int attack, int defense, int hitPoints)
        {
            IAssembler assembler = new VehicleAssembler(model, weight, price, attack, defense, hitPoints);

            var vehicleTypes = Assembly.GetCallingAssembly().GetTypes()
                 .Where(t => typeof(IVehicle).IsAssignableFrom(t) && !t.IsAbstract)
                 .ToArray();

            var carType = vehicleTypes.FirstOrDefault(t => t.Name.Contains(vehicleType));

            var vehicle = (IVehicle)Activator.CreateInstance(carType, new object[] { model, weight, price, attack, defense, hitPoints, assembler });

            return vehicle;
        }
    }
}

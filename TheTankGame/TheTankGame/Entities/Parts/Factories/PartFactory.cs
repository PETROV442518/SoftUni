using System;
using System.Linq;
using System.Reflection;
using TheTankGame.Entities.Parts.Contracts;
using TheTankGame.Entities.Parts.Factories.Contracts;

namespace TheTankGame.Entities.Parts.Factories
{
    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partType, string model, double weight, decimal price, int additionalParameter)
        {
            var partTypes = Assembly.GetCallingAssembly().GetTypes()
                 .Where(t => typeof(IPart).IsAssignableFrom(t) && !t.IsAbstract)
                 .ToArray();

            var pType = partTypes.FirstOrDefault(t => t.Name.Contains(partType));

            var part = (IPart)Activator.CreateInstance(pType, new object[] { model, weight, price, additionalParameter} );

            return part;
        }
    }
}

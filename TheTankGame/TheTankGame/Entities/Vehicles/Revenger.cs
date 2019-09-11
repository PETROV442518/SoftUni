using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TheTankGame.Entities.Miscellaneous;
using TheTankGame.Entities.Miscellaneous.Contracts;

namespace TheTankGame.Entities.Vehicles
{
   
    public class Revenger : BaseVehicle
    {
        private readonly IList<string> orderedParts;

        public Revenger(string model, double weight, decimal price, int attack, int defense, int hitPoints) : base(model, weight, price, attack, defense, hitPoints)
        {
        }

        public Revenger(string model, double weight, decimal price, int attack, int defense, int hitPoints, IAssembler assembler)
            : base(model, weight, price, attack, defense, hitPoints, assembler)
        {
            
        }

        
    }
}

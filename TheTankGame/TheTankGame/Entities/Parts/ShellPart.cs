namespace TheTankGame.Entities.Parts
{
    using Contracts;

    public class ShellPart : BasePart, IDefenseModifyingPart
    {
        
        public ShellPart(string model, double weight, decimal price, int defensiveModifier) : base(model, weight, price)
        {
            this.DefenseModifier = defensiveModifier;
        }

        public int DefenseModifier { get; private set; }
    }
}
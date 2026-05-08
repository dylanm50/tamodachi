using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class Food
    {
        Global.FoodNames name;

        public double price { get; }

        public Global.PersonalityNames personality { get; }

        public Food(Global.FoodNames name, double price, Global.PersonalityNames personality)
        {
            if (price <= 0)
            {
                throw new ArgumentException(price.ToString());
            }

            this.name = name;
            this.price = price;
            this.personality = personality;
        }

        public override string ToString()
        {
            if (name == Global.FoodNames.mcChicken)
            {
                return "Mc Chicken";
            }

            return name.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Food)
            {
                Food food = (Food) obj;
                
                return name == food.name;
            }
            
            return base.Equals(obj);
        }
    }
}

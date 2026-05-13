using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace tamodachi
{
    public class FoodItem
    {
        public Global.FoodNames food { get; }
        public int amount { get; private set; }

        public FoodItem(Global.FoodNames food)
        {
            this.food = food;
            amount = 1;
        }

        public FoodItem(Global.FoodNames food, int amount)
        {
            this.food = food;
            this.amount = amount;
        }

        public void Add()
        {
            amount ++;
        }

        public bool Subtract()
        {
            if (amount - 1 >= 0)
            {
                amount --;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Global.FoodNameToString(food);
        }

        public override bool Equals(object? obj)
        {
            if (obj is FoodItem)
            {
                FoodItem f = (FoodItem)obj;

                return f.food == this.food;
            }else if (obj is Food)
            {
                Food f = (Food) obj;

                return f.name == food;
            }

            return false;
        }
    }
}

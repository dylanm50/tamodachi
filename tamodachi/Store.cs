using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class Store
    {
        public Food[] stock { get; }

        public Store(Food[] stock)
        {
            if (stock.Length == 3)
            {
                this.stock = stock;
            }else
            {
                throw new ArgumentException(stock.Length.ToString());
            }
        }

        public Food Buy(ref double wallet, int food, ref string message)
        {
            Food item = stock[food];
            double newWallet = wallet - item.price;

            if (newWallet >= 0)
            {
                wallet = newWallet;

                message = $"You have bought {item} for {item.price:C}.\nYour new balance is {wallet:C}.";

                return item;
            }

            message = $"You don't have the funds to buy that!";

            return null;
        }

        public override string ToString()
        {
            string s = "Welcome to the superdubermarket!\n\t";
            
            foreach (Food f in stock)
            {
                s += $"{f}: {f.price:C},";
            }

            s.Remove(s.Length - 1);

            return s;
        }
    }
}

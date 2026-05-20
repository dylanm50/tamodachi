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
        
        /// <summary>
        /// Buys an item
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="food"></param>
        /// <param name="message">message that will be displayed</param>
        /// <returns></returns>
        public Food Buy(ref double wallet, int food, ref string message)
        {
            Food item = stock[food];

            return Global.BuyItem(ref wallet, item,ref message);
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

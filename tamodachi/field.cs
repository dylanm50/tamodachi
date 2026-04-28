using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class Field
    {
        public int value { get; }

        public Field(int value)
        {
            if (value > -5 && value < 5)
            {
                if (value == 0)
                {
                    throw new ArgumentException(value.ToString());
                }
                
                this.value = value;
            }else
            {
                throw new ArgumentOutOfRangeException(value.ToString());
            }
        }
    }
}

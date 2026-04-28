using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class AttributeRange
    {
        Field[] range;

        public AttributeRange(int[] range)
        {
            if (range.Length == 2)
            {
                if (range[0] < range[1])
                {
                    this.range = new Field[] { new Field(range[0]), new Field(range[1]) };
                }
                else
                {
                    throw new ArgumentException(range[0].ToString());
                }
            }else
            {
                throw new ArgumentException(range.Length.ToString());
            }
        }

        public bool Match(Field value)
        {
            int i = value.value;
            
            return i >= range[0].value && i <= range[1].value;
        }
    }
}

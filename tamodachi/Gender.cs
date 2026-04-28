using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class Gender
    {
        public Program.Egender gender { get; }


        public Gender(Program.Egender gender)
        {
            this.gender = gender;
        }

        public override string ToString()
        {
            if (gender == Program.Egender.nonBinary)
            {
                return "non binary";
            }

            return gender.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Gender)
            {
                Gender g = (Gender) obj;
                
                return g.gender == gender;
            }
            
            return false;
        }
    }
}

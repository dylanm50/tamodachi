using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class phrase
    {
        string message;
        public System.TimeOnly[] timing { get; }

        public phrase(string message, System.TimeOnly[] timing)
        {
            if(timing.Length != 2)
            {
                throw new ArgumentException(timing.Length.ToString());
            }

            if (timing[0] > timing[1])
            {
                throw new ArgumentException(timing[0].ToString());
            }

            this.message = message;
            this.timing = timing;
        }

        public override string ToString()
        {
            return message;
        }
    }
}

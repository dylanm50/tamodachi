using System;
using System.Collections.Generic;
using System.Text;
using static tamodachi.Program;

namespace tamodachi
{
    public class Personality
    {
        public Global.PersonalityNames name { get; }

        AttributeRange m;
        AttributeRange s;
        AttributeRange e;
        AttributeRange t;
        AttributeRange n;

        public List<phrase> phrases { get; }

        public Personality
        (
            Global.PersonalityNames name,

            int[] m,
            int[] s,
            int[] e,
            int[] t,
            int[] n,

            phrase[] phrases
        )
        {
            this.name = name;

            this.m = new AttributeRange(m);
            this.s = new AttributeRange(s);
            this.e = new AttributeRange(e);
            this.t = new AttributeRange(t);
            this.n = new AttributeRange(n);

            this.phrases = phrases.ToList();
        }

        public bool Match(Field fM, Field fS, Field fE, Field fT, Field fN)
        {
            return m.Match(fM) && s.Match(fS) && e.Match(fE) && t.Match(fT) && n.Match(fN);
        }

        public override string ToString()
        {
            return name.ToString();
        }
    }
}

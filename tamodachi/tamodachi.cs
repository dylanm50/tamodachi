using System;
using System.Collections.Generic;
using System.Text;
using static tamodachi.Program;

namespace tamodachi
{   
    public class Tamodachi
    {
        public string name { get; }
        
        Field Movement;
        Field Speech;
        Field Energy;
        Field Thinking;
        Field Normal;
        
        public Gender gender { get; }
        public List<Gender> fancies { get; }

        public Personality Personality { get; }

        public Tamodachi
        (
            string name,
            int m, int s, int e, int t, int n,
            Program.Egender gender, Program.Egender[] fancies
        )
        {
            this.name = name;

            Movement = new Field(m);
            Speech   = new Field(s);
            Energy   = new Field(e);
            Thinking = new Field(t);
            Normal   = new Field(n);

            bool check = true;

            foreach (Personality p in Program.Personalities)
            {
                if (p.Match(Movement, Speech, Energy, Thinking, Normal))
                {
                    Personality = p;

                    check = false;

                    break;
                }
            }

            // no personalities match
            if (check)
            {
                throw new ArgumentException();
            }

            this.gender = new Gender(gender);

            this.fancies = new List<Gender>();

            foreach (Egender g in fancies)
            {   
                this.fancies.Add(new Gender(g));
            }

            foreach (Gender g in this.fancies)
            {
                if (this.fancies.FindAll(new Predicate<Gender>(n => g.Equals(n))).Count > 1)
                {
                    throw new ArgumentException();
                }
            }
        }

        private bool MatchG(Tamodachi person)
        {
            foreach (Gender gender in fancies)
            {
                if (person.gender.Equals(gender))
                {
                    foreach (Gender g2 in person.fancies)
                    {
                        if (this.gender.Equals(g2))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        public bool Match(Tamodachi person)
        {
            if (MatchG(person))
            {
                PersonalityNames contempt = Program.PersonalityNames.contempt;
                PersonalityNames a = Personality.name;
                PersonalityNames b = person.Personality.name;

                if (a == contempt || b == contempt)
                {
                    return true;
                }

                return a == b;
            }

            return false;
        }
        
        string Say(string message)
        {
            return $"{this.name}: \"{message}\"";
        }

        public string Talk(string backupMessage)
        {
            phrase[] filteredPhrases = 
                Personality.phrases.FindAll
                (
                    n => (n.timing[1] >= Program.time) && (n.timing[0] <= Program.time)
                ).ToArray();

            int length = filteredPhrases.Length;

            if (length == 0)
            {
                return Say(backupMessage);
            }

            int i = new Random().Next(length);
            
            return Say(filteredPhrases[i].ToString());
        }

        public string MatchToString(Tamodachi person)
        {
            if (Match(person))
            {
                return $"{name} and {person.name} would make a lovely couple!";
            }

            return $"{name} and {person.name} could not get involved romanticaly";
        }

        public override string ToString()
        {
            string s = $"Name: {name}\n";

                   s += String.Format("\t{0,11}: {1}\n", "Personality", Personality);
                   s += String.Format("\t{0,11}: {1}\n", "Gender"     , gender     );
                   s += String.Format("\t{0,11}:"      , "Fancies"                 );

            // Adding each gender
            foreach (Gender g in fancies)
            {
                s += $"{g} ";
            }

            if (fancies.Count == 0)
            {
                s += "Noone";
            }

            return s;
        }
    }
}

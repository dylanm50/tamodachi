using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using static tamodachi.Program;

namespace tamodachi
{   
    public class Tamodachi
    {
        public string name { get; }
        
        public Field Movement { get; }
        public Field Speech { get; }
        public Field Energy { get; }
        public Field Thinking { get; }
        public Field Normal { get; }
        
        public Gender gender { get; }
        public List<Gender> fancies { get; }

        public Personality Personality { get; }

        public List<FoodRelationship> foods = new List<FoodRelationship>();

        // This is who I am :)
        public Tamodachi
        (
            string name,
            int m, int s, int e, int t, int n,
            Global.Egender gender, Global.Egender[] fancies
        )
        {
            this.name = name.Trim();

            Movement = new Field(m);
            Speech   = new Field(s);
            Energy   = new Field(e);
            Thinking = new Field(t);
            Normal   = new Field(n);

            bool check = true;

            foreach (Personality p in Global.Personalities)
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

            foreach (Global.Egender g in fancies)
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

        public Tamodachi
        (
            string name,
            int m, int s, int e, int t, int n,
            Global.Egender gender, Global.Egender[] fancies,
            FoodRelationship[] foodrelationship
        ): this
           (
            name,
            m, s, e, t, n,
            gender, fancies
           )
        {   
            foods = foodrelationship.ToList();
        }

        // Can I fuck this person?
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

        // Do I like this person?
        public bool Like(Tamodachi person)
        {
            return 
                Personality.name == person.Personality.name ||
                (
                    Personality.name == Global.PersonalityNames.contempt ||
                    person.Personality.name == Global.PersonalityNames.contempt
                );
        }

        // Should I fuck this person?
        public bool Match(Tamodachi person)
        {
            if (MatchG(person))
            {
                Global.PersonalityNames contempt = Global.PersonalityNames.contempt;
                Global.PersonalityNames a = Personality.name;
                Global.PersonalityNames b = person.Personality.name;

                if (a == contempt || b == contempt)
                {
                    return true;
                }

                return a == b;
            }

            return false;
        }
        
        // Say the thing
        public string Say(string message)
        {
            return $"{this.name}: \"{message}\"";
        }

        // Say something
        public string Talk(string backupMessage, System.TimeOnly time)
        {
            phrase[] filteredPhrases = 
                Personality.phrases.FindAll
                (
                    n => (n.timing[1] >= time) && (n.timing[0] <= time)
                ).ToArray();

            int length = filteredPhrases.Length;

            if (length == 0)
            {
                return Say(backupMessage);
            }

            int i = new Random().Next(length);
            
            return Say(filteredPhrases[i].ToString());
        }


        // Have a chat (eww it looks like javascript)
        public string Conversation(string o, Tamodachi person, int level)
        {
            if(level <0)
            {
                throw new ArgumentOutOfRangeException(level.ToString());
            }
            
            string ind = Global.indent(level);

            string s = $"{ind}Conversation between {this.name} and {person.name}\n";

            s += $"{ind}\t{Say($"Hello {person.name}, what do you think about {o}?")}";

            if (Like(person))
            {
                s += $"\n{ind}\t{person.Say($"Yo I love {o}")}";
            }else
            {
                s += $"\n{ind}\t{person.Say($"I hate {o}")}";
            }

            return s;
        }

        public string Eat(Food food, int level)
        {   
            FoodRelationship relationship = null;

            string ind = Global.indent(level);

            bool check = true;

            foreach (FoodRelationship r in foods)
            {
                if (r.food == food)
                {
                    check = false;

                    relationship = r;

                    //Console.WriteLine("FOOD EXISTS");

                    break;
                }
            }

            if (check)
            {
                //Console.WriteLine("FOOD DOESNT EXIST");

                relationship = new FoodRelationship(food.name, this);

                foods.Add(relationship);
            }
            

            string start = $"{ind}{name} is eating {food}\n{ind}\t";

            if (relationship.like < 5)
            {
                return $"{start}{name} didn't really like it";
            }
            else if (relationship.like < 6)
            {
                return $"{start}{name} thought it was alright";
            }
            else if (relationship.like < 10)
            {
                return $"{start}{name} really liked it";
            }else
            {
                return $"{start}{name} REALLY liked it!!!";
            }
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

                   s += String.Format("\t\t\t{0,11}: {1}\n", "Personality", Personality);
                   s += String.Format("\t\t\t{0,11}: {1}\n", "Gender"     , gender     );
                   s += String.Format("\t\t\t{0,11}:"      , "Fancies"                 );

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

using System;
using System.Collections.Generic;
using System.Numerics;
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

        public List<FoodRelationship> foods { get; } = new List<FoodRelationship>();

        public int XP;

        public List<phrase> phrases { get; } = new List<phrase>();

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
            FoodRelationship[] foodrelationship,
            int xp,
            phrase[] phrases
        ): this
           (
            name,
            m, s, e, t, n,
            gender, fancies
           )
        {   
            foods = foodrelationship.ToList();
            XP = xp;
            this.phrases = phrases.ToList();
        }

        /// <summary>
        /// Checks if tamodachi is the same gender as person
        /// </summary>
        /// <param name="person">the person who will be liked</param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if tamadochai and person will get along
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool Like(Tamodachi person)
        {
            return 
                Personality.name == person.Personality.name ||
                (
                    Personality.name == Global.PersonalityNames.contempt ||
                    person.Personality.name == Global.PersonalityNames.contempt
                );
        }

        /// <summary>
        /// Checks if tamodachi and person can be in a romantic relationship
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Tamodachi will say a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Say(string message)
        {
            return $"{this.name}: \"{message}\"";
        }

        /// <summary>
        /// Tamodachi will say a phrase
        /// </summary>
        /// <param name="backupMessage">Backup message if no phrases can be said</param>
        /// <param name="time">Current time</param>
        /// <returns></returns>
        public string Talk(string backupMessage, System.TimeOnly time)
        {
            List<phrase> filteredPhrases =
                Personality.phrases.FindAll
                (
                    n => (n.timing[1] >= time) && (n.timing[0] <= time)
                );

            phrase[] filteredPhrasesA = filteredPhrases.Concat(phrases).ToArray();

            int length = filteredPhrasesA.Length;

            if (length == 0)
            {
                return Say(backupMessage);
            }

            int i = new Random().Next(length);
            
            return Say(filteredPhrasesA[i].ToString());
        }


        /// <summary>
        /// Two tamodachis will have a conversation about an object
        /// </summary>
        /// <param name="o">The object</param>
        /// <param name="person">The other person</param>
        /// <param name="level">The indentation level</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Gets the current level of the tamodachi
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return XP / Global.XPinLevel;
        }

        /// <summary>
        /// Tamodachi will eat a food
        /// </summary>
        /// <param name="food">The food</param>
        /// <param name="level">Indentation level</param>
        /// <param name="display">Function to display the output</param>
        /// <param name="input">Function to get user input in the case of a level up</param>
        public void Eat(Food food, int level, Action<string> display, Func<string> input)
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

            int levelBefore = GetLevel();
            
            int gain = relationship.like * 100;
            
            XP += gain;

            int levelAfter = GetLevel();    

            string start = $"{ind}{name} is eating {food}\n{ind}\t";

            if (relationship.like < 5)
            {
                display($"{start}{name} didn't really like it");
            }
            else if (relationship.like < 6)
            {
                display($"{start}{name} thought it was alright");
            }
            else if (relationship.like < 10)
            {
                display($"{start}{name} really liked it");
            }else
            {
                display($"{start}{name} REALLY liked it!!!");
            }

            string levelS = "";
            string levelUpS = "";
            bool levelUp = false;

            if (levelAfter == levelBefore)
            {
                levelS = $"\n{name} is level {levelAfter}";
            }
            else
            {
                display($"{ind}\t{name} is now level {levelAfter}!");

                levelUp = true;
            }

            display($"{ind}\t{name} gained {gain} XP!{levelS}");

            if(levelUp)
            {
                LevelUp(display, input);
            }
        }

        /// <summary>
        /// Function to get a new phrase for leveling up
        /// </summary>
        /// <param name="display">Function to display messages</param>
        /// <param name="input">Function to get input</param>
        void LevelUp(Action<string> display, Func<string> input)
        {
            display($"write a new phrase for {name}");

            string phraseText = input();

            phrase phrase = new phrase(phraseText, new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) });

            phrases.Add(phrase);
        }

        /// <summary>
        /// Displays if two tamodachis would date
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
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
                s += $" {g}";
            }

            if (fancies.Count == 0)
            {
                s += " Noone";
            }

            return s;
        }
    }
}

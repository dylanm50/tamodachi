using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

namespace tamodachi
{
    // Static public vars that need to be accessed throughout many classes.
    // Kept seperate from program as these are constant amongst all programs
    // Also contains the entry point
    public class Global
    {
        public enum PersonalityNames
        {
            happy,
            sad,
            contempt
        };

        public enum Egender
        {
            male,
            female,
            nonBinary
        };

        public static Egender StringToEgender(string s)
        {
            switch(s)
            {
                case "male":
                    return Egender.male;
                case "female":
                    return Egender.female;
                case "non binary":
                    return Egender.nonBinary;
                default:
                    throw new ArgumentException(s);
            }
               
        }

        public enum FoodNames
        {
            pizza,
            sushi,
            burger,
            mcChicken
        };

        public static FoodNames StringToFoodName(string s)
        {
            switch (s)
            {
                case "pizza":
                    return FoodNames.pizza;
                case "sushi":
                    return FoodNames.sushi;
                case "burger":
                    return FoodNames.burger;
                case "Mc Chicken":
                    return FoodNames.mcChicken;
                default:
                    throw new ArgumentException(s);
            }
        }

        // I LOVE ARRAYS IN C# (JAVA SUCKS ASS)
        public static Personality[] Personalities =
        {
            new Personality
            (
                PersonalityNames.happy,

                new int[] { -4, 4 }, //m
                new int[] { -4, 4 }, //s
                new int[] { 1, 4 }, //e
                new int[] { -4, -1 }, //t
                new int[] { 1, 4 }, //n

                new phrase[]
                {
                    new phrase
                    (
                        "I love everything",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I have so much energy",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I look forward to tomorrow",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    )
                }
            ),

            new Personality
            (
                PersonalityNames.sad,

                new int[] { -4, -1 }, //m
                new int[] { -4, 4 }, //s
                new int[] { -4, -1 }, //e
                new int[] { 1, 4 }, //t
                new int[] { -4, -1 }, //n

                new phrase[]
                {
                    new phrase
                    (
                        "I hate the world",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "Im tired",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I just need to get through today",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    )
                }
            ),

            new Personality
            (
                PersonalityNames.contempt,

                new int[] { -4, 4 }, //m
                new int[] { -4, 4 }, //s
                new int[] { -4, 4 }, //e
                new int[] { -4, 4 }, //t
                new int[] { -4, 4 }, //n

                new phrase[]
                {
                    new phrase
                    (
                        "Meh",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I guess I can do that",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "Tommorow will come i guess",
                        new System.TimeOnly[] { new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    )
                }
            )
        };

        public static Food[] foods =
        {
            new Food(FoodNames.pizza, 5.77, PersonalityNames.happy),
            new Food(FoodNames.sushi, 10.00, PersonalityNames.sad),
            new Food(FoodNames.burger, 7.05, PersonalityNames.contempt),
            new Food(FoodNames.mcChicken, 9.50, PersonalityNames.contempt),
        };

        public static Food FoodNameToFood(FoodNames name)
        {
            foreach(Food f in foods)
            {
                if (f.name == name)
                {
                    return f;
                }
            }

            throw new Exception();
        }

        static void Testing()
        {
            Console.WriteLine("Running a C# tamodachi game container.");
            Console.WriteLine("\tInstance 1:");

            Console.WriteLine("\t\tLoading ...");

            string m = "";
            bool g = false;
            
            Program p = new Program(true, false, ref m, ref g);

            Tamodachi happy = p.tamodachis[0];
            Tamodachi sad = p.tamodachis[1];
            Tamodachi contempt = p.tamodachis[2];

            Console.WriteLine("\t\tFinished loading!");

            Console.WriteLine($"\t\tThe current time is {p.time}");

            Console.WriteLine($"\t\t{happy}");
            Console.WriteLine($"\t\t{sad}");
            Console.WriteLine($"\t\t{contempt}");
            Console.WriteLine($"\t\t{happy.MatchToString(sad)}");
            Console.WriteLine($"\t\t{happy.Talk("ummm idk what to say XD", p.time)}");
            Console.WriteLine($"\t\t{happy.Talk("imm be a STAR", p.time)}");

            string o = p.objects[new Random().Next(p.objects.Count() - 1)];

            Console.WriteLine(happy.Conversation(o, sad, 2));

            Food pizza = foods[0];
            Food sushi = foods[1];
            Food burger = foods[2];

            /*
            Console.WriteLine($"{happy.Eat(pizza, 2)}");
            Console.WriteLine($"{happy.Eat(pizza, 2)}");
            Console.WriteLine($"{happy.Eat(sushi, 2)}");
            Console.WriteLine($"{happy.Eat(burger, 2)}");

            Console.WriteLine($"{sad.Eat(pizza, 2)}");
            Console.WriteLine($"{sad.Eat(sushi, 2)}");
            Console.WriteLine($"{sad.Eat(sushi, 2)}");
            Console.WriteLine($"{sad.Eat(burger, 2)}");

            Console.WriteLine($"{contempt.Eat(pizza, 2)}");
            Console.WriteLine($"{contempt.Eat(sushi, 2)}");
            Console.WriteLine($"{contempt.Eat(burger, 2)}");
            Console.WriteLine($"{contempt.Eat(burger, 2)}");
            */

            Store s = new Store(new Food[] {pizza, sushi, burger });

            p.money = 100;

            string empty = "";

            happy.Eat(pizza, 0);
            p.foods = new List<FoodNames> {FoodNames.mcChicken, FoodNames.burger};

            p.Save();

            /*
            int l = 100000;

            Console.WriteLine($"Initalising {l} programs");

            Program[] programs = new Program[l];

            for (int i = 0; i < l; i ++)
            {
                programs[i] = new Program();
            }

            Console.WriteLine("Finished!");
            */
        }

        public static string FoodNameToString(FoodNames f)
        {
            if (f == FoodNames.mcChicken)
            {
                return "Mc Chicken";
            }

            return f.ToString();
        }

        static int CharToNum(char c)
        {
            return (int) c - 48;
        }

        static int StringToInt(string s)
        {
            if(s.Length == 2)
            {
                return CharToNum(s[1]) * - 1;
            }
            else if (s.Length == 1)
            {
                return CharToNum(s[0]);
            }

            return 0;
        }

        static bool Create(ref Program p, bool manditory)
        {
            string name;
            
            while (true)
            {

                Console.WriteLine("Creating a Guy!");

                string acceptableValues = "(non 0 values within the range of -4-4 are accepted)";

                Console.WriteLine($"How much do they move? {acceptableValues}");

                int m = StringToInt(Console.ReadLine());

                Console.WriteLine($"How much do they talk? {acceptableValues}");

                int s = StringToInt(Console.ReadLine());

                Console.WriteLine($"How much energy do they have? {acceptableValues}");

                int e = StringToInt(Console.ReadLine());

                Console.WriteLine($"How much do they think? {acceptableValues}");

                int t = StringToInt(Console.ReadLine());

                Console.WriteLine($"How quirky are they? {acceptableValues}");

                int n = StringToInt(Console.ReadLine());

                Console.WriteLine("What is their name?");

                name = Console.ReadLine();

                Console.WriteLine("What is their gender (1 for male, 2 for female, 3 for non binary)");

                string gender = Console.ReadLine();
                Egender egender = new Egender();

                switch (gender)
                {
                    case "1":
                        egender = Egender.male;

                        break;
                    case "2":
                        egender = Egender.female;

                        break;
                    default:
                        egender = Egender.nonBinary;

                        break;
                }

                List<Egender> likes = new List<Egender>();

                Console.WriteLine($"Type \"y\" if {name} likes men.");

                if (Console.ReadLine() == "y")
                {
                    likes.Add(Egender.male);
                }

                Console.WriteLine($"Type \"y\" if {name} likes women.");

                if (Console.ReadLine() == "y")
                {
                    likes.Add(Egender.female);
                }

                Console.WriteLine($"Type \"y\" if {name} likes non binaries.");

                if (Console.ReadLine() == "y")
                {
                    likes.Add(Egender.nonBinary);
                }

                Console.WriteLine($"Attempting to create {name}!");

                try
                {
                    p.tamodachis.Add(new Tamodachi(name, m, s, e, t, n, egender, likes.ToArray()));

                    return true;
                }
                catch
                {
                    Console.WriteLine("Creation unsucessful");

                    if (!manditory)
                    {
                        Console.WriteLine("Press \"y\" to abandon");

                        if (Console.ReadLine() == "y")
                        {
                            return false;
                        }
                    }
                }
            }
        }
        public static string indent(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException(n.ToString());
            }

            string s = "";

            for (int i = 0; i < n; i++)
            {
                s += "\t";
            }

            return s;
        }

        static Food Buy(Program p, Store s)
        {
            Console.WriteLine(s);

            Console.WriteLine($"\tYou have {p.Money()}");
            
            string selection = "";

            for(int i = 0; i < s.stock.Length; i ++)
            {
                selection += $"\tPress {i + 1} for {s.stock[i]} for {s.stock[i].price:C}";

                if (i != s.stock.Length - 1)
                {
                    selection += "\n";
                }
            }

            Console.WriteLine(selection);

            int index = CharToNum(Console.ReadLine()[0]) - 1;

            if (index > -1 && index < s.stock.Length)
            {
                String message = "";
                
                Food food = s.Buy(ref p.money, index, ref message);

                Console.WriteLine($"\t{message}");

                p.foods.Add(food.name);

                return food;
            }else
            {
                Console.WriteLine("Incorrect selection");
                
                return null;
            }
        }

        static Program Startup()
        {
            string s = "";
            bool g = false;
            
            Program p = new Program(false, false, ref s, ref g);

            Create(ref p, true);

            Console.WriteLine(p.tamodachis[0]);
            Console.WriteLine(p.tamodachis[0].Say("Im kind of bored..."));
            Console.WriteLine(p.tamodachis[0].Say("Lets invite someone new to the island!"));

            Create(ref p, true);

            Console.WriteLine(p.tamodachis[1]);
            Console.WriteLine(p.tamodachis[1].Say("Im so happy to be here!"));
            Console.WriteLine(p.tamodachis[1].Say("What is your favourite object?"));

            string obj = Console.ReadLine();

            p.objects.Add(obj);

            Console.WriteLine(p.tamodachis[0].Conversation(obj, p.tamodachis[1], 0));

            p.money = 100;

            Console.WriteLine(p.Money());

            return p;
        }

        static void Shopping(ref Program p)
        {
            Console.WriteLine(p.tamodachis[0].Say("Lets go to the store!"));

            Store s = new Store(new Food[] { foods[0], foods[1], foods[3] });

            Food food = null;

            while (food == null)
            {
                food = Buy(p, s);
            }

            while (true)
            {
                Console.WriteLine("Choose someone to give the food to!");

                Console.WriteLine($"Press 1 for {p.tamodachis[0].name}, Press 2 for {p.tamodachis[1].name}");

                int i = CharToNum(Console.ReadLine()[0]) - 1;

                if (i > -1 && i < 2)
                {
                    Console.WriteLine(p.tamodachis[i].Eat(food, 0));

                    break;
                }

                Console.WriteLine("Incorrect selection!");
            }
        }
        
        public static void Main(string[] args)
        {
            /*
            Program p = Startup();

            Shopping(ref p);
            */

            string m = "";
            bool g = false;

            Program p = new Program(true, false, ref m, ref g);

            p.Load();

            Console.WriteLine(p.tamodachis[0].foods[1].food);

            //p.Save();

            //Console.WriteLine(p.Load());
        }
    }
}

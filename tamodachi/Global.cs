using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
            mcChicken,
            ramen,
            porridge,
            KFCvalueMeal,
            bobba,
            jerky,
            TVdinner,
            skittles,
            cake,
            peanuts,
            cola,
            watermellon,
            carbonara,
            assassinsSpaghetti,
            pancakes
        };

        static Dictionary<FoodNames, string> foodDict = new Dictionary<FoodNames, string>
        {
            { FoodNames.pizza             , "pizza"                },
            { FoodNames.sushi             , "sushi"                },
            { FoodNames.burger            , "burger"               },
            { FoodNames.mcChicken         , "Mc Chicken"           },
            { FoodNames.ramen             , "ramen"                },
            { FoodNames.porridge          , "porridge"             },
            { FoodNames.KFCvalueMeal      , "KFC Value Meal"       },
            { FoodNames.bobba             , "bobba"                },
            { FoodNames.jerky             , "jerky"                },
            { FoodNames.TVdinner          , "TV Dinner"            },
            { FoodNames.skittles          , "skittles"             },
            { FoodNames.cake              , "cake"                 },
            { FoodNames.cola              , "cola"                 },
            { FoodNames.watermellon       , "watermellon"          },
            { FoodNames.carbonara         , "carbonara"            },
            { FoodNames.peanuts           , "peanuts"              },
            { FoodNames.assassinsSpaghetti, "Assassin's Spaghetti" },
            { FoodNames.pancakes          , "pancakes"             }
        };

        public static FoodNames StringToFoodName(string s)
        {
            FoodNames food = foodDict.FirstOrDefault(x => x.Value == s).Key;

            if (food.Equals(default(KeyValuePair<FoodNames, string>)))
            {
                throw new ArgumentException(s);
            }

            return food;
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

        // don't add prices that are more granular than one cent
        public static Food[] foods =
        {
            new Food( FoodNames.pizza              , 05.77 , PersonalityNames.happy    ),
            new Food( FoodNames.sushi              , 10.00 , PersonalityNames.sad      ),
            new Food( FoodNames.burger             , 07.05 , PersonalityNames.contempt ),
            new Food( FoodNames.mcChicken          , 09.50 , PersonalityNames.contempt ),
            new Food( FoodNames.ramen              , 12.00 , PersonalityNames.contempt ),
            new Food( FoodNames.porridge           , 01.00 , PersonalityNames.contempt ),
            new Food( FoodNames.KFCvalueMeal       , 09.00 , PersonalityNames.happy    ),
            new Food( FoodNames.bobba              , 08.50 , PersonalityNames.happy    ),
            new Food( FoodNames.jerky              , 09.50 , PersonalityNames.happy    ),
            new Food( FoodNames.TVdinner           , 05.06 , PersonalityNames.sad      ),
            new Food( FoodNames.skittles           , 09.50 , PersonalityNames.happy    ),
            new Food( FoodNames.cake               , 20.00 , PersonalityNames.happy    ),
            new Food( FoodNames.peanuts            , 07.00 , PersonalityNames.happy    ),
            new Food( FoodNames.cola               , 03.50 , PersonalityNames.happy    ),
            new Food( FoodNames.watermellon        , 30.00 , PersonalityNames.happy    ),
            new Food( FoodNames.carbonara          , 22.00 , PersonalityNames.sad      ),
            new Food( FoodNames.assassinsSpaghetti , 21.00 , PersonalityNames.sad      ),
            new Food( FoodNames.pancakes           , 09.00 , PersonalityNames.happy    )
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
            string s = foodDict[f];

            if (s == null)
            {
                throw new ArgumentException(f.ToString());
            }

            return s;
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

        public static Food BuyItem(ref double wallet, Food food, ref string message)
        {
            double newWallet = wallet - food.price;

            if (newWallet >= 0)
            {
                wallet = newWallet;

                message = $"You have bought {food} for {food.price:C}.\nYour new balance is {wallet:C}.";

                return food;
            }

            message = $"You don't have the funds to buy that!";

            return null;
        }

        static char ShowStock(Program p, Store s)
        {
            Console.WriteLine($"\tYou have {p.Money()}");

            string selection = "";

            for (int i = 0; i < s.stock.Length; i++)
            {
                selection += $"\tPress {i + 1} for {s.stock[i]} for {s.stock[i].price:C}";

                if (i != s.stock.Length - 1)
                {
                    selection += "\n";
                }
            }

            Console.WriteLine(selection);

            Console.WriteLine("Press 'a' to view more stock or press 'q' to exit the store");

            return Console.ReadLine()[0];
        }

        static int Menue(Func<int, int, string> funS, Func<int, int, object> funO, int count, ref object o, string s = "")
        {
            int stockIndex = 0;

            o = null;

            while (true)
            { 
                int max = stockIndex + 9;

                if (max > count)
                {
                    max = count;
                }

                int c = 1;
                string message = "";

                for (int i = stockIndex; i < max; i++)
                {
                    message += funS(i, c) + "\n";

                    c++; // a worse language
                }

                message = message.Remove(message.Length - 1);

                Console.WriteLine(message);
                Console.WriteLine($"Press 'x' to go back or 'c' to go forward {s}, press 'q' to exit the store");

                char selection2 = Console.ReadLine()[0];

                if (selection2 == 'x')
                {
                    stockIndex -= 9;

                    if (stockIndex < 0)
                    {
                        stockIndex = 0;
                    }

                }
                else if (selection2 == 'c')
                {
                    stockIndex += 9;

                    if (stockIndex >= count)
                    {
                        stockIndex = count - 9;
                    }
                }
                else if (selection2 == 'a')
                {
                    return 0;
                }
                else if (selection2 == 'q')
                {
                    return 2;
                }
                else
                {
                    int sel = CharToNum(selection2) - 1;

                    if (sel > -1 && sel < 9)
                    {
                        o = funO(sel, stockIndex);

                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect selection");

                        return -1;
                    }
                }
            }
        }

        static Food Buy(Program p, Store s)
        {
            Console.WriteLine(s);

            char selection;

            while (true)
            {
                selection = ShowStock(p, s);

                if (selection == 'q')
                {
                    return null;
                }
                else if (selection == 'a')
                {
                    while (true)
                    {
                        Object food = null;

                        int result = Menue
                            (
                                (i, c) => $"\tPress {c} to buy {FoodNameToString(p.foods[i])} for {FoodNameToFood(p.foods[i]).price:C}",
                                (i, j) => FoodNameToFood(p.foods[j + i]),
                                p.foods.Count(),
                                ref food,
                                "or 'a' to go back to today's items"
                            );

                        if (result == 1)
                        {
                            return (Food) food;
                        }
                        else if (result == 2)
                        {
                            return null;
                        }
                        else if (result == 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            int index = CharToNum(selection) - 1;

            if (index > -1 && index < s.stock.Length)
            {
                String message = "";
                
                Food food = s.Buy(ref p.money, index, ref message);

                Console.WriteLine($"\t{message}");

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

        static Program Empty()
        {
            string m = "";
            bool g = false;

            return new Program(false, false, ref m, ref g);
        }
        
        public static void Main(string[] args)
        {
            /*
            string message = "";
            bool check = false;

            Program p = new Program(false, false, ref message, ref check);
            p.money = 100;
            p.foods = new List<FoodNames>
            {
                FoodNames.pizza,
                FoodNames.sushi,
                FoodNames.burger,
                FoodNames.mcChicken,
                FoodNames.ramen,
                FoodNames.porridge,
                FoodNames.KFCvalueMeal,
                FoodNames.bobba,
                FoodNames.jerky,
                FoodNames.TVdinner,
                FoodNames.skittles,
                FoodNames.cake,
                FoodNames.peanuts,
                FoodNames.cola,
                FoodNames.watermellon,
                FoodNames.carbonara,
                FoodNames.assassinsSpaghetti,
                FoodNames.pancakes
            };
            

            Food[] stock = { foods[0], foods[1], foods[2] };

            Console.WriteLine(Buy(p, new Store(stock)));
            */

            //Program p = Startup();

            /*
            string m = "";
            bool g = false;

            Program p = new Program(false, false, ref m, ref g);

            p.tamodachis = new List<Tamodachi> {new Tamodachi("test", 1, 1, 1, 1, 1, Egender.male, new Egender[] {})};
            p.objects = new List<string> {"test object"};
            p.foods = new List<FoodNames> {FoodNames.pizza};

            p.Save();
            p.Load();

            Console.WriteLine(p);
            */

            //p.Save();

            //Console.WriteLine(p.Load());

            Program p = Empty();

            for (int i = 0; i < 22; i++)
            {
                p.tamodachis.Add(new Tamodachi(i.ToString(), 1, 1, 1, 1, 1, Egender.male, new Egender[0]));
            }

            while (true)
            {
                Object tamodachi = null;

                int result = Menue(
                    (i, c) => $"press {c} to select {p.tamodachis[i].name}",
                    (i, j) => p.tamodachis[j + i],
                    p.tamodachis.Count(),
                    ref tamodachi
                );

                if (result == 2)
                {
                    break;
                }
                else if (result == 1)
                {
                    Console.WriteLine((Tamodachi)tamodachi);

                    break;
                }
                else if (result == 0)
                {
                    Console.WriteLine("Incorrect selection!");
                }
            }
        }
    }
}

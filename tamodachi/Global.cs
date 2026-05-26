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
            switch (s)
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

        // Dictionary used for converting foodnames to strings
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

        public static int XPinLevel = 1000;

        /// <summary>
        /// Converts foodname to string
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if string can't be converted</exception>
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
        // List of foods
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

        /// <summary>
        /// Converts foodName to food
        /// </summary>
        /// <param name="name">foodName to be converted</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if food can't be found</exception>
        public static Food FoodNameToFood(FoodNames name)
        {
            foreach (Food f in foods)
            {
                if (f.name == name)
                {
                    return f;
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// Converts foodNames to String USE THIS FUNCTION INSTEAD OF ENUMS DEFAULT TO STRING
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FoodNameToString(FoodNames f)
        {
            string s = foodDict[f];

            if (s == null)
            {
                throw new ArgumentException(f.ToString());
            }

            return s;
        }

        // These are 2 hacky solutions I came up with to get integers out of user input, normally you would use Int.Parse() but this is slightly faster

        static int CharToNum(char c)
        {
            return (int)c - 48;
        }

        static int StringToInt(string s)
        {
            if (s.Length == 2)
            {
                return CharToNum(s[1]) * -1;
            }
            else if (s.Length == 1)
            {
                return CharToNum(s[0]);
            }

            return 0;
        }

        /// <summary>
        /// Creates a tamodachi
        /// </summary>
        /// <param name="p">The program where the tamodachi will be stored</param>
        /// <param name="manditory">Flag where function will be exited if false</param>
        /// <returns>True if tamodachi is created</returns>
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

                Console.WriteLine($"Type \"y\" if {name} is a child (children cannot date)");

                string response = Console.ReadLine();
                bool child = response.Length > 0 && response[0] == 'y';

                if (!child)
                {
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

        /// <summary>
        /// Indents string
        /// </summary>
        /// <param name="n">Number of indents</param>
        /// <returns>Outputed string</returns>
        /// <exception cref="ArgumentException">If <param name="n"> < 0</exception>
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

        /// <summary>
        /// Buys an item
        /// </summary>
        /// <param name="wallet">Wallet to buy item with</param>
        /// <param name="food">Food to buy</param>
        /// <param name="message">Outputed message</param>
        /// <returns></returns>
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

        /// <summary>
        /// Shows stock in the store
        /// </summary>
        /// <param name="p">The program to get the money from</param>
        /// <param name="s">The store where the stock comes from</param>
        /// <returns>Users selection</returns>
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

        /// <summary>
        /// General purpose function that will let the user choose from an arbitrary amount of <c>{Object}s<c>"
        /// </summary>
        /// <param name="funS">String that displays for each object</param>
        /// <param name="funO">Function to get the object</param>
        /// <param name="count">Number of objects to pull from</param>
        /// <param name="o">Outputed object</param>
        /// <param name="s">Optional string that displays if user is able to fall back to another menu</param>
        /// <returns>-1 if user gives incorrect input, 0 if user falls back, 1 if object is outputed, 2 if user quits</returns>
        static int Menu(Func<int, int, string> funS, Func<int, object> funO, int count, ref object o, string s = "")
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

                if (message.Length > 0)
                {
                    message = message.Remove(message.Length - 1);
                }

                Console.WriteLine(message);
                Console.WriteLine($"Press 'x' to go back or 'c' to go forward {s}, press 'q' to exit");

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
                    int min = 9;

                    if (min > count)
                    {
                        min = count;
                    }
                    
                    if (stockIndex >= count)
                    {
                        stockIndex = count - min;
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
                        o = funO(sel + stockIndex);

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

        /// <summary>
        /// Prompts the user to but an item from the store
        /// </summary>
        /// <param name="p">The chosen program</param>
        /// <param name="s">The chosen store</param>
        /// <returns>The food bought</returns>
        static Food Buy(Program p, Store s)
        {
            bool check = false;
            
            Console.WriteLine(s);

            char selection;

            while (true)
            {
                if (check)
                {
                    Console.WriteLine($"You have {p.money:C}");
                }

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

                        int result = Menu
                            (
                                (i, c) => $"\tPress {c} to buy {FoodNameToString(p.foods[i])} for {FoodNameToFood(p.foods[i]).price:C}",
                                i => FoodNameToFood(p.foods[i]),
                                p.foods.Count(),
                                ref food,
                                "or 'a' to go back to today's items"
                            );

                        if (result == 1)
                        {
                            return (Food)food;
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

                check = true;
            }

            int index = CharToNum(selection) - 1;

            if (index > -1 && index < s.stock.Length)
            {
                String message = "";

                Food food = s.Buy(ref p.money, index, ref message);

                Console.WriteLine($"\t{message}");

                return food;
            }
            else
            {
                Console.WriteLine("Incorrect selection");

                return null;
            }
        }

        /// <summary>
        /// Starts the game, does initalisation if no save data is found
        /// </summary>
        /// <returns></returns>
        static Program Startup()
        {
            string s = "";
            bool g = false;

            Program p = new Program(false, true, ref s, ref g);

            Console.WriteLine(s);

            if (g)
            {
                Console.WriteLine($"It is {TimeOnly.FromTimeSpan(p.time.TimeOfDay)}");

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
            }

            return p;
        }

        /// <summary>
        /// Generates <param name="n"> sudo random numbers within a range of <param name="l"></param>
        /// </summary>
        /// <param name="l"></param>
        /// <param name="n"></param>
        /// <returns>An array of size <param name="n"> containing the sudo random numbers</returns>
        /// <exception cref="ArgumentException">
        /// Throws an exception if <param name="n"> > <param name="l"> or <param name="n"> <= 0 or <param name="l"> <= 0
        /// </exception>
        public static int[] Random(int l, int n)
        {
            if (n > l)
            {
                throw new ArgumentException(n.ToString());
            }
            
            if (n <= 0)
            {
                throw new ArgumentException(n.ToString());
            }

            if (l <= 0)
            {
                throw new ArgumentException(l.ToString());
            }
            
            int min = 0;
            int[] a = new int[n];
            int sect = l / n;
            Random rand = new Random();

            for (int i = 0; i < n; i ++)
            {
                int max = sect * (i + 1);
                a[i] = rand.Next(min, max);
                min = max;
            }

            return a;
        }

        /// <summary>
        /// Main loop of the program, edit this if you want new things to happen
        /// </summary>
        public static void MainLoop()
        {
            // From these 2 vars you can deduce the entire state of the program
            Program p = Startup();
            Random r = new Random();

            while (true)
            {   
                Console.WriteLine($"It is {TimeOnly.FromTimeSpan(p.time.TimeOfDay)}");

                Console.WriteLine
                (
                    "Press 1 to view tamodachis\n" +
                    "press 2 to buy food\n" +
                    "press 3 to create a new tamodachi\n" +
                    "press s to save, press x to exit"
                );

                string inputString = Console.ReadLine();
                char input = '?'; //place holder value

                if (inputString.Length > 0)
                {
                    input = inputString[0];
                }

                if (input == '3')
                {
                    if (Create(ref p, false))
                    {
                        Console.WriteLine($"Successfully created {p.tamodachis[p.tamodachis.Count() - 1].name}!");
                    }
                }
                else if (input == '2')
                {
                    if (p.todaysFoods == null)
                    {
                        int n = 3;
                        int[] a = Random(foodDict.Count, n);
                        p.todaysFoods = new FoodNames[n];

                        for (int i = 0; i < n; i ++)
                        {
                            p.todaysFoods[i] = foods[a[i]].name;
                        }
                    }

                    Food[] f = { FoodNameToFood(p.todaysFoods[0]), FoodNameToFood(p.todaysFoods[1]), FoodNameToFood(p.todaysFoods[2]) };

                    Food food = Buy(p, new Store(f));

                    if (food != null)
                    {
                        p.StoreItem(food.name);
                        bool add = true;
                        
                        foreach(FoodNames foodName in p.foods)
                        {
                            if (foodName == food.name)
                            {
                                add = false;

                                break;
                            }
                        }

                        if (add)
                        {
                            p.foods.Add(food.name);
                        }
                    }
                }
                else if (input == '1')
                {
                    Object tamodachi = null;

                    while (true)
                    {
                        int result = Menu(
                            (i, c) => $"press {c} to select {p.tamodachis[i].name}",
                            i => p.tamodachis[i],
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

                    if (tamodachi != null)
                    {
                        Tamodachi tamodachiT = (Tamodachi)tamodachi;

                        Console.WriteLine($"Press 'a' to give {tamodachiT.name} some food");

                        if (Console.ReadLine() == "a")
                        {
                            Object food = null;

                            while (true)
                            {
                                int result = Menu(
                                    (i, c) => $"press {c} to give {tamodachiT.name} some {p.inventory[i].food} (you have {p.inventory[i].amount})",
                                    i => p.inventory[i].food,
                                    p.inventory.Count(),
                                    ref food
                                );

                                if (result == 2)
                                {
                                    break;
                                }
                                else if (result == 1)
                                {
                                    FoodNames foodN = (FoodNames)food;

                                    tamodachiT.Eat(FoodNameToFood(foodN), 2, Console.WriteLine, Console.ReadLine);

                                    p.UseItem(foodN);

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
                else if (input == 's')
                {
                    p.Save();
                }
                else if (input == 'd')
                {
                    Console.WriteLine(p); // debugging
                }
                else if (input == 'x')
                {
                    Console.WriteLine("Are you sure you want to exit? unsaved progress will be lost? (press 'y' to confirm)");

                    if (Console.ReadLine() == "y")
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input!");
                }

                int rng = r.Next(2);

                if (rng == 0)
                {
                    // Conversation
                    int n = 2;
                    int[] a = Random(p.tamodachis.Count, n);

                    Tamodachi t1 = p.tamodachis[a[0]];
                    Tamodachi t2 = p.tamodachis[a[1]];
                    string o = p.objects[r.Next(p.objects.Count())];

                    Console.WriteLine(t1.Conversation(o, t2, 0));
                }
                else if (rng == 1)
                {
                    // Asks a question about an object
                    Tamodachi person = p.tamodachis[r.Next(p.tamodachis.Count())];

                    Console.WriteLine(person.Say("Hey I have to ask a question!"));
                    Console.WriteLine(person.Say("Whats an object that your really fond of?"));

                    string o = Console.ReadLine();

                    Console.WriteLine(person.Say($"Ah a {o}, I could really do with one of those!"));

                    p.objects.Add(o);
                }
            }
        }

        public static void Main(string[] args)
        {
            MainLoop();
        }      
    }
}

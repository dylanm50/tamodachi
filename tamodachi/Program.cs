using System.ComponentModel.Design;
using System.Security.AccessControl;
using static tamodachi.Global;

namespace tamodachi
{
    public class Program
    {
        //NO STATIC GLOBAL VARS, they belong in global
        public System.DateTime time { get; } = DateTime.Now;

        public System.DateTime lastTime = new DateTime(1999, 1, 1);

        public List<Tamodachi> tamodachis = new List<Tamodachi>();

        public List<string> objects = new List<string>();

        public double money = 0;

        public List<Global.FoodNames> foods = new List<Global.FoodNames>();
        
        public List<FoodItem> inventory = new List<FoodItem>();

        public FoodNames[] todaysFoods = null;

        string fileName;

        /// <summary>
        /// Resets state to inital values
        /// </summary>
         void ResetState()
         {
            lastTime = new DateTime(1999, 1, 1);

            tamodachis = new List<Tamodachi>();

            objects = new List<string>();

            money = 0;

            foods = new List<Global.FoodNames>();

            inventory = new List<FoodItem>();

            todaysFoods = null;
        }

        /// <summary>
        /// Displays current balance
        /// </summary>
        /// <returns></returns>
        public string Money()
        {
            return $"You have {money:C}";
        }

        /// <summary>
        /// Uses up a food item
        /// </summary>
        /// <param name="food">The food to be used up</param>
        /// <exception cref="ArgumentException">Thrown if no food can be found</exception>
        public void UseItem(FoodNames food)
        {
            for (int i = 0; i < inventory.Count(); i ++)
            {
                if (food == inventory[i].food)
                {
                    inventory[i].Subtract();

                    return;
                }
            }

            throw new ArgumentException(food.ToString());
        }

        /// <summary>
        /// Stores an item of food
        /// </summary>
        /// <param name="food">The food item to be added</param>
        public void StoreItem(FoodNames food)
        {
            for (int i = 0; i < inventory.Count(); i++)
            {
                if (food == inventory[i].food)
                {
                    inventory[i].Add();

                    return;
                }
            }

            inventory.Add(new FoodItem(food));
        }

        /// <summary>
        /// Saves state of game to a text file
        /// </summary>
        /// <exception cref="Exception">Thrown when saving is unsuccessful</exception>
        public void Save()
        {
            Console.WriteLine("Saving progress, don't close the program or turn off the system!");

            if (tamodachis == null || objects == null)
            {
                throw new Exception(null); // This should not happen
            }

            try
            {
                using var writer = new StreamWriter(fileName);

                writer.WriteLine(time);

                writer.WriteLine($"tamodachis");

                foreach (Tamodachi t in tamodachis)
                {
                    writer.WriteLine($"\t{t.name}");

                    writer.WriteLine("\t\tfields");

                    writer.WriteLine($"\t\t\t{t.Movement.value}");
                    writer.WriteLine($"\t\t\t{t.Speech.value}");
                    writer.WriteLine($"\t\t\t{t.Energy.value}");
                    writer.WriteLine($"\t\t\t{t.Thinking.value}");
                    writer.WriteLine($"\t\t\t{t.Normal.value}");

                    writer.WriteLine($"\t\t{t.gender}");

                    writer.WriteLine("\t\tfancies");

                    foreach (Gender g in t.fancies)
                    {
                        writer.WriteLine($"\t\t\t{g}");
                    }

                    writer.WriteLine("\t\tfoods");

                    foreach (FoodRelationship f in t.foods)
                    {
                        writer.WriteLine($"\t\t\t{Global.FoodNameToString(f.food.name)}");
                        writer.WriteLine($"\t\t\t\t{f.like}");
                    }

                    writer.WriteLine($"\t\t{t.XP}");

                    writer.WriteLine("\t\tphrases");

                    foreach (phrase phrase in t.phrases)
                    {
                        writer.WriteLine($"\t\t\t{phrase.message}");

                        writer.WriteLine($"\t\t\t\t{phrase.timing[0]}");
                        writer.WriteLine($"\t\t\t\t{phrase.timing[1]}");
                    }
                }

                writer.WriteLine("objects");

                foreach (string o in objects)
                {
                    writer.WriteLine($"\t{o}");
                }

                writer.WriteLine("money");
                writer.WriteLine($"\t{money}");

                writer.WriteLine("foods");

                foreach (FoodNames f in foods)
                {
                    writer.WriteLine($"\t{Global.FoodNameToString(f)}");
                }

                writer.WriteLine("inventory");

                foreach (FoodItem i in inventory)
                {
                    if (i.amount > 0)
                    {
                        writer.WriteLine($"\t{Global.FoodNameToString(i.food)}");
                        writer.WriteLine($"\t\t{i.amount}");
                    }
                }

                writer.WriteLine("todaysFoods");

                if (todaysFoods != null)
                {
                    foreach (FoodNames f in todaysFoods)
                    {
                        writer.WriteLine($"\t{Global.FoodNameToString(f)}");
                    }
                }

                Console.WriteLine("Saving successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: saving unsuccessfull: exception {e} raised!");
            }
        }

        /// <summary>
        /// Loads the game from a text file
        /// </summary>
        /// <returns>True if loading is successful</returns>
        public bool Load()
        {
            // store state as a backup
            DateTime tempLastTime = lastTime;
            Tamodachi[] tempTamodachis = tamodachis.ToArray();
            string[] tempObjects = objects.ToArray();
            double tempMoney = money;
            FoodNames[] tempFoods = foods.ToArray();
            FoodItem[] tempInventory = inventory.ToArray();
            FoodNames[] tempTodaysFoods = todaysFoods;
 
            ResetState();

            try
            {   
                string[] lines = File.ReadAllLines(fileName);
                int i = 0;

                while(true)
                {
                    if (i == 0)
                    {
                        lastTime = DateTime.Parse(lines[i]);

                        i ++;
                    }
                    else if (lines[i] == "tamodachis")
                    {
                        i ++;
                        
                        while(lines[i][0] == '\t')
                        {
                            string name = lines[i].Trim();

                            int movement = int.Parse(lines[i + 2].Trim());
                            int speech   = int.Parse(lines[i + 3].Trim());
                            int energy   = int.Parse(lines[i + 4].Trim());
                            int thinking = int.Parse(lines[i + 5].Trim());
                            int normal   = int.Parse(lines[i + 6].Trim());

                            Egender gender = Global.StringToEgender(lines[i + 7].Trim());

                            List<Egender> fancies = new List<Egender>();

                            string s = lines[i + 9];
                            int j = 0;

                            while (s[2] == '\t')
                            {
                                fancies.Add(Global.StringToEgender(s.Trim()));

                                j ++;

                                s = lines[i + 9 + j];
                            }

                            Egender[] fanciesArray = fancies.ToArray();

                            int h = 0;

                            int k = 0;
                            s = lines[i + 10 + j];

                            List<FoodRelationship> foodRelationships = new List<FoodRelationship>();

                            while (s.Length >= 3 && s[2] == '\t')
                            {
                                Global.FoodNames food = Global.StringToFoodName(s.Trim());

                                int like = int.Parse(lines[i + 10 + j + k + 1]);

                                foodRelationships.Add(new FoodRelationship(food, like));

                                k += 2;

                                s = lines[i + 10 + j + k];

                                if (s.Length < 3)
                                {   
                                    break;
                                }
                            }

                            i += 10 + j + k + h;

                            int xp = int.Parse(lines[i].Trim());
                            i+= 2;

                            s = lines[i];

                            List<phrase> phrases = new List<phrase>(); 

                            while (s[2] == '\t')
                            {
                                string phraseText = lines[i];
                                TimeOnly start = TimeOnly.Parse(lines[i + 1].Trim());
                                TimeOnly end = TimeOnly.Parse(lines[i + 2].Trim());

                                phrases.Add(new phrase(phraseText, new TimeOnly[] {start, end}));

                                i += 3;

                                s = lines[i];
                            }

                            phrase[] phrasesA = phrases.ToArray();

                            FoodRelationship[] foodRelationshipsArray = foodRelationships.ToArray();

                            tamodachis.Add(new Tamodachi
                                (
                                    name, movement, speech, energy, thinking, normal,
                                    gender, fanciesArray,
                                    foodRelationshipsArray,
                                    xp,
                                    phrasesA
                                )
                            );
                        }
                    }
                    else if (lines[i] == "objects")
                    {
                        string s = lines[i + 1];
                        int k = 0;

                        while (s[0] == '\t')
                        {
                            objects.Add(s.Trim());

                            k ++;

                            s = lines[i + 1 + k];
                        }

                        i += k + 1;
                    }
                    else if (lines[i] == "money")
                    {
                        money = double.Parse(lines[i + 1].Trim());

                        i += 2;
                    }else if (lines[i] == "foods")
                    {
                        string s = lines[i + 1];
                        int k = 0;

                        while (s[0] == '\t')
                        {
                            foods.Add(Global.StringToFoodName(s.Trim()));

                            k ++;

                            s = lines[i + 1 + k];
                        }

                        i += k + 1;
                    }
                    else if (lines[i] == "inventory")
                    {
                        string s = lines[i + 1];
                        int k = 0;

                        while (s[0] == '\t')
                        {
                            Global.FoodNames food = Global.StringToFoodName(s.Trim());
                            int amount = int.Parse(lines[i + 2 + k].Trim());

                            inventory.Add(new FoodItem(food, amount));

                            k += 2;

                            s = lines[i + 1 + k];
                        }

                        i += k + 1;
                    }
                    else if (lines[i] == "todaysFoods")
                    {
                        // There might be no values so we have to check for this
                        if (lines.Length - 1 < i + 1)
                        {   
                            return true;
                        }
                        
                        string s = lines[i + 1];
                        int k = 0;

                        todaysFoods = new FoodNames[] { };

                        while (true)
                        {
                            Global.FoodNames food = Global.StringToFoodName(s.Trim());
                            todaysFoods = todaysFoods.Append(food).ToArray();

                            k ++;

                            if (i + 1 + k > lines.Length - 1)
                            {   
                                return true;
                            }

                            s = lines[i + 1 + k];
                        }
                    }
                    else
                    {
                        SetState(tempLastTime, tempTamodachis, tempObjects, tempMoney, tempFoods, tempInventory, tempTodaysFoods);
                        
                        return false;
                    }
                }
            }
            catch
            {
                SetState(tempLastTime, tempTamodachis, tempObjects, tempMoney, tempFoods, tempInventory, tempTodaysFoods);

                return false;
            }
        }

        /// <summary>
        /// Sets state of the game
        /// </summary>
        /// <param name="lastTime"></param>
        /// <param name="tamodachis"></param>
        /// <param name="objects"></param>
        /// <param name="money"></param>
        /// <param name="foods"></param>
        /// <param name="inventory"></param>
        /// <param name="todaysFoods"></param>
        private void SetState
        (
            DateTime lastTime,
            Tamodachi[] tamodachis,
            string[] objects,
            double money,
            FoodNames[] foods,
            FoodItem[] inventory,
            FoodNames[] todaysFoods
        )
        {
            this.lastTime = lastTime;

            this.tamodachis = tamodachis.ToList();

            this.objects = objects.ToList();

            this.money = money;

            this.foods = foods.ToList();

            this.inventory = inventory.ToList();

            this.todaysFoods = todaysFoods;
        }

        public Program(bool testing, bool load, ref string message, ref bool newGame, string fileName = "save.txt")
        {
            this.fileName = fileName;

            if (testing)
            {
                ResetState();
                
                tamodachis.Add(
                new Tamodachi
                (
                    "Happy",
                    -4, -4, 1, -4, 1,
                    Global.Egender.male,
                    new Global.Egender[]
                    {
                    Global.Egender.female, Global.Egender.nonBinary
                    },
                    new FoodRelationship[] 
                    { 
                        new FoodRelationship(Global.FoodNames.pizza, 10),
                        new FoodRelationship(Global.FoodNames.sushi, 5)
                    },
                    750,
                    new phrase[] 
                    {
                        new phrase("hello", new TimeOnly[] {new TimeOnly(0, 0), new TimeOnly(23, 59)}),
                        new phrase("i love turtles", new TimeOnly[] {new TimeOnly(0, 0), new TimeOnly(23, 59)}),
                    }
                ));

                tamodachis.Add(
                new Tamodachi
                (
                    "Sad",
                    -4, -4, -4, 1, -4,
                    Global.Egender.female,
                    new Global.Egender[]
                    {
                    Global.Egender.female,
                    Global.Egender.nonBinary, Global.Egender.male
                    }
                ));

                tamodachis.Add(
                new Tamodachi
                (
                    "Indiferent",
                    4, 4, 4, 4, 4,
                    Global.Egender.female,
                    new Global.Egender[]
                    {
                    Global.Egender.female,
                    Global.Egender.nonBinary, Global.Egender.male
                    }
                ));

                objects = new List<string>() { "ball", "gameboy", "chocolate" };
                money = 100;
                foods = new List<Global.FoodNames>() { Global.FoodNames.sushi, Global.FoodNames.mcChicken, Global.FoodNames.burger };

                FoodItem sushi = new FoodItem(Global.FoodNames.sushi);
                FoodItem mc = new FoodItem(Global.FoodNames.mcChicken);

                for (int i = 0; i < 100; i ++)
                {
                    sushi.Add();

                    if (i % 2 == 0)
                    {
                        mc.Add();
                    }
                }

                inventory = new List<FoodItem>() { sushi, mc };

                Save();
            }else if (load)
            {
                message = "loading save!\n";
                
                if (Load())
                {
                    if (lastTime.Date != time.Date)
                    {
                        todaysFoods = null;

                        int totalXP = 0;

                        foreach (Tamodachi t in tamodachis)
                        {
                            totalXP += t.XP;
                        }

                        int totalLevels = totalXP / Global.XPinLevel;

                        money += Math.Log(totalLevels) * 87; //https://www.desmos.com/calculator/pwaysjki6p
                    }
                    
                    message += "loaded save";
                    newGame = false;
                }else
                {
                    message += "failed to find save, starting new game";
                    newGame = true;
                }
            }
        }

        public override string ToString()
        {
            string s = $"{lastTime}\n";
            s += "\tTamodachis\n";
            
            foreach(Tamodachi t in tamodachis)
            {  
                s += $"\t\t{t.name}\n";

                s += "\t\t\tfields:\n";
                s += $"\t\t\t\t{t.Movement.value}\n";
                s += $"\t\t\t\t{t.Speech.value}\n";
                s += $"\t\t\t\t{t.Energy.value}\n";
                s += $"\t\t\t\t{t.Thinking.value}\n";
                s += $"\t\t\t\t{t.Normal.value}\n";

                s += $"\t\t\tgender: {t.gender}\n";

                s += "\t\t\tlikes:\n";

                foreach (Gender g in t.fancies)
                {
                    s += $"\t\t\t\t{g}\n";
                }

                s += "\t\t\tfoods:\n";

                foreach (FoodRelationship r in t.foods)
                {
                    s += $"\t\t\t\t{r.food}\n";
                    s += $"\t\t\t\t\t{r.like}\n";
                }

                s += $"\t\t\tXP:{t.XP}\n";

                s += "\t\t\tphrases:\n";

                foreach (phrase phrase in t.phrases)
                {
                    s += $"\t\t\t\t{phrase.message}\n";

                    s += $"\t\t\t\t\tstart:{phrase.timing[0]}\n";
                    s += $"\t\t\t\t\tend:{phrase.timing[1]}\n";
                }
            }

            s += "\tObjects\n";

            foreach(string o in objects)
            {
                s += $"\t\t{o}\n";
            }

            s += $"\tmoney: {money:C}\n";

            s += "\tfoods:\n";

            foreach (FoodNames f in foods)
            {
                s += $"\t\t{Global.FoodNameToString(f)}\n";
            }

            s += "\tinventory:\n";

            foreach (FoodItem i in inventory)
            {
                s += $"\t\t{i.food}\n";
                s += $"\t\t\t{i.amount}\n";
            }

            s += "\ttodaysFoods:\n";

            if (todaysFoods != null)
            {
                foreach (Global.FoodNames f in todaysFoods)
                {
                    s += $"\t\t{Global.FoodNameToString(f)}\n";
                }
            }

            return s;
        }
    }
}
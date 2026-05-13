using System.ComponentModel.Design;
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

        public static string mTime(System.TimeOnly time)
        {
            return time.ToString("HH:mm");
        }

        public string Money()
        {
            return $"You have {money:C}";
        }

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

        public void Save()
        {
            Console.WriteLine("Saving progress, don't close the program or turn off the system!");

            if (tamodachis == null || objects == null)
            {
                throw new Exception(null); // This should not happen
            }

            try
            {
                using var writer = new StreamWriter("save.txt");

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

        public bool Load()
        {
            try
            {
                //setting everthing to a blank slate 
                tamodachis = new List<Tamodachi>();
                objects = new List<string>();
                foods = new List<FoodNames>();
                inventory = new List<FoodItem>();
                money = 0;
                
                string[] lines = File.ReadAllLines("save.txt");
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

                            if (fancies.Count() == 0)
                            {
                                h = -2;
                            }

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
                                    k += 0;
                                    
                                    break;
                                }
                            }

                            if (foodRelationships.Count() == 0 && fancies.Count() == 0)
                            {
                                h += 2;
                            }
                            
                            i += 10 + j + k + h;

                            FoodRelationship[] foodRelationshipsArray = foodRelationships.ToArray();

                            tamodachis.Add(new Tamodachi
                                (
                                    name, movement, speech, energy, thinking, normal,
                                    gender, fanciesArray,
                                    foodRelationshipsArray
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
                        ResetState();
                        
                        return false;
                    }
                }
            }
            catch
            {
                ResetState();
                
                return false;
            }
        }

        public Program(bool testing, bool load, ref string message, ref bool newGame)
        {
            if (testing)
            {   
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
                        todaysFoods = new FoodNames[3];
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
            string s = $"{time}\n";
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
            }

            s += "\tObjects\n";

            foreach(string o in objects)
            {
                s += $"\t\t{o}\n";
            }

            s += $"\tmoney:{money:C}\n";

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

            foreach (Global.FoodNames f in todaysFoods)
            {
                s += $"\t\t{Global.FoodNameToString(f)}\n";
            }

            return s;
        }
    }
}
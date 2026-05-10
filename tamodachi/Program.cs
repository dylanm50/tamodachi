using static tamodachi.Global;

namespace tamodachi
{
    public class Program
    {
        //NO STATIC GLOBAL VARS, they belong in global
        public System.TimeOnly time { get; } = TimeOnly.FromDateTime(DateTime.Now);

        public List<Tamodachi> tamodachis = new List<Tamodachi>();

        public List<string> objects = new List<string>();

        public double money = 0;

        public List<Global.FoodNames> foods = new List<Global.FoodNames>();
        
        public List<FoodItem> inventory = new List<FoodItem>();

        public static string mTime(System.TimeOnly time)
        {
            return time.ToString("HH:mm");
        }

        public string Money()
        {
            return $"You have {money:C}";
        }

        public void Save()
        {
            Console.WriteLine("Saving progress, don't close the program or turn off the system!");

            try
            {
                using var writer = new StreamWriter("save.txt");

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
                    writer.WriteLine($"\t{Global.FoodNameToString(i.food)}");
                    writer.WriteLine($"\t\t{i.amount}");
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
                string[] lines = File.ReadAllLines("save.txt");
                int i = 0;

                while(true)
                {
                    if (lines[i] == "tamodachis")
                    {
                        while(lines[i + 1][0] == '\t')
                        {
                            string name = lines[i + 1].Trim();

                            int movement = int.Parse(lines[i + 3].Trim());
                            int speech   = int.Parse(lines[i + 4].Trim());
                            int energy   = int.Parse(lines[i + 5].Trim());
                            int thinking = int.Parse(lines[i + 6].Trim());
                            int normal   = int.Parse(lines[i + 7].Trim());

                            Egender gender = Global.StringToEgender(lines[i + 8].Trim());

                            List<Egender> fancies = new List<Egender>();

                            string s = lines[i + 10];
                            int j = 0;

                            while (s[2] == '\t')
                            {
                                fancies.Add(Global.StringToEgender(s.Trim()));

                                j ++;

                                s = lines[i + 10 + j];
                            }

                            Egender[] fanciesArray = fancies.ToArray();

                            int k = 0;
                            s = lines[i + 12 + j + k];

                            List<FoodRelationship> foodRelationships = new List<FoodRelationship>();

                            while (s[2] == '\t')
                            {
                                Global.FoodNames food = Global.StringToFoodName(s.Trim());
                                int like = int.Parse(lines[i + 12 + j + k + 1]);

                                foodRelationships.Add(new FoodRelationship(food, like));

                                k += 2;

                                s = lines[i + 12 + j + k];
                            }

                            i += 10 + j + k;

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
                    else if (lines[i += 1] == "objects")
                    {
                        string s = lines[i + 1];
                        int k = 0;

                        while (s[0] == '\t')
                        {
                            objects.Add(s.Trim());

                            k ++;

                            s = lines[i + 1 + k];
                        }

                        i += k;
                    }else if (lines[i] == "money")
                    {
                        money = int.Parse(lines[i + 1].Trim());

                        i ++;
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

                        i += k;
                    }
                    else if (lines[i] == "inventory")
                    {
                        string s = lines[i + 1];
                        int k = 0;

                        while (true)
                        {
                            Global.FoodNames food = Global.StringToFoodName(s.Trim());
                            int amount = int.Parse(lines[i + 2 + k].Trim());

                            inventory.Add(new FoodItem(food, amount));

                            k += 2;

                            if (i + 1 + k >= lines.Length)
                            {
                                return true; // read the entire file :)
                            }

                            s = lines[i + 1 + k];
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }catch
            {
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
                    message += "loaded save";
                    newGame = false;
                }else
                {
                    message += "failed to find save, starting new game";
                    newGame = true;
                }
            }
        }
    }
}
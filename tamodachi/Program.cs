namespace tamodachi
{
    public class Program
    {
        //NO STATIC GLOBAL VARS, they belong in global
        public System.TimeOnly time { get; } = TimeOnly.FromDateTime(DateTime.Now);

        public List<Tamodachi> tamodachis = new List<Tamodachi>();

        public List<string> objects = new List<string>();

        public static string mTime(System.TimeOnly time)
        {
            return time.ToString("HH:mm");
        }

        public Program(bool testing)
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
            }
        }
    }
}
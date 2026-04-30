namespace tamodachi
{
    public class Program
    {
        //NO STATIC GLOBAL VARS, they belong in global
        public System.TimeOnly time { get; } = TimeOnly.FromDateTime(DateTime.Now);

        public List<Tamodachi> tamodachis = new List<Tamodachi>();
        
        public static string mTime(System.TimeOnly time)
        {
            return time.ToString("HH:mm");
        }

        public void Init()
        {
            tamodachis.Add(
            new Tamodachi
            (
                "Person A",
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
                "Person B",
                -4, -4, 1, -4, 1,
                Global.Egender.female,
                new Global.Egender[]
                {
                    Global.Egender.female,
                    Global.Egender.nonBinary, Global.Egender.male
                }
            ));
        }
    }
}
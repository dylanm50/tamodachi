namespace tamodachi
{
    public class Program
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

        // I LOVE ARRAYS IN C# (JAVA SUCKS ASS)
        public static Personality[] Personalities =
        {
            new Personality
            (
                PersonalityNames.happy,

                new int[] { -4, 4 }, //m
                new int[] { -4, 4 }, //s
                new int[] {  1, 4 }, //e
                new int[] { -4,-1 }, //t
                new int[] {  1, 4 }, //n

                new string[] 
                {
                    "I love everything",
                    "I have so much energy",
                    "I look forward to tomorrow"
                }
            ),

            new Personality
            (
                PersonalityNames.sad,

                new int[] { -4 ,-1}, //m
                new int[] { -4 , 4}, //s
                new int[] { -4 ,-1}, //e
                new int[] {  1 , 4}, //t
                new int[] { -4 ,-1}, //n

                new string[]
                {
                    "I hate the world",
                    "Im tired",
                    "I just need to get through today"
                }
            ),

            new Personality
            (
                PersonalityNames.contempt,

                new int[] { -4 , 4}, //m
                new int[] { -4 , 4}, //s
                new int[] { -4 , 4}, //e
                new int[] { -4 , 4}, //t
                new int[] { -4 , 4}, //n

                new string[]
                {
                    "Meh",
                    "I guess I can do that",
                    "Tommorow will come i guess"
                }
            )
        };
        
        public static void Main(string[] args)
        {
            Tamodachi personA = new Tamodachi
            (
                "Person A",
                -4, -4, 1, -4, 1,
                Egender.male, 
                new Egender[] 
                {
                    Egender.female, Egender.nonBinary
                }
            );

            Tamodachi personB = new Tamodachi
            (
                "Person B",
                -4, -4, 1, -4, 1,
                Egender.female,
                new Egender[] 
                { 
                    Egender.female,
                    Egender.nonBinary, Egender.male 
                }
            );

            Console.WriteLine("Created Some Guys!");
            Console.WriteLine(personA);
            Console.WriteLine(personB);
            Console.WriteLine(personA.MatchToString(personB));
            Console.WriteLine(personA.Talk());
            Console.WriteLine(personB.Talk());
        }
    }
}
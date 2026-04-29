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

        public static System.TimeOnly time { get; } = TimeOnly.FromDateTime(DateTime.Now);

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

                new phrase[] 
                {
                    new phrase
                    (
                        "I love everything",
                        new System.TimeOnly[] {new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I have so much energy",
                        new System.TimeOnly[] {new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I look forward to tomorrow",
                        new System.TimeOnly[] {new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
                    )
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

                new phrase[]
                {
                    new phrase
                    (
                        "I hate the world",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "Im tired",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I just need to get through today",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59)}
                    )
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

                new phrase[]
                {
                    new phrase
                    (
                        "Meh",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I guess I can do that",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "Tommorow will come i guess",
                        new System.TimeOnly[] {new System.TimeOnly(0, 0), new System.TimeOnly(23, 59) }
                    )
                }
            )
        };
        
        static string mTime(System.TimeOnly time)
        {
            return time.ToString("HH:mm");
        }

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

            Console.WriteLine($"The current time is {time}");

            Console.WriteLine("Created Some Guys!");
            Console.WriteLine(personA);
            Console.WriteLine(personB);
            Console.WriteLine(personA.MatchToString(personB));
            Console.WriteLine(personA.Talk("ummm idk what to say XD"));
            Console.WriteLine(personB.Talk("imm be a STAR"));
        }
    }
}
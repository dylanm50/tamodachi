using System;
using System.Collections.Generic;
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
                        new System.TimeOnly[] { new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I have so much energy",
                        new System.TimeOnly[] { new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
                    ),
                    new phrase
                    (
                        "I look forward to tomorrow",
                        new System.TimeOnly[] { new System.TimeOnly(19, 0), new System.TimeOnly(23, 59) }
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

        public static void Main(string[] args)
        {
            Console.WriteLine("Loading ...");

            Program p = new Program();

            p.Init();

            Tamodachi personA = p.tamodachis[0];
            Tamodachi personB = p.tamodachis[1];

            Console.WriteLine("Finished loading!");

            Console.WriteLine($"The current time is {p.time}");

            Console.WriteLine(personA);
            Console.WriteLine(personB);
            Console.WriteLine(personA.MatchToString(personB));
            Console.WriteLine(personA.Talk("ummm idk what to say XD", p.time));
            Console.WriteLine(personB.Talk("imm be a STAR", p.time));
            
            /*
            int l = 100000;

            Console.WriteLine($"Initalising {l} programs");

            Program[] programs = new Program[l];

            for (int i = 0; i < l; i ++)
            {
                programs[i] = new Program();
                programs[i].Init();
            }

            Console.WriteLine("Finished!");
            */
        }
    }
}

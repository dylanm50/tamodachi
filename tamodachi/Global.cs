using System;
using System.Collections.Generic;
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

        public static void Main(string[] args)
        {
            
            Console.WriteLine("Running a C# tamodachi game container.");
            Console.WriteLine("\tInstance 1:");

            Console.WriteLine("\t\tLoading ...");

            Program p = new Program();

            p.Init();

            Tamodachi happy    = p.tamodachis[0];
            Tamodachi sad      = p.tamodachis[1];
            Tamodachi contempt = p.tamodachis[2];

            Console.WriteLine("\t\tFinished loading!");

            Console.WriteLine($"\t\tThe current time is {p.time}");

            Console.WriteLine($"\t\t{happy}");
            Console.WriteLine($"\t\t{sad}");
            Console.WriteLine($"\t\t{happy.MatchToString(sad)}");
            Console.WriteLine($"\t\t{happy.Talk("ummm idk what to say XD", p.time)}");
            Console.WriteLine($"\t\t{happy.Talk("imm be a STAR", p.time)}");

            string o = p.objects[new Random().Next(p.objects.Count() - 1)];

            Console.WriteLine(contempt.Conversation(o, sad, 2));

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

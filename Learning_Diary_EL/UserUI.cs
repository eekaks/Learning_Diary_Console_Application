using System;

namespace Learning_Diary_EL
{
    class UserUI
    {
        public static void PrintProgramBanner()
        {
            Console.WriteLine(@" __         _______     ___      .______     .__   __.  __  .__   __.   _______    _______    __       ___      .______     ____    ____");
            Console.WriteLine(@"|  |      |   ____ |   /   \     |   _  \    |  \ |  | |  | |  \ |  |  /  _____|   |       \ |  |     /   \     |   _  \    \   \  /   /");
            Console.WriteLine(@"|  |      |  | __     /  ^  \    |  | _) |   |   \|  | |  | |   \|  | |  |  __     |  .--.  ||  |    /  ^  \    |  | _) |    \   \/   /");
            Console.WriteLine(@"|  |      |   __ |   /  /_\  \   |      /    |  . `  | |  | |  . `  | |  | | _|    |  |  |  ||  |   /  /_\  \   |      /      \_    _/");
            Console.WriteLine(@"|  `----. |  | ____ /  _____  \  |  |\  \----|  |\   | |  | |  |\   | |  |__| |    |  '--'  ||  |  /  _____  \  |  |\  \----.   |  |");
            Console.WriteLine(@"| _______|| _______/__/     \__\ | _| `._____|__| \__| |__| |__| \__|  \______|    |_______/ |__| /__/     \__\ | _| `._____|   |__|"); 
        }

        public static void PrintBanner(string title)
        {
            int emptiesLeft = (28 - title.Length) / 2;
            int emptiesRight = (28 - title.Length) / 2;
            if (title.Length % 2 != 0)
            {
                emptiesRight += 1;
            }
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("*" + new string(' ', emptiesLeft) + title + new string(' ', emptiesRight) + "*");
            Console.WriteLine(new string('*', 30));
            Console.WriteLine();
        }

        public static int GetInt(string prompt, string error)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    int input = int.Parse(Console.ReadLine());
                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine(error);
                }
            }
        }
        public static double GetDouble(string prompt, string error)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    double input = double.Parse(Console.ReadLine());
                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine(error);
                }
            }
        }
        public static DateTime GetDateTime(string prompt, string error)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    DateTime input = DateTime.Parse(Console.ReadLine());
                    return input;
                }
                catch (Exception e)
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}
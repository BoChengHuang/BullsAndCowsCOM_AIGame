using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    class Program
    {
        public static ArrayList list = new ArrayList();
        public static int count = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Mode 1: COM guess.");
            Console.WriteLine("Mode 2: Debug 1000 times.");
            Console.Write("Which do you want: ");
            string mode = Console.ReadLine();

            if (mode == "1") {
                ComputerGuess();
            } else if (mode == "2") {
                DebugMode();
            }

            Console.ReadKey();
        }

        public static void DebugMode()
        {

            list = GenerateList();
            ArrayList random = new ArrayList();

            for (int i = 0; i < 1000; i++) {
                random.Add(GenerateNumberFromList());
            }

            float sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                count = 0;
                list = GenerateList();
                while (!GameForDebug((int)random[i])) ;
                sum += count;
            }
            Console.WriteLine("Avg Times: " + sum / 1000);

        }

        public static void ComputerGuess() {

            list = GenerateList();

            while (!Game()) ;
        }

        public static ArrayList GenerateList()
        {
            ArrayList list = new ArrayList();

            for (int i = 123; i <= 9876; i++) {
                int d1 = i / 1000;
                int d2 = (i / 100) % 10;
                int d3 = (i / 10) % 10;
                int d4 = i % 10;
               
                if (d1 != d2 && d1 != d3 && d1 != d4 && d2 != d3 && d2 != d4 && d3 != d4) {
                    list.Add(i);
                }
            }

            return list;
        }

        public static bool Game()
        {
            int guess = GenerateNumberFromList();
            float rate;
            count++;
            string msg = guess.ToString();
            if (msg.Count() == 3) {
                msg = '0' + msg;
            }
            rate = ((float)list.Count / 5040) * 100;
            //rate = list.Count;
            Console.WriteLine("Round " + count + ": " + msg +  ", give me hints. ( Win Rate: " + (100 - rate) + "% )");
            Console.Write("How many bulls: ");
            string bulls = Console.ReadLine();
            Console.Write("How many cows: ");
            string cows = Console.ReadLine();
            int bull = (int)char.GetNumericValue(bulls.ToCharArray()[0]);
            int cow = (int)char.GetNumericValue(cows.ToCharArray()[0]);

            if (bull < 0 || bull > 4 || cow < 0 || cow > 4)
            {
                Console.WriteLine("Digit must be 1~4.");
                return false;
            }
            else if (bull == 4 && cow == 0){
                Console.WriteLine("Comupter Win! Your secret number is " + guess);
                return true;
            }

            CheckList(bull, cow, guess);

            if (list.Count == 1)
            {
                Console.WriteLine("Round " + count + ": " + list[0] + ", give me hints. ( Win Rate: " + (100 - rate) + "% )");
                Console.Write("How many bulls: ");
                int a = (int)char.GetNumericValue(Console.ReadLine().ToCharArray()[0]);
                Console.Write("How many cows: ");
                int b = (int)char.GetNumericValue(Console.ReadLine().ToCharArray()[0]);
                if (a != 4 || b != 0)
                {
                    Console.WriteLine("You must type something wrong, try again...");
                    //Console.WriteLine(list[0]);
                }
                else if (a == 4 && b == 0)
                {
                    Console.WriteLine("Comupter Win! Your secret number is " + list[0]);
                    return true;
                }
                return true;
            } else if (list.Count == 0){
                Console.WriteLine("You must type something wrong, try again...");
                return true;
            }
            else {
                return false;
            }
        }

        public static int GenerateNumberFromList() {
            int number = 0;
            //Random rnd = new Random();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int index = rnd.Next(1, list.Count);
            number = (int)list[index];
            return number;
        }

        public static void CheckList(int bull, int cow, int guess) {
            ArrayList newList = new ArrayList();

            for (int i = 0; i < list.Count; i++) {

                ArrayList ab = new ArrayList();
                ab = GetAB(guess, (int)list[i]);

                if (bull == (int)ab[0] && cow == (int)ab[1]) {
                    newList.Add(list[i]);
                }

            }

            list = newList;
        }

        public static bool GameForDebug(int ans)
        {
            int guess = GenerateNumberFromList();
            count++;

            ArrayList ab = new ArrayList();
            ab = GetAB(guess, ans);

            int bull = (int)ab[0];
            int cow = (int)ab[1];

            if (bull < 0 || bull > 4 || cow < 0 || cow > 4)
            {
                return true;
            }
            else if (bull == 4 && cow == 0){
                Console.WriteLine("Comupter Win! Your secret number is " + guess);
                return true;
            }

            CheckList(bull, cow, guess);

            if (list.Count == 1)
            {
                return true;
            }
            else if (list.Count == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public static ArrayList GetAB(int guess, int ans) {
            ArrayList ab = new ArrayList();
            int a = 0;
            int b = 0;
            int[] listArr = new int[4];
            int[] guessArr = new int[4];
            listArr[0] = ans / 1000;
            listArr[1] = (ans / 100) % 10;
            listArr[2] = (ans / 10) % 10;
            listArr[3] = ans % 10;

            guessArr[0] = guess / 1000;
            guessArr[1] = (guess / 100) % 10;
            guessArr[2] = (guess / 10) % 10;
            guessArr[3] = guess % 10;

            for (int i = 0; i < 4; i++) {
                if (guessArr[i] == listArr[i])
                {
                    a++;
                }
                else {
                    for (int j = 0; j < 4; j++)
                    {
                        if (guessArr[j] == listArr[i]) {
                            b++;
                        }
                    }
                }
            }

            ab.Add(a);
            ab.Add(b);

            return ab;
        }

    }
}

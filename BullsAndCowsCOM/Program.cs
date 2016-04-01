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

        public static ArrayList userList = new ArrayList();
        public static int userCount = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Mode 1: COM guess.");
            Console.WriteLine("Mode 2: Debug.");
            Console.WriteLine("Mode 3: COM vs You.");
            Console.Write("Which do you want: ");
            string mode = Console.ReadLine();

            if (mode == "1") {
                ComputerGuess();
            } else if (mode == "2") {
                DebugMode();
            } else if (mode == "3") {
                Battle();
            }

            Console.ReadKey();
        }

        public static void DebugMode()
        {

            list = GenerateList();
            ArrayList random = new ArrayList();
            int[] timeCount = Enumerable.Repeat(0, 10).ToArray();

            Console.Write("How many times do you want to run: ");
            int times = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < times; i++) {
                random.Add(GenerateNumberFromList());
            }

            float sum = 0;
            for (int i = 0; i < times; i++)
            {
                count = 0;
                list = GenerateList();
                while (!GameForDebug((int)random[i])) ;
                sum += count;
                timeCount[count]++;
            }
            Console.WriteLine("Avg Turns: " + sum / times + " Tunr(s).");
            for (int i = 1; i < timeCount.Count(); i++) {
                Console.WriteLine("Guees for " + i + " turn(s): " + timeCount[i]);
            }

        }

        public static void Battle() {
            Console.WriteLine("Guess mutual numbers, as soon as posiible.");
            Console.WriteLine("");
            Console.WriteLine("Human first...");

            list = GenerateList();
            userList = GenerateList();
            bool userWin = false;
            bool comWin = false;
            int ansint = GenerateNumberFromList();

            while (!userWin && !comWin) {

                //Console.WriteLine(ansint);
                int[] ans = new int[4];

                ans[0] = ansint / 1000;
                ans[1] = (ansint / 100) % 10;
                ans[2] = (ansint / 10) % 10;
                ans[3] = ansint % 10;

                userWin = UserGame(ans);
                if (userWin)
                {
                    break;
                }
                comWin = Game();
                if (comWin)
                {
                    break;
                }

                double comWinRate = Math.Round(100 - ((double)list.Count / 5040) * 100);
                double userWinRate = Math.Round(100 - ((double)userList.Count / 5040) * 100);
                
                Console.WriteLine("COM win rate: " + (double)(comWinRate / (comWinRate + userWinRate)) * 100 + "%, Your win rate: " + (double)(userWinRate / (comWinRate + userWinRate)) * 100 + "%...");
            }
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
            Console.WriteLine("");
            Console.WriteLine("Round " + count + ": " + msg +  ", give me hints. ( Guess Rate: " + (100 - rate) + "% )");
            Console.Write("How many bulls: ");
            int bull = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many cows: ");
            int cow = Convert.ToInt32(Console.ReadLine());


            if (bull < 0 || bull > 4 || cow < 0 || cow > 4)
            {
                Console.WriteLine("Digit must be 1~4.");
                count--;
                Game();
            }
            else if (bull == 4 && cow == 0){
                Console.WriteLine("Comupter Win! Your secret number is " + msg);
                return true;
            }

            CheckList(bull, cow, guess, true);

            if (list.Count == 1)
            {
                Console.WriteLine("");
                Console.WriteLine("Corfirm: " + count + ": " + list[0] + ", give me comfirm. ( Guess Rate: " + (100 - rate) + "% )");
                Console.Write("How many bulls: ");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.Write("How many cows: ");
                int b = Convert.ToInt32(Console.ReadLine());
                if (a != 4 || b != 0)
                {
                    Console.WriteLine("You must type something wrong, try again...");
                    //Console.WriteLine(list[0]);
                }
                else if (a == 4 && b == 0)
                {
                    Console.WriteLine("Your secret number is " + list[0]);
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

        public static bool UserGame(int[] ans)
        {
            Console.WriteLine("");
            Console.WriteLine("Guess a four digit number");
            string guess = Console.ReadLine();

            char[] guessed = guess.ToCharArray();
            int bullsCount = 0;
            int cowsCount = 0;

            if (guessed.Length != 4 || (guessed[0] == guessed[1] || guessed[0] == guessed[2] || guessed[0] == guessed[3]) || guessed[1] == guessed[2] || guessed[1] == guessed[3] || guessed[2] == guessed[3])
            {
                Console.WriteLine("Not a valid guess. Try again");
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                int curguess = (int)char.GetNumericValue(guessed[i]);
                if (curguess < 0 || curguess > 9)
                {
                    Console.WriteLine("Digit must be ge greater -1 and lower 10.");
                    return false;
                }

                if (curguess == ans[i])
                {
                    bullsCount++;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (curguess == ans[j])
                            cowsCount++;
                    }
                }
            }

            CheckList(bullsCount, cowsCount, Convert.ToInt32(guess), false);

            if (bullsCount == 4)
            {
                Console.WriteLine("Congratulations! You have won!");
                return true;
            }
            else
            {
                Console.WriteLine("{0} bulls and {1} cows", bullsCount, cowsCount);
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

        public static void CheckList(int bull, int cow, int guess, bool isCOM) {
            ArrayList newList = new ArrayList();

            if (isCOM)
            {
                for (int i = 0; i < list.Count; i++)
                {

                    ArrayList ab = new ArrayList();
                    ab = GetAB(guess, (int)list[i]);

                    if (bull == (int)ab[0] && cow == (int)ab[1])
                    {
                        newList.Add(list[i]);
                    }

                }

                list = newList;
            }
            else {
                for (int i = 0; i < userList.Count; i++)
                {

                    ArrayList ab = new ArrayList();
                    ab = GetAB(guess, (int)userList[i]);

                    if (bull == (int)ab[0] && cow == (int)ab[1])
                    {
                        newList.Add(userList[i]);
                    }

                }

                userList = newList;
            }

            
        }

        public static bool GameForDebug(int ans)
        {
            int guess = GenerateNumberFromList();
            count++;

            ArrayList ab = new ArrayList();
            ab = GetAB(guess, ans);

            string msg = guess.ToString();
            if (msg.Count() == 3)
            {
                msg = '0' + msg;
            }

            int bull = (int)ab[0];
            int cow = (int)ab[1];

            if (bull < 0 || bull > 4 || cow < 0 || cow > 4)
            {
                return true;
            }
            else if (bull == 4 && cow == 0){
                Console.WriteLine("Comupter Win! Your secret number is " + msg);
                return true;
            }

            CheckList(bull, cow, guess, true);

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

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
            list = GenerateList();

            while (!game()) ;

            Console.ReadKey();
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

            /* Print list
            for (int j = 0; j < list.Count; j++) {
                Console.WriteLine(list[j]);
            }*/

            Console.WriteLine("Length: " + list.Count);
            return list;
        }

        public static void KnuthShuffle<T>(ref T[] array)
        {
            System.Random random = new System.Random();
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(array.Length);
                T temp = array[i]; array[i] = array[j]; array[j] = temp;
            }
        }

        public static bool game()
        {
            int guess = generateNumberFromList();
            count++;
            string msg = guess.ToString();
            if (msg.Count() == 3) {
                msg = '0' + msg;
            }
            Console.WriteLine("Round " + count + ": " + msg +  ", How many bulls and cows?");
            string bulls = Console.ReadLine();
            string cows = Console.ReadLine();
            int bull = (int)char.GetNumericValue(bulls.ToCharArray()[0]);
            int cow = (int)char.GetNumericValue(cows.ToCharArray()[0]);

            if (bull < 0 || bull > 4 || cow <0 || cow > 4)
            {
               Console.WriteLine("Digit must be 1~4.");
               return false;
            }

            checkList(bull, cow, guess);

            if (list.Count == 1)
            {
                Console.WriteLine("Round " + count + ": " + list[0] + ", How many bulls and cows?");
                int a = (int)char.GetNumericValue(Console.ReadLine().ToCharArray()[0]);
                int b = (int)char.GetNumericValue(Console.ReadLine().ToCharArray()[0]);
                if (a != 4 || b != 0) {
                    Console.WriteLine("You must type something wrong, try again...");
                }
                Console.WriteLine("Comupter Win! Your secret number is " + list[0]);
                return true;
            } else if (list.Count == 0){
                Console.WriteLine("You must type something wrong, try again...");
                return true;
            }
            else {
                return false;
            }
        }

        public static int generateNumberFromList() {
            int number = 0;
            Random rnd = new Random();
            int index = rnd.Next(1, list.Count);
            number = (int)list[index];
            return number;
        }

        public static void checkList(int bull, int cow, int guess) {
            ArrayList newList = new ArrayList();

            for (int i = 0; i < list.Count; i++) {
                int[] listArr = new int[4];
                int[] guessArr = new int[4];
                int bullsCount = 0;
                int cowsCount = 0;
                listArr[0] = (int)list[i] / 1000;
                listArr[1] = ((int)list[i] / 100) % 10;
                listArr[2] = ((int)list[i] / 10) % 10;
                listArr[3] = (int)list[i] % 10;        

                guessArr[0] = guess / 1000;
                guessArr[1] = (guess / 100) % 10;
                guessArr[2] = (guess / 10) % 10;
                guessArr[3] = guess % 10;

                for (int j = 0; j < 4; j++) {

                    if (listArr[j] == guessArr[j])
                    {
                        bullsCount++;
                    }
                    else
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (listArr[j] == guessArr[k])
                                cowsCount++;
                        }
                    }
                }

                if (bull == bullsCount && cow == cowsCount) {
                    newList.Add(list[i]);
                }

            }

            list = newList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Sweepstakes
{
    static class UserInterface
    {





        public static string GetinputClear(string output)
        {
            Console.Clear();
            Console.WriteLine(output);
            return Console.ReadLine();
        }
        public static string GetInputInline(string output)
        {
            Console.WriteLine(output);
            return Console.ReadLine();
        }
        public static void DisplayOnly(string output)
        {
            Console.Clear();
            Console.WriteLine(output);
        }
        public static void DisplayInline(string output)
        {
            Console.WriteLine(output);
        }
        public static void Clear()
        {
            Console.Clear();
        }
        public static bool EmailCheck(string email)
        {
            bool dot = true;
            int dotseqence=1;
            int at = 0;
            int space = 0;
            bool output = false;
            foreach (char item in email)
            {
                if (item == '.')
                {
                    dotseqence++;
                    if (dotseqence<1)
                    {
                        dot = false;
                    }
                    
                }
                else if (item == '@')
                {
                    dotseqence++;
                    at++;
                }
                else if (item== ' ')
                {
                    space++;
                }
                else
                {
                    dotseqence = 0;
                }
            }
            if (at==1 &&space==0&&!dot)
            {
                output = true;
            }
            return output;
        }
        public static string GetEmail(string output)
        {
            Console.WriteLine(output);
            string email = Console.ReadLine();
            output = "remember, an email can not have spaces, multiple @ symbols, or a sequence of dots ex:'john..doe @' Please try again";
            do
            {
                Console.WriteLine(output);
                output = "invalid email, please try again";
                email = Console.ReadLine();
            } while (!EmailCheck(email));
            return email;           
        }

    }
}

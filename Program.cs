using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace DLTP_Phase1_AdressBook2
{
    class Program
    {
        List<Person> Book = new List<Person>();
        static void Main(string[] args)
        {
            Menu();
        }
        private static void Menu()
        {
            Console.WriteLine("**** Pick an option ****");
            Console.WriteLine("1: Show contacts");
            Console.WriteLine("2: Add contact");
            Console.WriteLine("3: Remove contact");
            Console.WriteLine("4: Save to file");
            Console.WriteLine("End program by typing 'quit'");
            Console.Write("> ");
            Console.ReadLine();
        }
        class Person
        {
            public string name, adress, phonenumber, email;
            public Person(string N, string A, string P, string E)
            {
                name = N;
                adress = A;
                phonenumber = P;
                email = E;
            }
        }
    }
}

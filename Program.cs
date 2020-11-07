using System;
using System.IO;
using System.Collections.Generic;
using System.Data;

namespace DLTP_Phase1_AdressBook2
{
    class Program
    {
        private static List<Person> Book = new List<Person>(); // keep list outside methods, so every method can call for it
        static void Main(string[] args)
        {
            string filePath = ReadFile();
            bool quit = false;
            Menu();
            ReadFile();
            do
            {
                Console.ReadLine();
            }
            while (!quit);
        }
        public static string ReadFile()
        {
            string filePath = @"C:\adressbook\contacts.txt";
            string[] fileText = File.ReadAllLines(filePath);
            //Read file and saves already written information into list
            foreach (string row in fileText)
            {
                string[] split = row.Split(';');
                Person N = new Person(split[0], split[1], split[2], split[3]);
                Book.Add(N);
            }
            return filePath;
        }
        public static void Menu()
        {
            Console.WriteLine("**** Pick an option ****");
            Console.WriteLine("1: Show contacts");
            Console.WriteLine("2: Add contact");
            Console.WriteLine("3: Remove contact");
            Console.WriteLine("4: Save to file");
            Console.WriteLine("End program by typing 'quit'");
            Console.Write("> ");
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

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
                string userInput = Console.ReadLine();
                if (userInput == "quit")
                {
                    quit = true;
                }
                UserPick(filePath, userInput);
            }
            while (!quit);
            Console.WriteLine("Goodbye!");
        }
        public static void UserPick(string filePath, string userInput)
        {
            string input = userInput;
            switch (input)
            {
                case "1":
                    Console.Clear();
                    if (new FileInfo(filePath).Length == 0)
                    {
                        Console.WriteLine("No contacts found!\n");
                        Menu();
                    }
                    else
                    {
                        PrintFile(filePath);
                        Menu();
                    }
                    break;
                case "2":
                    AddContact();
                    break;
            }
        }
        public static void AddContact()
        {
            Console.Clear();
            Console.Write("Enter first & lastname: ");
            string newName = Console.ReadLine();
            Console.Write("Enter adress: ");
            string newAdress = Console.ReadLine();
            Console.Write("Enter phone: ");
            string newPhone = Console.ReadLine();
            Console.Write("Enter email: ");
            string newEmail = Console.ReadLine();
            Person newContact = new Person(newName, newAdress, newPhone, newEmail);
            Book.Add(newContact);
            //Print out
            Console.WriteLine($"{newName}, {newAdress}, {newPhone}, {newEmail} was added!");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(!) IMPORTANT: Remember to save to file (4) before ending program (!)\n");
            Console.ResetColor();
            Menu();
        }

        public static void PrintFile(string filePath)
        {
            string[] fileText = File.ReadAllLines(filePath);
            Console.WriteLine("Contacts");
            foreach (string row in fileText)
            {
                string[] split = row.Split(';');
                Console.Write($"Name: {split[0]}\nAdress: {split[1]}\nPhone: {split[2]}\nEmai: {split[3]}\n");
                Console.WriteLine();
            }
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

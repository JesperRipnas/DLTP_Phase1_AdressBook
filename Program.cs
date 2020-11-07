using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;

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
                    Console.Clear();
                    AddContact(filePath);
                    Menu();
                    break;
                case "3":
                    Console.Clear();
                    if (new FileInfo(filePath).Length == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("No contacts found!\n");
                        Menu();
                    }
                    else
                    {
                        PrintFile(filePath);
                        RemoveContact(filePath);
                    }
                    break;
                case "4":
                    break;
            }
        }
        public static void SaveToFile(string filePath)
        {
            // Clear file before writing
            // Source: https://stackoverflow.com/questions/14071475/remove-all-previous-text-before-writing
            File.WriteAllText(filePath, string.Empty);
            // Write to file
            // Source: https://www.geeksforgeeks.org/how-to-read-and-write-a-text-file-in-c-sharp/
            foreach (Person i in Book)
            {
                File.AppendAllText(filePath, $"{i.name};{i.adress};{i.phonenumber};{i.email}\n");
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Saved to file! {filePath}\n");
            Console.ResetColor();
        }
        public static void AddContact(string filePath)
        {
            Console.Write("(1/4) Enter first & lastname: ");
            string newName = Console.ReadLine();
            Console.Write("(2/4) Enter adress: ");
            string newAdress = Console.ReadLine();
            Console.Write("(3/4) Enter phone: ");
            string newPhone = Console.ReadLine();
            Console.Write("(4/4) Enter email: ");
            string newEmail = Console.ReadLine();
            // Doublecheck
            Console.WriteLine("Are you sure you want to add the contact? (y/n)");
            string input = Console.ReadLine().ToLower();
            if (input == "y")
            {
                Person newContact = new Person(newName, newAdress, newPhone, newEmail);
                Book.Add(newContact);
                SaveToFile(filePath);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No changes were made\n");
            }
        }
        public static void RemoveContact(string filePath)
        {
            Console.Write("Enter full name of contact you want to remove: ");
            string removeContact = Console.ReadLine();
            // Source: https://www.c-sharpcorner.com/UploadFile/mahesh/remove-items-from-a-C-Sharp-list/
            for (int i = 0; i < Book.Count; i++)
            {
                if (removeContact == Book[i].name)
                {
                    // Doublecheck
                    Console.WriteLine("Are you sure you want to remove the contact? (y/n)");
                    string input = Console.ReadLine().ToLower();
                    if (input == "y")
                    {
                        Book.RemoveAt(i);
                        SaveToFile(filePath);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No changes were made\n");
                        break;
                    }
                }
                else if(removeContact != Book[i].name)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("(!) Incorrect name!\n");
                    Console.ResetColor();
                    RemoveContact(filePath);
                    break;
                }
            }
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
            Console.WriteLine("4: Modify contact");
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

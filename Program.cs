using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace DLTP_Phase1_AdressBook2
{
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
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        //Check if file contains data or not. Source https://stackoverflow.com/questions/3007711/is-file-empty-check
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
                        if (new FileInfo(filePath).Length == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("No contacts found!\n");
                            Menu();
                        }
                        else
                        {
                            SearchContact();
                        }
                        break;
                    case "3":
                        Console.Clear();
                        AddContact(filePath);
                        Menu();
                        break;
                    case "4":
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
                    case "5":
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
                            ModifyContact(filePath);
                        }
                        break;
                    case "quit":
                        break;
                    default:
                        Console.WriteLine("Incorrect input, please try again!");
                        break;
                }
            }
            while (!quit);
            Console.WriteLine("Goodbye!");
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
            Console.WriteLine($"Saved to file!\n");
            System.Threading.Thread.Sleep(1000);
            Console.ResetColor();
            Console.Clear();
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
            Console.Write("Are you sure you want to add the contact? (y/n): ");
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
                    Console.Write("Are you sure you want to remove the contact? (y/n): ");
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
            }
            Menu();
        }
        public static void ModifyContact(string filePath)
        {
            Console.Write("Enter full name of contact you want to change: ");
            string modifyContact = Console.ReadLine();
            for (int i = 0; i < Book.Count; i++)
            {
                if (modifyContact == Book[i].name)
                {
                    Console.Write("Change name, adress, phone or email?: ");
                    string changeInput = Console.ReadLine().ToLower();

                    switch (changeInput)
                    {
                        case "name":
                            Console.Write($"Change {Book[i].name} to: ");
                            string newName = Console.ReadLine();
                            Book[i].name = newName;
                            SaveToFile(filePath);
                            Menu();
                            break;
                        case "adress":
                            Console.Write($"Change {Book[i].adress} to: ");
                            string newAdress = Console.ReadLine();
                            Book[i].adress = newAdress;
                            SaveToFile(filePath);
                            Menu();
                            break;
                        case "phone":
                            Console.Write($"Change {Book[i].phonenumber} to: ");
                            string newPhone = Console.ReadLine();
                            Book[i].phonenumber = newPhone;
                            SaveToFile(filePath);
                            Menu();
                            break;
                        case "email":
                            Console.Write($"Change {Book[i].email} to: ");
                            string newEmail = Console.ReadLine();
                            Book[i].email = newEmail;
                            SaveToFile(filePath);
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Incorrect input, please try again!");
                            break;
                    }
                }
            }
        }
        public static void SearchContact()
        {
            int counter = 0;
            Console.Write("Search for: ");
            string searchInput = Console.ReadLine();
            for (int i = 0; i < Book.Count; i++)
            {
                if (Book[i].name.Contains(searchInput) || Book[i].adress.Contains(searchInput) || Book[i].phonenumber.Contains(searchInput) || Book[i].email.Contains(searchInput))
                {
                    Console.WriteLine($"\nName: {Book[i].name}\nAdress: {Book[i].adress}\nPhone: {Book[i].phonenumber}\nEmail: {Book[i].email}");
                    counter++;
                }
            }
            Console.WriteLine($"\nFound ({counter}) results\n");
            Console.WriteLine("****************************");
            Menu();
        }
        public static void PrintFile(string filePath)
        {
            string[] fileText = File.ReadAllLines(filePath);
            Console.WriteLine("   Contacts in adressbook");
            Console.WriteLine("****************************");
            foreach (string row in fileText)
            {
                string[] split = row.Split(';');
                Console.Write($"\nName: {split[0]}\nAdress: {split[1]}\nPhone: {split[2]}\nEmail: {split[3]}\n");
            }
            Console.WriteLine();
        }
        public static string ReadFile()
        {
            try
            {
                // Read the contacts.txt in project folder instead of a hardwired path (if someone would clone project)
                //source https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project/31526925
                //source https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up
                string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\data\contacts.txt");
                string[] fileText = File.ReadAllLines(filePath);
                foreach (string row in fileText)
                {
                    string[] split = row.Split(';');
                    Person N = new Person(split[0], split[1], split[2], split[3]);
                    Book.Add(N);
                }
                return filePath;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Could not load file: contacts.txt");
                Console.WriteLine("Press any key to quit");
                Console.ResetColor();
                Console.ReadLine();
                Environment.Exit(0);
                return "";
            }
        }
        public static void Menu()
        {
            Console.WriteLine("End program by typing 'quit'");
            Console.WriteLine("****** Pick an option ******\n");
            Console.WriteLine("1: Show all");
            Console.WriteLine("2: Search");
            Console.WriteLine("3: Add");
            Console.WriteLine("4: Remove");
            Console.WriteLine("5: Modify\n");
            Console.Write("> ");
        }
    }
}

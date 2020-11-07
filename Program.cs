using System;
using System.Collections.Generic;

namespace DLTP_Phase1_AdressBook2
{
    class Program
    {
        List<Person> Book = new List<Person>();
        static void Main(string[] args)
        {
            
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

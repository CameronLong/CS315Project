using System;
using System.Collections;


public class Program
{
    public static void createContact(Hashtable phonebook){
        Console.WriteLine("\n\n");
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        string phoneNumber;
        bool test = true;
        while(true){
            Console.WriteLine("Enter Phone Number (use no separators, only numbers): ");
            phoneNumber = Console.ReadLine();
            if(phoneNumber.Length==10){
                foreach(char data in phoneNumber){
                    if(!char.IsDigit(data)){
                        test=false;
                    }
                }
                if(test == true){
                    break;
                }
            }
            Console.WriteLine("There is an error with the phone number. Make sure it is 10 numbers, no other characters\n\n");
        }
        phonebook.Add(name, phoneNumber);
        
    }
    
    public static void showContacts(Hashtable phonebook){
        Console.WriteLine("\n\n");
        foreach (DictionaryEntry entry in phonebook)
        {
            Console.WriteLine($"Name: {entry.Key} | Phone Number: {entry.Value}");
        }
    }
    
    public static void Main()
    {
        // Creating a hashtable
        Hashtable phonebook = new Hashtable();
        
        while(true){
            Console.WriteLine("\n\n");
            Console.WriteLine("1) Create Contact:");
            Console.WriteLine("2) Delete Contact:");
            Console.WriteLine("3) Show Contacts:");
            Console.WriteLine("4) Exit:");
            Console.WriteLine("Make a selection:");
            string input = Console.ReadLine();
            
            if(input=="4"){
                break;
            }
            
            switch(input){
                case "1": createContact(phonebook); break;
                case "2": break;
                case "3": showContacts(phonebook); break;
                default: Console.WriteLine("Invalid Selection"); break;
            }
        }
        
    }
}

using System;
using System.Collections;


public class Program
{
    
    public static string numberInput(Hashtable phonebook){
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
        return phoneNumber;
    }
    
    public static void callNumber(string numberToCall, Hashtable phonebook){
        int matchesfound = 0;
        foreach (DictionaryEntry entry in phonebook)
        {
            //Console.WriteLine($"{entry.Value}");
            //Console.WriteLine($"{numberToCall}");
            if (entry.Value.ToString() == numberToCall){
                matchesfound++;
                for (int i = 0; i<4; i++){
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("\nRinging...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("...");
                }
                System.Threading.Thread.Sleep(1000);
                Random rnd = new Random();
                int rng = rnd.Next(1,10);
                if (rng == 1){
                    Console.WriteLine("\n\n****** LINE CONNECTED! ******");
                    Console.WriteLine("\n\n...Hello?");
                    System.Threading.Thread.Sleep(200);
                    Console.WriteLine("\n\n...Hello?");
                    System.Threading.Thread.Sleep(250);
                    Console.WriteLine("\n\nI'm going back to bed...");
                    System.Threading.Thread.Sleep(400);
                    Console.WriteLine("\n\n**** LINE DISCONNECTED! ****");
                }
                else{
                    Console.WriteLine("\n\n Sorry, your call could not be completed...");
                    Console.WriteLine("Please try again soon!");
                }
                break;
            }
        }
        if (matchesfound == 0){
            Console.WriteLine("\n\nThat number is not recognized!");
        }
    }
    
    public static void createContact(Hashtable phonebook){
        Console.WriteLine("\n\n");
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        string inputtedNumber = numberInput(phonebook);
        int duplicate = 0;
        foreach (DictionaryEntry entry in phonebook)
        {
            if (entry.Value.ToString() == inputtedNumber)
            {
                duplicate = 1;
                Console.WriteLine("That number is already assigned!");
            }
        }
        if (duplicate != 1)
        {
            phonebook.Add(name, inputtedNumber);
        }
    }
    
    public static void showContacts(Hashtable phonebook){
        Console.WriteLine("\n\n");
        foreach (DictionaryEntry entry in phonebook)
        {
            Console.WriteLine($"Name: {entry.Key} | Phone Number: {entry.Value}");
        }
    }
    
    
    public static void usePhone(Hashtable phonebook){
        Console.WriteLine("\n\n");
        callNumber(numberInput(phonebook),phonebook);
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
            Console.WriteLine("4) Use Phone:");
            Console.WriteLine("5) Exit:");
            Console.WriteLine("Make a selection:");
            string input = Console.ReadLine();
            
            if(input=="5"){
                break;
            }
            
            switch(input){
                case "1": createContact(phonebook); break;
                case "2": break;
                case "3": showContacts(phonebook); break;
                case "4": usePhone(phonebook); break;
                default: Console.WriteLine("Invalid Selection"); break;
            }
        }
        
    }
}

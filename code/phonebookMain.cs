using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

unsafe public struct TableItem {
    public int key;
    public string name;
    public string phoneNumber;
};

unsafe public class HashTable {
    public TableItem* items;
    public int size;
    public int count;

    public HashTable() {
        items = (TableItem*)Marshal.AllocHGlobal(sizeof(TableItem*) * 10);
        for(int i = 0; i < 10; i++){
            items[i].name = null;
            items[i].key = 0;
            Console.WriteLine("item key: " + items[i].key);
            // items[i].phoneNumber = null;
        }
    }
    public void addTableElement(string name, string inputtedNumber){
        unsafe {
            TableItem* itemPtr = (TableItem*)Marshal.AllocHGlobal(sizeof(TableItem));
            TableItem newItem = new TableItem();
            int tries = 0;
            newItem.key = hash_function(name.Substring(0,1).ToCharArray()[0], tries);
            Console.WriteLine("new key: " + newItem.key);
            newItem.name = name;
            newItem.phoneNumber = inputtedNumber;
            *itemPtr = newItem;

            bool inserted = false;
            for(int i = 0; i < 10; i++){
                while(!inserted){
                    if (itemPtr->key == 0) {
                        items[itemPtr->key] = *itemPtr;
                        inserted = true;
                    } else {
                        while (String.Equals(items[itemPtr->key].key, itemPtr->key)) {
                            // Handle Collision
                            tries++;
                            itemPtr->key = hash_function(name[i], tries);
                        }
                        // Do not handle the Collision
                        items[itemPtr->key] = *itemPtr;
                        inserted = true;
                    }
                }
            }
        }
    }

    public static int hash_function(char newKey, int tries) {
        int i = 0;
        i += newKey;
        int tempKey = (i % 10) + tries;

        while (tempKey > 10) {
            tempKey -= 10;
        }
        return tempKey;
    }
        public int hash_function(char newKey) {
        int i = 0;
        i += newKey;
        int tempKey = (i % 10);

        while (tempKey > 10) {
            tempKey -= 10;
        }
        return tempKey;
    }
};

public class Program {
    
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
        foreach (DictionaryEntry entry in phonebook) {
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
    
    unsafe public static void createContact(Hashtable phonebook, HashTable *newHashTable){
        Console.WriteLine("\n\n");
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        string inputtedNumber = numberInput(phonebook);
        int duplicate = 0;


        // foreach (DictionaryEntry entry in phonebook) {
        //     if (entry.Value.ToString() == inputtedNumber) {
        //         duplicate = 1;
        //         Console.WriteLine("That number is already assigned!");
        //     }
        // }
        for(int i = 0; i < 10; i++){
            if(newHashTable->items[i].phoneNumber == inputtedNumber){
                duplicate = 1;
                Console.WriteLine("That number is already assigned!");
            }
        }
        if (duplicate != 1) {
            // phonebook.Add(name, inputtedNumber);
            // Attempt to implement Cameron's Hash Table
            newHashTable->addTableElement(name, inputtedNumber);
        }
    }

    unsafe public static void deleteContact(HashTable *newHashTable){
        Console.WriteLine("\n\n");
        Console.WriteLine("Enter a name to delete from the phonebook: ");
        string deleteName = Console.ReadLine();
        int deleteKey = newHashTable->hash_function(deleteName.Substring(0,1).ToCharArray()[0]);
        // newHashTable[deleteKey].items->name = null;
        for(int i = 0; i < 10; i++){
            Console.WriteLine("Item key: " + newHashTable->items[i].key);
            if(newHashTable->items[i].key == deleteKey){
                newHashTable->items[i].name = null;
                newHashTable->items[i].phoneNumber = null;
                newHashTable->items[i].key = 0;
            }
        }

    }
    
    unsafe public static void showContacts(Hashtable phonebook, HashTable *newPhonebook){
        Console.WriteLine("\n\n");
        // foreach (DictionaryEntry entry in phonebook)
        // {
        //     Console.WriteLine($"Name: {entry.Key} | Phone Number: {entry.Value}");
        // }

        unsafe{
            System.Console.WriteLine("------------Phone Book-----------------");

            for (int i = 0; i < 10; i++) {
                if(newPhonebook->items[i].name != null){
                    System.Console.WriteLine("Name: " + newPhonebook->items[i].name + " | Phone Number: " + newPhonebook->items[i].phoneNumber);
                }
            }

            System.Console.WriteLine("---------------------------------------");
            newPhonebook = null;
        }
    }
    
    
    public static void usePhone(Hashtable phonebook){
        Console.WriteLine("\n\n");
        callNumber(numberInput(phonebook),phonebook);
    }
    
    unsafe public static void Main()
    {
        // Creating a hashtable
        Hashtable phonebook = new Hashtable();
        unsafe {
            HashTable phonebookTable = new HashTable();
        
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
                    case "1": createContact(phonebook, &phonebookTable); break;
                    case "2": deleteContact(&phonebookTable); break;
                    case "3": showContacts(phonebook, &phonebookTable); break;
                    case "4": usePhone(phonebook); break;
                    default: Console.WriteLine("Invalid Selection"); break;
                }
            }
            phonebookTable = null;
            Console.WriteLine("Hello World");
        }
        GC.Collect();
    }
}

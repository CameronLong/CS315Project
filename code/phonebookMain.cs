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
        }
    }
    unsafe public void addTableElement(string name, string inputtedNumber){
        TableItem* itemPtr = (TableItem*)Marshal.AllocHGlobal(sizeof(TableItem));
        TableItem newItem = new TableItem();
        int tries = 0;
        newItem.key = hash_function(name.Substring(0,1).ToCharArray()[0], tries);
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
    
    unsafe public static string numberInput(HashTable *phonebook){
        string phoneNumber;
        bool test = true;
        while(true){
            Console.Write("Enter Phone Number (use no separators, only numbers): ");
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
    
    unsafe public static void callNumber(string numberToCall, HashTable *phonebook){
        int matchesfound = 0;
        for (int j = 0; j < 10; j++) {
            if (phonebook->items[j].phoneNumber == numberToCall){
                matchesfound++;
                for (int i = 0; i<3; i++){
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
                int rng = rnd.Next(1,3);
                if (rng == 1 || rng == 3){
                    Console.WriteLine("\n\n****** LINE CONNECTED! ******");
                    Console.WriteLine("\n...Hello?");
                    System.Threading.Thread.Sleep(200);
                    Console.WriteLine("\n...Hello?");
                    System.Threading.Thread.Sleep(250);
                    Console.WriteLine("\nI'm going back to bed...");
                    System.Threading.Thread.Sleep(400);
                    Console.WriteLine("\n**** LINE DISCONNECTED! ****");
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
    
    unsafe public static void createContact(HashTable *newHashTable){
        Console.WriteLine("\n");
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        string inputtedNumber = numberInput(newHashTable);
        int duplicate = 0;

        for(int i = 0; i < 10; i++){
            if(newHashTable->items[i].phoneNumber == inputtedNumber){
                duplicate = 1;
                Console.WriteLine("That number is already assigned!");
            }
        }
        if (duplicate != 1) {
            // Attempt to implement Cameron's Hash Table
            newHashTable->addTableElement(name, inputtedNumber);
        }
    }

    unsafe public static void deleteContact(HashTable *newHashTable){
        Console.WriteLine("\n");
        Console.Write("Enter Name: ");
        string deleteName = Console.ReadLine();
        int deleteKey = newHashTable->hash_function(deleteName.Substring(0,1).ToCharArray()[0]);
        for(int i = 0; i < 10; i++){
            if(newHashTable->items[i].key == deleteKey){
                newHashTable->items[i].name = null;
                newHashTable->items[i].phoneNumber = null;
                newHashTable->items[i].key = 0;
            }
        }

    }
    
    unsafe public static void showContacts(HashTable *newPhonebook){
        Console.WriteLine("\n");
        System.Console.WriteLine("---------------Contacts-----------------");

        for (int i = 0; i < 10; i++) {
            if(newPhonebook->items[i].name != null){
                System.Console.WriteLine("Name: " + newPhonebook->items[i].name + " | Phone Number: " + newPhonebook->items[i].phoneNumber);
            }
        }

        System.Console.WriteLine("----------------------------------------");
        newPhonebook = null;
    }
    
    
    unsafe public static void usePhone(HashTable *phonebook){
        Console.WriteLine("\n\n");
        callNumber(numberInput(phonebook),phonebook);
    }
    
    unsafe public static void Main() {
        // Creating a hashtable
        HashTable phonebookTable = new HashTable();
        
        while(true){
            Console.WriteLine("\n-------Menu-------");
            Console.WriteLine("1) Create Contact:");
            Console.WriteLine("2) Delete Contact:");
            Console.WriteLine("3) Show Contacts:");
            Console.WriteLine("4) Use Phone:");
            Console.WriteLine("5) Exit:");
            Console.Write("Make a selection: ");
            string input = Console.ReadLine();
                
            if(input=="5"){
                break;
            }
                
            switch(input){
                case "1": createContact(&phonebookTable); break;
                case "2": deleteContact(&phonebookTable); break;
                case "3": showContacts(&phonebookTable); break;
                case "4": usePhone(&phonebookTable); break;
                default: Console.WriteLine("Invalid Selection"); break;
            }
        }
        phonebookTable = null;
        GC.Collect();
    }
}

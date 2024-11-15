using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;

public class Program
{
    public class HashTable
    {
        public TableItem[] items;
        public int size = 11;
        public int count;

        public HashTable()
        {
            items = new TableItem[size];
            for (int i = 0; i < size; i++)
            {
                items[i].name = null;
                items[i].key = 0;
                items[i].phoneNumber = null;
            }


        }

        public void addTableElement(string name, string inputtedNumber, int type)
        {
            TableItem newItem = new TableItem();
            int num = Int32.Parse(inputtedNumber.Substring(6));
            int tries = 0;
            newItem.key = hash_function(num, tries, type);
            newItem.name = name;
            newItem.phoneNumber = inputtedNumber;

            bool inserted = false;
            while (!inserted && tries < size)
            {
                int key = newItem.key;
                if (items[key].name == null)
                {
                    items[key] = newItem;
                    inserted = true;
                }
                else
                {
                    tries++;
                    newItem.key = hash_function(num, tries, type);
                }
            }

        }

        public int hash_function(int newKey, int tries, int type)
        {
            if (type == 1)
            {
                int tKey = (newKey % 10 + tries) % 10;
                return tKey;
            }
            else if (type == 2)
            {
                int temKey = (newKey % 10 + (tries * tries)) %10;
                return temKey;
            }
            int tempKey = (int)((newKey/(Math.Pow(10, tries)))) % 10;
            return tempKey;

        }

    }

    public struct TableItem
    {
        public string name;
        public string phoneNumber;
        public int key;
    }

    public static string numberInput()
    {
        Console.Write("Enter Phone Number: ");
        return Console.ReadLine();
    }

    public static void createContact(ref HashTable newHashTable, int type)
    {
        Console.WriteLine("Enter Name: ");
        string name = Console.ReadLine();
        string inputtedNumber = numberInput();


        for (int i = 0; i < newHashTable.size; i++)
        {
            if (inputtedNumber.Length == 10)
            {
                if (newHashTable.items[i].phoneNumber == inputtedNumber || !Int32.TryParse(inputtedNumber.Substring(0,5), out int yuh)|| !Int32.TryParse(inputtedNumber.Substring(5), out int yuh2))
                {
                    Console.WriteLine("Error, make sure input is a valid phone number!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Must be 10 numbers!");
                return;
            }
        }
        newHashTable.addTableElement(name, inputtedNumber, type);
    }


    public static void deleteContact(ref HashTable newHashTable, int type)
    {
        Console.WriteLine("Enter a name to delete from the phonebook: ");
        string deleteName = Console.ReadLine();

        int tries = 0;
        bool contactFound = false;

        while (tries < newHashTable.size)
        {
            for (int i = 0; i < newHashTable.size; i++)
            {
                if (newHashTable.items[i].name != null && newHashTable.items[i].name == deleteName)
                {
                    newHashTable.items[i].name = null;
                    newHashTable.items[i].phoneNumber = null;
                    newHashTable.items[i].key = 0;
                    contactFound = true;
                    Console.WriteLine("Contact deleted.");
                    break;
                }
            }

            if (!contactFound)
            {
                Console.WriteLine("Contact not found.");
                break;  
            }

            if (contactFound == true)
            {
                break;
            }
        }
    }

    public static void showContacts(ref HashTable newPhonebook)
    {
        Console.WriteLine("------------Phone Book-----------------");
        for (int i = 0; i < newPhonebook.size; i++)
        {
            if (newPhonebook.items[i].phoneNumber == null)
            {
                Console.WriteLine("Empty");
            }
            if (newPhonebook.items[i].name != null)
            {
                Console.WriteLine("Name: " + newPhonebook.items[i].name + " | Phone Number: " + newPhonebook.items[i].phoneNumber);
            }
        }
        Console.WriteLine("---------------------------------------");
    }

    public static void callNumber(string numberToCall, ref HashTable newPhonebook)
    {
        for (int i = 0; i < newPhonebook.size; i++)
        {
            if (newPhonebook.items[i].phoneNumber == numberToCall)
            {
                Console.WriteLine("Calling " + newPhonebook.items[i].name);
                return;
            }
        }
        Console.WriteLine("Number not found in contacts.");
    }

    public static void usePhone(ref HashTable phonebook)
    {
        string numberToCall = numberInput();
        callNumber(numberToCall, ref phonebook);
    }

    public static void Main()
    {
        int inputInt = 0;

        while (true)
        {
            Console.WriteLine("\n\n1) Linear Probing");
            Console.WriteLine("2) Quadratic Probing");
            Console.WriteLine("3) Double-Hashing");
            Console.Write("Make a selection: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    inputInt = 1;
                    break;
                case "2":
                    inputInt = 2;
                    break;
                case "3":
                    inputInt = 3;
                    break;

                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

            if (inputInt == 1 || inputInt == 2 || inputInt == 3)
            {
                break;
            }
        }

        HashTable phonebookTable = new HashTable();

        while (true)
        {
            Console.WriteLine("\n\n1) Create Contact");
            Console.WriteLine("2) Delete Contact");
            Console.WriteLine("3) Show Contacts");
            Console.WriteLine("4) Call Number");
            Console.WriteLine("5) Exit");
            Console.Write("Make a selection: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    createContact(ref phonebookTable, inputInt);
                    break;
                case "2":
                    deleteContact(ref phonebookTable, inputInt);
                    break;
                case "3":
                    showContacts(ref phonebookTable);
                    break;
                case "4":
                    usePhone(ref phonebookTable);
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }
}

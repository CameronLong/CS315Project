﻿using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

public class Program
{
    public class HashTable
    {
        public TableItem[] items;
        public int size = 10;
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
                int tKey = (newKey % size + tries) % size;
                return tKey;
            }
            else if (type == 2)
            {
                int temKey = (newKey % size + (tries * tries)) % size;
                return temKey;
            }
            int tempKey = (newKey % size + (tries * (newKey % size))) % size;
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
        int deleteKey = newHashTable.hash_function(deleteName[0], 0, type);

        if (newHashTable.items[deleteKey].name == deleteName)
        {
            newHashTable.items[deleteKey].name = null;
            newHashTable.items[deleteKey].phoneNumber = null;
            newHashTable.items[deleteKey].key = 0;
            Console.WriteLine("Contact deleted.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
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

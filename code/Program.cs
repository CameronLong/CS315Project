// using System;
// using System.Runtime.InteropServices;
// using System.Globalization;
// using System.Runtime.InteropServices.Marshalling;
// using System.Runtime.InteropServices;
// unsafe public struct TableItem
// {
//     public int key;
//     public string name;
//     public int quantity;
// };

// unsafe public class HashTable
// {
//     public TableItem* items;
//     public int size;
//     public int count;

//     public HashTable(string[] ingredients) {
//         size = 9;
//         count = 0;
//         items = (TableItem*)Marshal.AllocHGlobal(sizeof(TableItem*) * 5);

//         TableItem* itemPtr = (TableItem*)Marshal.AllocHGlobal(sizeof(TableItem));
//         int tries = 0;
//         Random rnd = new Random();
//         for (int i = 0; i <= 10; i++) {
//             TableItem newItem = new TableItem();
//             // items[i] = null;
//             newItem.key = hash_function(ingredients[i].Substring(0, 1).ToCharArray()[0], tries);
//             newItem.name = ingredients[i];
//             newItem.quantity = rnd.Next(1, 10);
//             *itemPtr = newItem;
//             // items[newItem.key] = *itemPtr;
//             // System.Console.WriteLine("Key=" + newItem.key);
//             if (itemPtr->key == 0) {
//                 items[itemPtr->key] = *itemPtr;
//             } else {
//                 while (String.Equals(items[itemPtr->key].key, itemPtr->key)) {
//                     // Handle Collision
//                     System.Console.WriteLine("New Item: " + itemPtr->name + " Key=" + itemPtr->key);
//                     // System.Console.WriteLine("A collision ocurred, current item: " + items[newItem.key].key);
//                     tries++;
//                     itemPtr->key = hash_function(ingredients[i].Substring(0, 1).ToCharArray()[0], tries);
//                     // System.Console.WriteLine("New key=" + itemPtr->key + "\n");
//                 }
//                 // Do not handle the Collision
//                 // System.Console.WriteLine("A collision has not ocurred");
//                 items[itemPtr->key] = *itemPtr;
//                 // System.Console.WriteLine(items[newItem.key].name + "\n");
//             }
//         }

//     }
//     public static int hash_function(char newKey, int tries) {
//         int i = 0;
//         i += newKey;
//         int tempKey = (i % 10) + tries;

//         while (tempKey > 10) {
//             tempKey -= 10;
//         }
//         return tempKey;
//     }
// };


// public class Program {
//     public static void Main(string[] args) {
//         // Declare array of ingredients
//         string[] ingredients = new string[] { "Bread", "Milk", "Flour", "Baking Soda", "Chocolate Chips", "Sugar", "Jam", "Butter", "Cheese", "Baking Powder", "Vanilla" };

//         // Muse use unsafe to use pointers in c#
//         unsafe {
//             // Create a table item
//             HashTable? itemPointers = new HashTable(ingredients);

//             // Print the table
//             System.Console.WriteLine("------------Hash table--------------");

//             for (int i = 0; i < 11; i++) {
//                 System.Console.WriteLine("Item: " + itemPointers.items[i].name + ", Quantity: " + itemPointers.items[i].quantity);
//             }

//             System.Console.WriteLine("------------------------------------");
//             itemPointers = null;
//         }
//     }
// }

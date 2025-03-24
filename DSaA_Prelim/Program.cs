using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DSaA_Prelim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] tower = new int[3][];
            int diskCount = 0;
            int sourceTower = 0;
            int sourceDisk = 0;
            int sourceIndex = 0;
            int targetTower = 0;
            int targetDisk = 0;
            int moveCount = 0;
            bool userDisk = false;
            bool mainGame = true;
            bool inputAuth = false;

            do // This is the Starting Disks Count Input Checker.
            {
                Console.Write("How many disks?: ");
                if (int.TryParse(Console.ReadLine(), out diskCount))
                {
                    if (diskCount > 10)
                        Console.WriteLine("Disks should be 10 AT MOST");

                    else if (diskCount < 3)
                        Console.WriteLine("Disks should be 3 AT LEAST");

                    else
                        userDisk = true;
                }
                else
                    Console.WriteLine("Invalid Input");
            } while (!userDisk);

            // This initializes the Starting elements or contents of the Tower.
            for (int i = 0; i < tower.Length; i++)
            {
                tower[i] = new int[diskCount+2];
                for (int j = 0; j < tower[i].Length; j++)
                {
                    if (i == 0)
                    {
                        if (j < diskCount)
                            tower[i][(tower[i].Length - 1) - j] = diskCount - j;
                        else
                            tower[i][(tower[i].Length - 1) - j] = 0;
                    }
                    else
                    {
                        tower[i][j] = 0;
                    }
                }
            }

            // This is the Printer Loop for elements of the Tower.
            for (int i = 0; i < (diskCount + 2); i++) 
            {
                for (int j = 0; j < tower.Length; j++) 
                {
                    if (tower[j][i] != 0)
                        Console.Write($"[{tower[j][i]}]\t");
                    else
                        Console.Write(" |\t");
                }
                Console.WriteLine();
                if (i+1 == (diskCount + 2))
                    Console.WriteLine($"-0-\t-1-\t-2-");
            }

            while (mainGame)
            {
                Console.Clear();
                Console.WriteLine($"Move Counter: {moveCount}");
                for (int i = 0; i < (diskCount + 2); i++)
                {
                    for (int j = 0; j < tower.Length; j++)
                    {
                        if (tower[j][i] != 0)
                            Console.Write($"[{tower[j][i]}]\t");
                        else
                            Console.Write(" |\t");
                    }
                    Console.WriteLine();
                    if (i + 1 == (diskCount + 2))
                        Console.WriteLine($"-0-\t-1-\t-2-");
                }

                do
                {
                    Console.Write($"\nSource Tower: ");
                    string userST = Console.ReadLine();
                    int userTower;
                    if (int.TryParse(userST, out userTower))
                    {
                        if (userTower > 2 || userTower < 0)
                            Console.WriteLine("Enter [0] [1] [2] Tower Only");
                        else
                        {
                            if (tower[userTower][tower[userTower].Length-1] != 0)
                            {
                                sourceTower = userTower;
                                inputAuth = true;
                                for (int i = 0; i < tower[sourceTower].Length; i++)
                                {
                                    if (tower[sourceTower][i] > sourceDisk)
                                    {
                                        sourceDisk = tower[sourceTower][i];
                                        sourceIndex = i;
                                        Console.WriteLine($"Source disk found [{sourceDisk}] at Tower[{sourceTower}] in index [{sourceIndex}]");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No disk in this tower");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid Tower");
                    }
                }while(!inputAuth);

            }
        }
    }
}

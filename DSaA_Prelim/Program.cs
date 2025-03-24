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
            ConsoleColor[] colors = {ConsoleColor.White,ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.DarkRed,ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow,ConsoleColor.DarkMagenta };
            int diskCount = 0;
            int moveCount = 0;
            bool userDisk = false;
            bool mainGame = true;

            do // This is the Starting Disks Count Input Checker.
            {
                Console.Write("How many disks?: ");
                if (int.TryParse(Console.ReadLine(), out diskCount)) {
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
            for (int i = 0; i < tower.Length; i++) {
                tower[i] = new int[diskCount+2];
                for (int j = 0; j < tower[i].Length; j++) {
                    if (i == 0)
                    {
                        if (j < diskCount)
                            tower[i][(tower[i].Length - 1) - j] = diskCount - j;
                        else
                            tower[i][(tower[i].Length - 1) - j] = 0;
                    }
                    else
                        tower[i][j] = 0;
                }
            }

            // This is the Printer Loop for elements of the Tower.
            for (int i = 0; i < (diskCount + 2); i++)  {
                for (int j = 0; j < tower.Length; j++) {
                    Console.ForegroundColor = colors[tower[j][i]];
                    if (tower[j][i] != 0)
                        Console.Write($"[{tower[j][i]}]\t");
                    else
                        Console.Write(" |\t");
                    Console.ResetColor();
                }
                Console.WriteLine();
                if (i+1 == (diskCount + 2))
                    Console.WriteLine($"-0-\t-1-\t-2-");
            }
            Console.WriteLine($"Welcome to Tower of Hanoi!\nThe Goal is to move all the disk to Tower [2].\n|Reminder|\nYou can't put higher value disk on top of lower value disk.\n\nPress enter to Start...");
            Console.ReadKey();
            while (mainGame) // This Holds the whole main loop for the whole game
            {
                bool towerAuth = false;

                while (!towerAuth)  // This holds the authentication for tower choices of the user and under goes several constraints
                {
                    bool inputAuth = false;
                    int sourceTower = 0;
                    int sourceDisk = 0;
                    int sourceIndex = 0;

                    int targetTower = 0;
                    int targetDisk = 0;
                    int targetIndex = 0;
                    do
                    {
                        Console.Write($"\nSource Tower: ");
                        string userSorT = Console.ReadLine();
                        int userTower;
                        if (int.TryParse(userSorT, out userTower)) {
                            if (userTower <= 2 && userTower >= 0) {
                                if (tower[userTower][tower[userTower].Length - 1] != 0) { // This checks if the bottom of the source tower has content.
                                    sourceTower = userTower;
                                    inputAuth = true;
                                    for (int i = 0; i < tower[sourceTower].Length; i++) { // This Finds the upper most part of an existing disk and takes all the info.
                                        if (tower[sourceTower][i] > 0) {
                                            sourceDisk = tower[sourceTower][i];
                                            sourceIndex = i;
                                            break;
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("No disk in this tower");
                            }
                            else
                                Console.WriteLine("Enter [0] [1] [2] Tower Only");
                        }
                        else
                            Console.WriteLine("Enter a valid Tower");

                    } while (!inputAuth);

                    inputAuth = false;
                    do
                    {
                        Console.Write($"\nTarget Tower: ");
                        string userTarT = Console.ReadLine();
                        int userTower;
                        if (int.TryParse(userTarT, out userTower)) {
                            if (userTower == sourceTower)
                                Console.WriteLine("You cant use the same tower");
                            else if (userTower <= 2 && userTower >= 0) {
                                targetTower = userTower;
                                inputAuth = true;
                                if (tower[userTower][tower[userTower].Length - 1] != 0) {
                                    for (int i = 0; i < tower[targetTower].Length; i++) { // This Finds the upper most part of an existing disk and takes all the info.
                                        if (tower[targetTower][i] > 0) {
                                            targetDisk = tower[targetTower][i];
                                            targetIndex = i;
                                            break;
                                        }
                                    }
                                }
                                else
                                    targetIndex = tower[targetTower].Length - 1;
                            }
                            else
                                Console.WriteLine("Enter [0] [1] [2] Tower Only");
                        }
                        else
                            Console.WriteLine("Enter a valid Tower");

                    } while (!inputAuth);
                    Console.Clear();
                    if (targetDisk > sourceDisk) {
                        tower[targetTower][targetIndex - 1] = sourceDisk;
                        tower[sourceTower][sourceIndex] = 0;
                        towerAuth = true;
                        moveCount++;
                    }
                    else if (targetDisk == 0) {
                        tower[targetTower][targetIndex] = sourceDisk;
                        tower[sourceTower][sourceIndex] = 0;
                        towerAuth = true;
                        moveCount++;
                    }
                    else
                        Console.WriteLine("The disk thats is going to move has a higher value than the existing disk in that tower.");

                    Console.WriteLine($"Move Counter: {moveCount}");
                    for (int i = 0; i < (diskCount + 2); i++) {
                        for (int j = 0; j < tower.Length; j++) {
                            Console.ForegroundColor = colors[tower[j][i]];
                            if (tower[j][i] != 0)
                                Console.Write($"[{tower[j][i]}]\t");
                            else
                                Console.Write(" |\t");
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                        if (i + 1 == (diskCount + 2))
                            Console.WriteLine($"-0-\t-1-\t-2-");
                    }
                    for (int k = 0; k < tower[0].Length-2; k++) {
                        if (tower[2][tower[2].Length-1-k] != tower[2].Length-2-k)
                            break;

                        else if (k >= tower.Length - 1) {
                            int leastMoves = 2;
                            for (int i = 0; i < diskCount-1; i++)
                                leastMoves = leastMoves * 2;
                            leastMoves = leastMoves-1;
                            float accuracy = ((float)leastMoves / (float)moveCount)*100;
                            Console.WriteLine($"FINISHED!\nYour Accuracy is [{accuracy}%] | [{moveCount}] of [{leastMoves}]");
                            Console.ReadKey();
                            mainGame = false;
                            break;
                        }

                    }

                }
            }
        }
    }
}

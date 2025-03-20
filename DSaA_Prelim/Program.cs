using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSaA_Prelim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[][] tower = new int[3][];
            int diskCount = 0;
            int sourceValue = 0;
            int targetValue = 0;
            bool userDisk = false;
            bool mainGame = true;

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
        }
    }
}

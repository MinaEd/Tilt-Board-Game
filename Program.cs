using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static TiltGame.Movements;
namespace TiltGame
{
    class Program
    {
        static System.Diagnostics.Stopwatch myStopWatch = new System.Diagnostics.Stopwatch();
        static void Main(string[] args)
        {
            string basePath = @"C:\3rdYear2ndSemester\Algo\Project\[4] Tilt Game\Test Cases";
            while (true)
            {
                Console.WriteLine("Enter 1 for Sample Tests or 2 for Complete Tests:");
                string userInput = Console.ReadLine();

                string folderPath;
                if (userInput == "1")
                {
                    folderPath = Path.Combine(basePath, "Sample Tests");
                    ReadFilesFromFolder(folderPath, true);
                }
                else if (userInput == "2")
                {
                    folderPath = Path.Combine(basePath, "Complete Tests");

                    Console.WriteLine("Choose subfolder:");
                    Console.WriteLine("1. Small");
                    Console.WriteLine("2. Medium");
                    Console.WriteLine("3. Large");
                    string subfolderChoice = Console.ReadLine();

                    string subfolderName;
                    if (subfolderChoice == "1")
                    {
                        subfolderName = "1 small";
                    }
                    else if (subfolderChoice == "2")
                    {
                        subfolderName = "2 medium";
                    }
                    else if (subfolderChoice == "3")
                    {
                        subfolderName = "3 large";
                    }
                    else
                    {
                        Console.WriteLine("Invalid subfolder choice. Exiting program.");
                        return;
                    }

                    folderPath = Path.Combine(basePath, "Complete Tests", subfolderName);

                    
                    ReadFilesFromFolder(folderPath, false);
                }
                else
                {
                    Console.WriteLine("Invalid input. Exiting program.");
                    break;
                }
            }
        }

        static void ReadFilesFromFolder(string folderPath, Boolean isSample)
        {
            string[] files = Directory.GetFiles(folderPath, "Case*.txt", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                
                if (file.EndsWith("-output.txt"))
                {
                    continue;
                }
                string[] lines = File.ReadAllLines(file);

            

                if (!int.TryParse(lines[0], out int size))
                {

                    continue; 
                }

                
                char[,] matrix = new char[size, size];

                
                int countOfSliders = 0;
                int[,] positionsOfSlider = new int[size * size, 2];
                int c = 0, r = 0;
                state s = new state();

                s.indicesOfSliders = new List<Tuple<int, int>>();

                for (int i = 0; i < size; i++)
                {
                    string[] row = lines[i + 1].Split(',');

                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = row[j].Trim()[0]; 
                        if (matrix[i, j] == 'o')
                        {
                            matrix[i, j] = '.';
                            countOfSliders++;
                            positionsOfSlider[r, 0] = i;
                            positionsOfSlider[r++, 1] = j;
                            s.indicesOfSliders.Add(Tuple.Create(i, j));
                        }

                    }
                }

                
                string[] targetCoords = lines[lines.Length - 1].Split(',');
                int targetX, targetY;
                targetX = int.Parse(targetCoords[0]);
                targetY = int.Parse(targetCoords[1]);
                target = new Tuple<int, int>(targetY, targetX);
                finalStateReached = false;
                finalState = -1;
                emptyMatrix = matrix;

                myStopWatch.Restart();
                BFS(s, size, countOfSliders);
                myStopWatch.Stop();
                char[] seqArr = new char[minNumOfMoves];
                int num = 0;
                while (sequence != null && sequence.Count > 0)
                {
                    char dir = sequence.Pop();
                    seqArr[num] = dir;
                    num++;
                    if (dir == 'l')
                    {
                        Console.Write("left, ");
                    }
                    if (dir == 'r')
                    {
                        Console.Write("right, ");
                    }
                    if (dir == 'u')
                    {
                        Console.Write("up, ");
                    }
                    if (dir == 'd')
                    {
                        Console.Write("down, ");
                    }

                }
                if (isSample && seqArr.Length > 0)
                {
                    s = new state();
                    s.indicesOfSliders = new List<Tuple<int, int>>();
                    Console.WriteLine("");
                    Console.WriteLine("intial");
                    for (int i = 0; i < countOfSliders; i++)
                    {
                        emptyMatrix[positionsOfSlider[i, 0], positionsOfSlider[i, 1]] = 'o';
                        s.indicesOfSliders.Add(Tuple.Create(positionsOfSlider[i, 0], positionsOfSlider[i, 1]));

                    }
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            Console.Write(emptyMatrix[i, j] + ", ");
                        }
                        Console.WriteLine("");
                    }
                    for (int i = 0; i < countOfSliders; i++)
                    {
                        emptyMatrix[positionsOfSlider[i, 0], positionsOfSlider[i, 1]] = '.';
                    }
                    for (int i = 0; i < seqArr.Length; i++)
                    {
                        Console.WriteLine("---------------------------------");
                        if (seqArr[i] == 'l')
                        {
                            Console.WriteLine("left");
                            s = moveLeft(s, size, countOfSliders);
                        }
                        if (seqArr[i] == 'r')
                        {
                            Console.WriteLine("right");
                            s = moveRight(s, size, countOfSliders);
                        }
                        if (seqArr[i] == 'u')
                        {
                            Console.WriteLine("up");
                            s = moveUp(s, size, countOfSliders);
                        }
                        if (seqArr[i] == 'd')
                        {
                            Console.WriteLine("down");
                            s = moveDown(s, size, countOfSliders);
                        }
                        for (int l = 0; l < countOfSliders; l++)
                        {
                            emptyMatrix[s.indicesOfSliders[l].Item1, s.indicesOfSliders[l].Item2] = 'o';
                        }
                        for (int k = 0; k < size; k++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                Console.Write(emptyMatrix[k, j] + ", ");
                            }
                            Console.WriteLine("");
                        }
                        for (int l = 0; l < countOfSliders; l++)
                        {
                            emptyMatrix[s.indicesOfSliders[l].Item1, s.indicesOfSliders[l].Item2] = '.';
                        }
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("the time in seconds is " + myStopWatch.Elapsed.Seconds.ToString());
                Console.WriteLine("the time in mins is " + myStopWatch.Elapsed.Minutes.ToString());
                
                Console.WriteLine("-----------------------------------------------");

                

                Console.WriteLine();
            }

        }


    }
}
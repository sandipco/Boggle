using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace Boggle
{
    class Program
    {

        static string[,] board;
        static long clock;
         const int totalTime = 180;
        static Thread tick;
        static System.Timers.Timer tim;
        static List<string> parents;
        static int rootRow, rootCol;

        static clsDictionary objDictionary;
        
        static void Main(string[] args)
        {
            Console.WriteLine("************WELCOME TO THE GAME OF BOGGLE*****************");
            Console.Write("            written by");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Sandip Timsina");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Send in your feedback to sandipco@gmail.com");
            Console.WriteLine("\n");
            Console.WriteLine("*********************Rules**********************");
            Console.WriteLine("* Start with a letter and move in any direction to its adjacent neighbor");
            Console.WriteLine("and continue until you find a valid word");
            Console.WriteLine("The 'Qu' cube counts as two letters");
            Console.WriteLine("* You cannot repeat a word");
            Console.WriteLine("* You have to complete the game in 3 minutes\n");
            Console.WriteLine("*******************SCORE************************");
            Console.WriteLine("NO. OF LETTERS   3    4    5    6    7   8 or more");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("POINTS           1    1    2    3    5   11");
            Console.WriteLine("************************************************");
            Console.WriteLine("\n");
            objDictionary = new clsDictionary();
            clock = totalTime;
            parents = new List<string>();
            
            tick = new Thread(runClock);
            tick.Start();
            prepareBoard();
            
            
            for (int i = 0; i < 4; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                for (int j = 0; j < 4; j++)
                {
                    
                        Console.Write("      " + board[i,j]);
                }
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine();
            beginProcess();
            
            Console.ReadKey();
            
        }
        static void runClock()
        {
            tim = new System.Timers.Timer(1000);
            tim.Elapsed += new System.Timers.ElapsedEventHandler(onTimeOut);
            tim.Enabled = true;
            
        }
        static void printWords()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            string strFileName = "latestOutput.txt";
            using (StreamWriter writer = new StreamWriter(strFileName))
            {
                for (int i = 0; i < 4; i++)
                {
                    writer.WriteLine();
                    for (int j = 0; j < 4; j++)
                    {

                        writer.Write("      " + board[i, j]);
                    }
                }
                writer.WriteLine();
                writer.WriteLine("*************OUTPUT***************");
                foreach (var v in objDictionary.validWords)
                {
                    writer.WriteLine(v.Value);
                    Console.WriteLine(v.Value);
                }
            }
            Console.WriteLine();
            Console.WriteLine("*************SCORE***********************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You could have achieved {0}", objDictionary.totalPoints);
            Console.ReadKey();
        }
        private static void beginProcess()
        {
            //rootCol = 0; rootRow = 0;
            //parents.Add("00");
            //lookForWord("G", 0, 0);
            //foreach (var v in parents)
            //    Console.WriteLine("Parents " + v);
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    rootCol = j; rootRow = i;
                    parents.Clear();
                    parents.Add(i.ToString() + j.ToString());
                    lookForWord(board[i, j], j, i);
                }

        }
        private static void lookForWord(string strSeq, int col, int row)
        {
            //Search in clockwise direction
            
            int nRow, nCol;
            if (row < 0 || row > 3 || col < 0 || col > 3)
                return;
            string newSequence = "";
            
            if (objDictionary.doesWordExist(strSeq))
            {
               
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        nRow = row + i;
                        nCol = col + j;
                        if ((nRow >= 0 && nRow <= 3) && (nCol >= 0 && nCol <= 3) 
                                && !parents.Contains(nRow.ToString() + nCol.ToString())
                                && !(nRow==rootRow && nCol==rootCol) 
                                )
                        {
                                parents.Add(nRow.ToString() + nCol.ToString());
                                string curPiece = board[nRow, nCol];
                                if (curPiece.ToUpper().CompareTo("QU") == 0)
                                    curPiece = "Q";
                                newSequence = strSeq + curPiece;
                                lookForWord(newSequence, nCol, nRow);
                        }
                        

                    }
                parents.Remove(row.ToString() + col.ToString());
            }
            else
            {
                int l=parents.IndexOf(row.ToString() + col.ToString());
                if(l>=0)
                for (int k = l; k < parents.Count; k++)
                    parents.RemoveAt(k);
            }
            
            
        }
        
        private static void prepareBoard()
        {
            board = new string[4, 4];
            //string array is required because of Qu
            string[] dice1 = { "A", "W", "O", "T", "O", "T" };
            string[] dice2 = { "B", "O", "A", "J", "B", "O" };
            string[] dice3 = { "H", "N", "Z", "N", "H", "L" };
            string[] dice4 = { "I", "S", "T", "O", "E", "S" };

            string[] dice5 = { "T", "S", "D", "I", "Y", "T" };
            string[] dice6 = { "P", "S", "C", "A", "O", "H" };
            string[] dice7 = { "T", "Y", "T", "L", "R", "E" };
            string[] dice8 = { "E", "R", "X", "I", "D", "L" };

            string[] dice9 = { "F", "S", "A", "F", "P", "K" };
            string[] dice10 = { "Qu", "U", "I", "H", "N", "M" };
            string[] dice11 = { "S", "N", "E", "I", "U", "E" };
            string[] dice12 = { "A", "E", "A", "N", "G", "E" };

            string[] dice13 = { "R", "V", "T", "W", "E", "H" };
            string[] dice14 = { "T", "M", "U", "O", "I", "C" };
            string[] dice15 = { "W", "E", "E", "N", "G", "H" };
            string[] dice16 = { "L", "Y", "V", "R", "D", "E" };

            Random rdm = new Random();

            board[0, 0] = dice1[rdm.Next(0, 5)];
            board[0, 1] = dice2[rdm.Next(0, 5)];
            board[0, 2] = dice3[rdm.Next(0, 5)];
            board[0, 3] = dice4[rdm.Next(0, 5)];

            board[1, 0] = dice5[rdm.Next(0, 5)];
            board[1, 1] = dice6[rdm.Next(0, 5)];
            board[1, 2] = dice7[rdm.Next(0, 5)];
            board[1, 3] = dice8[rdm.Next(0, 5)];

            board[2, 0] = dice9[rdm.Next(0, 5)];
            board[2, 1] = dice10[rdm.Next(0, 5)];
            board[2, 2] = dice11[rdm.Next(0, 5)];
            board[2, 3] = dice12[rdm.Next(0, 5)];

            board[3, 0] = dice13[rdm.Next(0, 5)];
            board[3, 1] = dice14[rdm.Next(0, 5)];
            board[3, 2] = dice15[rdm.Next(0, 5)];
            board[3, 3] = dice16[rdm.Next(0, 5)];
        }
   
    
        private static void onTimeOut(object source, ElapsedEventArgs e)
        {
            clock--;
            if (clock == 0)
            {
                tim.Stop();
                printWords();

            }
            Console.BackgroundColor = ConsoleColor.Red;
            int sec=(int)((clock % 60));
            string secs = sec.ToString();
            if (sec < 10)
                secs = "0" + secs;
            int min =(int) (clock / 60);
            Console.Write("\r{0}:{1}   ", min,secs);
            Console.BackgroundColor = ConsoleColor.Black ;
        }
    }
}

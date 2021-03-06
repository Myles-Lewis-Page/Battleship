using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mpage2battleship
{
    #region Global
    /// <summary>
    /// Holds global variabls
    /// </summary>
    public class Globals
    {
        public static int destroyersLeft = 0;
        public static int subsLeft = 0;
        public static int battleshipLeft = 0;
        public static int carriersLeft = 0;
        public static int lastShot = 0;

    }

    #endregion

    #region Board
    public class Gameboard
    {
        /// <summary>
        /// Board variable
        /// </summary>
        public static string[,] board;
        /// <summary>
        /// Make the board and fill it with blanks 
        /// </summary>
        public static void makeBoard()
        {
            board = new string[10, 10];
            int row = board.GetLength(0);
            int col = board.GetLength(1);
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    board[x, y] = "0";
                }
            }
        }
        /// <summary>
        /// prints the board using strings in spot to decide what to print
        /// </summary>
        /// <param name="cheatMode">If hides the ships or not</param>
        public static void printBoard(Boolean cheatMode)
        {
            int row = board.GetLength(0);
            int col = board.GetLength(1);
            Console.WriteLine("   A B C D E F G H I J");

            for (int x = 0; x < row; x++)
            {
                Console.Write(" " + (x) + "|");
                for (int y = 0; y < col; y++)
                {
                    if (board[x, y] == "0")
                    {
                        Console.Write("  ");
                    }
                    else if (board[x, y] == "1d" || board[x, y] == "1s" || board[x, y] == "1b" || board[x, y] == "1c")
                    {
                        if (cheatMode == true && board[x, y] == "1d")
                        {
                            Console.Write("D ");
                        }
                        else if (cheatMode == true && board[x, y] == "1s")
                        {
                            Console.Write("S ");
                        }
                        else if (cheatMode == true && board[x, y] == "1b")
                        {
                            Console.Write("B ");
                        }
                        else if (cheatMode == true && board[x, y] == "1c")
                        {
                            Console.Write("C ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                    else if (board[x, y] == "2")
                    {
                        Console.Write("O ");
                    }
                    else if (board[x, y] == "3")
                    {
                        Console.Write("X ");
                    }


                }

                Console.Write("\n");
            }
            Console.Write("\n\nShips Left\n___________________________\n");
            Console.Write("Destroyers Left: \t" + Globals.destroyersLeft + "\n");
            Console.Write("Submarines Left: \t" + Globals.subsLeft + "\n");
            Console.Write("Battleships Left: \t" + Globals.battleshipLeft + "\n");
            Console.Write("Carriers Left:    \t" + Globals.carriersLeft + "\n");

        }
        /// <summary>
        /// Check what is in a designated spot on the board
        /// </summary>
        /// <param name="x">X location</param>
        /// <param name="y">y location</param>
        /// <returns></returns>
        public static string checkSpot(int x, int y)
        {
            return board[x, y];
        }
    }
    #endregion

    #region gameLogic
    public class Game
    {
        /// <summary>
        /// call the ship classes to place the ships
        /// </summary>
        public static void placeShips()
        {
            var rand = new Random();
            int board = rand.Next(1,6);
            destroyer.add(board);
            submarines.add(board);
            battleship.add(board);
            carrier.add(board);
        }

        /// <summary>
        /// Using user input shoots a shot. Using last shot to decide what happend lasttime to tell the user
        /// </summary>
        public static void shoot()
        {
            switch (Globals.lastShot)
            {
                case (1):
                    Console.Write("\nLast shot was a HIT!\n\n");
                    break;
                case (2):
                    Console.Write("\nLast shot was a MISS!\n\n");
                    break;
                case (3):
                    Console.Write("\nLast shot was a HIT!\n");
                    Console.Write("DESTROYER SANK\n");
                    break;
                case (4):
                    Console.Write("\nLast shot was a HIT!\n");
                    Console.Write("SUB SANK\n");
                    break;
                case (5):
                    Console.Write("\nLast shot was a HIT!\n");
                    Console.Write("BATTLESHIP SANK\n");
                    break;
                case (6):
                    Console.Write("\nLast shot was a HIT!\n");
                    Console.Write("CARRIER SANK\n");
                    break;
            }

            Console.Write("Select your Letter to shoot\n");
            string letter = Console.ReadLine();

            Console.Write("Select your Number to shoot\n");
            int number = int.Parse(Console.ReadLine());

            int letterNum = 7;

            switch (letter.ToUpper())
            {
                case ("A"):
                    letterNum = 0;
                    break;
                case ("B"):
                    letterNum = 1;
                    break;
                case ("C"):
                    letterNum = 2;
                    break;
                case ("D"):
                    letterNum = 3;
                    break;
                case ("E"):
                    letterNum = 4;
                    break;
                case ("F"):
                    letterNum = 5;
                    break;
                case ("G"):
                    letterNum = 6;
                    break;
                case ("H"):
                    letterNum = 7;
                    break;
                case ("I"):
                    letterNum = 8;
                    break;
                case ("J"):
                    letterNum = 9;
                    break;
            }

            String check = Gameboard.checkSpot(number, letterNum);
            if (check == "0")
            {
                Gameboard.board[number, letterNum] = "2";
                Globals.lastShot = 2;
            }
            else
            {
                Gameboard.board[number, letterNum] = "3";
                Globals.lastShot = 1;
                submarines.check();
                destroyer.check();
                battleship.check();
                carrier.check();
            }
        }
    }
    #endregion

    #region ships
    /// <summary>
    /// Destroyer Class
    /// </summary>
    public class destroyer
    {
        static int[,] destroyer1 = new int[2, 2];
        static int[,] destroyer2 = new int[2, 2];
        static Boolean destroyer1Sunk = false;
        static Boolean destroyer2Sunk = false;

        /// <summary>
        /// add the destroyers tothe board
        /// </summary>
        public static void add(int board)
        {
            switch (board)
            {
                case 1:
                    Globals.destroyersLeft = 2;
                    destroyer1[0, 0] = 1;
                    destroyer1[0, 1] = 6;
                    destroyer1[1, 0] = 2;
                    destroyer1[1, 1] = 6;
                    Gameboard.board[1, 6] = "1d";
                    Gameboard.board[2, 6] = "1d";

                    destroyer2[0, 0] = 5;
                    destroyer2[0, 1] = 3;
                    destroyer2[1, 0] = 5;
                    destroyer2[1, 1] = 4;
                    Gameboard.board[5, 3] = "1d";
                    Gameboard.board[5, 4] = "1d";
                    break;
                case 2:
                    Globals.destroyersLeft = 2;
                    destroyer1[0, 0] = 5;
                    destroyer1[0, 1] = 1;
                    destroyer1[1, 0] = 6;
                    destroyer1[1, 1] = 1;
                    Gameboard.board[5, 1] = "1d";
                    Gameboard.board[6, 1] = "1d";

                    destroyer2[0, 0] = 3;
                    destroyer2[0, 1] = 6;
                    destroyer2[1, 0] = 3;
                    destroyer2[1, 1] = 7;
                    Gameboard.board[3, 6] = "1d";
                    Gameboard.board[3, 7] = "1d";
                    break;
                case 3:
                    Globals.destroyersLeft = 2;
                    destroyer1[0, 0] = 1;
                    destroyer1[0, 1] = 0;
                    destroyer1[1, 0] = 1;
                    destroyer1[1, 1] = 1;
                    Gameboard.board[1, 0] = "1d";
                    Gameboard.board[1, 1] = "1d";

                    destroyer2[0, 0] = 8;
                    destroyer2[0, 1] = 2;
                    destroyer2[1, 0] = 9;
                    destroyer2[1, 1] = 2;
                    Gameboard.board[8, 2] = "1d";
                    Gameboard.board[9, 2] = "1d";
                    break;
                case 4:
                    Globals.destroyersLeft = 2;
                    destroyer1[0, 0] = 5;
                    destroyer1[0, 1] = 1;
                    destroyer1[1, 0] = 5;
                    destroyer1[1, 1] = 2;
                    Gameboard.board[5, 1] = "1d";
                    Gameboard.board[5, 2] = "1d";

                    destroyer2[0, 0] = 2;
                    destroyer2[0, 1] = 8;
                    destroyer2[1, 0] = 2;
                    destroyer2[1, 1] = 9;
                    Gameboard.board[2, 8] = "1d";
                    Gameboard.board[2, 9] = "1d";
                    break;
                case 5:
                    Globals.destroyersLeft = 2;
                    destroyer1[0, 0] = 2;
                    destroyer1[0, 1] = 0;
                    destroyer1[1, 0] = 2;
                    destroyer1[1, 1] = 1;
                    Gameboard.board[2, 0] = "1d";
                    Gameboard.board[2, 1] = "1d";

                    destroyer2[0, 0] = 0;
                    destroyer2[0, 1] = 8;
                    destroyer2[1, 0] = 0;
                    destroyer2[1, 1] = 9;
                    Gameboard.board[0, 8] = "1d";
                    Gameboard.board[0, 9] = "1d";
                    break;

            }
            
        }

        /// <summary>
        /// Check if a destroyer is sunk
        /// </summary>
        public static void check()
        {
            if (destroyer1Sunk == false)
            {
                int x = 0;
                int shipHP = 2;
                while (x < 2)
                {
                    int location1 = destroyer1[x, 0];
                    int location2 = destroyer1[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 3;
                    Globals.destroyersLeft--;
                    destroyer1Sunk = true;
                }
            }

            if (destroyer2Sunk == false)
            {
                int x = 0;
                int shipHP = 2;
                while (x < 2)
                {
                    int location1 = destroyer2[x, 0];
                    int location2 = destroyer2[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 3;
                    Globals.destroyersLeft--;
                    destroyer2Sunk = true;
                }
            }
        }
    }
    /// <summary>
    /// Sub Class
    /// </summary>
    public class submarines
    {
        static int[,] submarine1 = new int[3, 2];
        static int[,] submarine2 = new int[3, 2];
        static Boolean sub1Sunk = false;
        static Boolean sub2Sunk = false;

        /// <summary>
        /// Add the subs to the board
        /// </summary>
        public static void add(int board)
        {
            switch (board)
            {
                case 1:
                    Globals.subsLeft = 2;
                    submarine1[0, 0] = 0;
                    submarine1[0, 1] = 0;
                    submarine1[1, 0] = 0;
                    submarine1[1, 1] = 1;
                    submarine1[2, 0] = 0;
                    submarine1[2, 1] = 2;
                    Gameboard.board[0, 0] = "1s";
                    Gameboard.board[0, 1] = "1s";
                    Gameboard.board[0, 2] = "1s";

                    submarine2[0, 0] = 7;
                    submarine2[0, 1] = 8;
                    submarine2[1, 0] = 8;
                    submarine2[1, 1] = 8;
                    submarine2[2, 0] = 9;
                    submarine2[2, 1] = 8;
                    Gameboard.board[7, 8] = "1s";
                    Gameboard.board[8, 8] = "1s";
                    Gameboard.board[9, 8] = "1s";
                    break;
                case 2:
                    Globals.subsLeft = 2;
                    submarine1[0, 0] = 0;
                    submarine1[0, 1] = 3;
                    submarine1[1, 0] = 1;
                    submarine1[1, 1] = 3;
                    submarine1[2, 0] = 2;
                    submarine1[2, 1] = 3;
                    Gameboard.board[0, 3] = "1s";
                    Gameboard.board[1, 3] = "1s";
                    Gameboard.board[2, 3] = "1s";

                    submarine2[0, 0] = 7;
                    submarine2[0, 1] = 3;
                    submarine2[1, 0] = 7;
                    submarine2[1, 1] = 4;
                    submarine2[2, 0] = 7;
                    submarine2[2, 1] = 5;
                    Gameboard.board[7, 3] = "1s";
                    Gameboard.board[7, 4] = "1s";
                    Gameboard.board[7, 5] = "1s";
                    break;
                case 3:
                    Globals.subsLeft = 2;
                    submarine1[0, 0] = 3;
                    submarine1[0, 1] = 4;
                    submarine1[1, 0] = 4;
                    submarine1[1, 1] = 4;
                    submarine1[2, 0] = 5;
                    submarine1[2, 1] = 4;
                    Gameboard.board[3, 4] = "1s";
                    Gameboard.board[4, 4] = "1s";
                    Gameboard.board[5, 4] = "1s";

                    submarine2[0, 0] = 4;
                    submarine2[0, 1] = 7;
                    submarine2[1, 0] = 4;
                    submarine2[1, 1] = 8;
                    submarine2[2, 0] = 4;
                    submarine2[2, 1] = 9;
                    Gameboard.board[4, 7] = "1s";
                    Gameboard.board[4, 8] = "1s";
                    Gameboard.board[4, 9] = "1s";
                    break;
                case 4:
                    Globals.subsLeft = 2;
                    submarine1[0, 0] = 5;
                    submarine1[0, 1] = 5;
                    submarine1[1, 0] = 5;
                    submarine1[1, 1] = 6;
                    submarine1[2, 0] = 5;
                    submarine1[2, 1] = 7;
                    Gameboard.board[5, 5] = "1s";
                    Gameboard.board[5, 6] = "1s";
                    Gameboard.board[5, 7] = "1s";

                    submarine2[0, 0] = 7;
                    submarine2[0, 1] = 9;
                    submarine2[1, 0] = 8;
                    submarine2[1, 1] = 9;
                    submarine2[2, 0] = 9;
                    submarine2[2, 1] = 9;
                    Gameboard.board[7, 9] = "1s";
                    Gameboard.board[8, 9] = "1s";
                    Gameboard.board[9, 9] = "1s";
                    break;
                case 5:
                    Globals.subsLeft = 2;
                    submarine1[0, 0] = 8;
                    submarine1[0, 1] = 4;
                    submarine1[1, 0] = 8;
                    submarine1[1, 1] = 5;
                    submarine1[2, 0] = 8;
                    submarine1[2, 1] = 6;
                    Gameboard.board[8, 4] = "1s";
                    Gameboard.board[8, 5] = "1s";
                    Gameboard.board[8, 6] = "1s";

                    submarine2[0, 0] = 0;
                    submarine2[0, 1] = 4;
                    submarine2[1, 0] = 1;
                    submarine2[1, 1] = 4;
                    submarine2[2, 0] = 2;
                    submarine2[2, 1] = 4;
                    Gameboard.board[0, 4] = "1s";
                    Gameboard.board[1, 4] = "1s";
                    Gameboard.board[2, 4] = "1s";
                    break;
            }
            
        }

        /// <summary>
        /// Check if either sub is sunk
        /// </summary>
        public static void check()
        {
            if (sub1Sunk == false)
            {
                int x = 0;
                int shipHP = 3;
                while (x < 3)
                {

                    int location1 = submarine1[x, 0];
                    int location2 = submarine1[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 4;
                    Globals.subsLeft--;
                    sub1Sunk = true;
                }
            }

            if (sub2Sunk == false)
            {
                int x = 0;
                int shipHP = 3;
                while (x < 3)
                {
                    int location1 = submarine2[x, 0];
                    int location2 = submarine2[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 4;
                    Globals.subsLeft--;
                    sub2Sunk = true;
                }
            }

        }

    }

    /// <summary>
    /// Battleship Class
    /// </summary>
    public class battleship
    {
        static int[,] battleship1 = new int[4, 2];
        static Boolean battleship1Sunk = false;

        /// <summary>
        /// add the battleship to the board
        /// </summary>
        public static void add(int board)
        {
            switch (board)
            {
                case 1:
                    Globals.battleshipLeft = 1;
                    battleship1[0, 0] = 3;
                    battleship1[0, 1] = 0;
                    battleship1[1, 0] = 3;
                    battleship1[1, 1] = 1;
                    battleship1[2, 0] = 3;
                    battleship1[2, 1] = 2;
                    battleship1[3, 0] = 3;
                    battleship1[3, 1] = 3;
                    Gameboard.board[3, 0] = "1b";
                    Gameboard.board[3, 1] = "1b";
                    Gameboard.board[3, 2] = "1b";
                    Gameboard.board[3, 3] = "1b";
                    break;
                case 2:
                    Globals.battleshipLeft = 1;
                    battleship1[0, 0] = 5;
                    battleship1[0, 1] = 7;
                    battleship1[1, 0] = 6;
                    battleship1[1, 1] = 7;
                    battleship1[2, 0] = 7;
                    battleship1[2, 1] = 7;
                    battleship1[3, 0] = 8;
                    battleship1[3, 1] = 7;
                    Gameboard.board[5, 7] = "1b";
                    Gameboard.board[6, 7] = "1b";
                    Gameboard.board[7, 7] = "1b";
                    Gameboard.board[8, 7] = "1b";
                    break;
                case 3:
                    Globals.battleshipLeft = 1;
                    battleship1[0, 0] = 9;
                    battleship1[0, 1] = 4;
                    battleship1[1, 0] = 9;
                    battleship1[1, 1] = 5;
                    battleship1[2, 0] = 9;
                    battleship1[2, 1] = 6;
                    battleship1[3, 0] = 9;
                    battleship1[3, 1] = 7;
                    Gameboard.board[9, 4] = "1b";
                    Gameboard.board[9, 5] = "1b";
                    Gameboard.board[9, 6] = "1b";
                    Gameboard.board[9, 7] = "1b";
                    break;
                case 4:
                    Globals.battleshipLeft = 1;
                    battleship1[0, 0] = 6;
                    battleship1[0, 1] = 0;
                    battleship1[1, 0] = 7;
                    battleship1[1, 1] = 0;
                    battleship1[2, 0] = 8;
                    battleship1[2, 1] = 0;
                    battleship1[3, 0] = 9;
                    battleship1[3, 1] = 0;
                    Gameboard.board[6, 0] = "1b";
                    Gameboard.board[7, 0] = "1b";
                    Gameboard.board[8, 0] = "1b";
                    Gameboard.board[9, 0] = "1b";
                    break;
                case 5:
                    Globals.battleshipLeft = 1;
                    battleship1[0, 0] = 6;
                    battleship1[0, 1] = 0;
                    battleship1[1, 0] = 7;
                    battleship1[1, 1] = 0;
                    battleship1[2, 0] = 8;
                    battleship1[2, 1] = 0;
                    battleship1[3, 0] = 9;
                    battleship1[3, 1] = 0;
                    Gameboard.board[6, 0] = "1b";
                    Gameboard.board[7, 0] = "1b";
                    Gameboard.board[8, 0] = "1b";
                    Gameboard.board[9, 0] = "1b";
                    break;
            }
        }

        /// <summary>
        /// Check if the battleship is sunk
        /// </summary>
        public static void check()
        {
            if (battleship1Sunk == false)
            {
                int x = 0;
                int shipHP = 4;
                while (x < 4)
                {

                    int location1 = battleship1[x, 0];
                    int location2 = battleship1[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 5;
                    Globals.battleshipLeft--;
                    battleship1Sunk = true;
                }
            }
        }
    }

    /// <summary>
    /// Carrier Class
    /// </summary>
    public class carrier
    {

        static int[,] carrier1 = new int[5, 2];
        static Boolean carrier1Sunk = false;

        /// <summary>
        /// Add the Carrier to the board
        /// </summary>
        public static void add(int board)
        {
            switch (board)
            {
                case 1:
                    Globals.carriersLeft = 1;
                    carrier1[0, 0] = 7;
                    carrier1[0, 1] = 1;
                    carrier1[1, 0] = 7;
                    carrier1[1, 1] = 2;
                    carrier1[2, 0] = 7;
                    carrier1[2, 1] = 3;
                    carrier1[3, 0] = 7;
                    carrier1[3, 1] = 4;
                    carrier1[4, 0] = 7;
                    carrier1[4, 1] = 5;
                    Gameboard.board[7, 1] = "1c";
                    Gameboard.board[7, 2] = "1c";
                    Gameboard.board[7, 3] = "1c";
                    Gameboard.board[7, 4] = "1c";
                    Gameboard.board[7, 5] = "1c";
                    break;
                case 2:
                    Globals.carriersLeft = 1;
                    carrier1[0, 0] = 1;
                    carrier1[0, 1] = 5;
                    carrier1[1, 0] = 1;
                    carrier1[1, 1] = 6;
                    carrier1[2, 0] = 1;
                    carrier1[2, 1] = 7;
                    carrier1[3, 0] = 1;
                    carrier1[3, 1] = 8;
                    carrier1[4, 0] = 1;
                    carrier1[4, 1] = 9;
                    Gameboard.board[1, 5] = "1c";
                    Gameboard.board[1, 6] = "1c";
                    Gameboard.board[1, 7] = "1c";
                    Gameboard.board[1, 8] = "1c";
                    Gameboard.board[1, 9] = "1c";
                    break;
                case 3:
                    Globals.carriersLeft = 1;
                    carrier1[0, 0] = 6;
                    carrier1[0, 1] = 3;
                    carrier1[1, 0] = 6;
                    carrier1[1, 1] = 4;
                    carrier1[2, 0] = 6;
                    carrier1[2, 1] = 5;
                    carrier1[3, 0] = 6;
                    carrier1[3, 1] = 6;
                    carrier1[4, 0] = 6;
                    carrier1[4, 1] = 7;
                    Gameboard.board[6, 3] = "1c";
                    Gameboard.board[6, 4] = "1c";
                    Gameboard.board[6, 5] = "1c";
                    Gameboard.board[6, 6] = "1c";
                    Gameboard.board[6, 7] = "1c";
                    break;
                case 4:
                    Globals.carriersLeft = 1;
                    carrier1[0, 0] = 0;
                    carrier1[0, 1] = 2;
                    carrier1[1, 0] = 1;
                    carrier1[1, 1] = 2;
                    carrier1[2, 0] = 2;
                    carrier1[2, 1] = 2;
                    carrier1[3, 0] = 3;
                    carrier1[3, 1] = 2;
                    carrier1[4, 0] = 4;
                    carrier1[4, 1] = 2;
                    Gameboard.board[0, 2] = "1c";
                    Gameboard.board[1, 2] = "1c";
                    Gameboard.board[2, 2] = "1c";
                    Gameboard.board[3, 2] = "1c";
                    Gameboard.board[4, 2] = "1c";
                    break;
                case 5:
                    Globals.carriersLeft = 1;
                    carrier1[0, 0] = 6;
                    carrier1[0, 1] = 3;
                    carrier1[1, 0] = 6;
                    carrier1[1, 1] = 4;
                    carrier1[2, 0] = 6;
                    carrier1[2, 1] = 5;
                    carrier1[3, 0] = 6;
                    carrier1[3, 1] = 6;
                    carrier1[4, 0] = 6;
                    carrier1[4, 1] = 7;
                    Gameboard.board[6, 3] = "1c";
                    Gameboard.board[6, 4] = "1c";
                    Gameboard.board[6, 5] = "1c";
                    Gameboard.board[6, 6] = "1c";
                    Gameboard.board[6, 7] = "1c";
                    break;
            }
        }

        /// <summary>
        /// Check if the carrier is sunk
        /// </summary>
        public static void check()
        {
            if (carrier1Sunk == false)
            {
                int x = 0;
                int shipHP = 5;
                while (x < 5)
                {

                    int location1 = carrier1[x, 0];
                    int location2 = carrier1[x, 1];
                    string value = Gameboard.checkSpot(location1, location2);
                    if (value == "3")
                    {
                        shipHP--;
                    }
                    x++;
                }
                if (shipHP == 0)
                {
                    Globals.lastShot = 6;
                    Globals.carriersLeft--;
                    carrier1Sunk = true;
                }
            }
        }
    }
    #endregion

    #region main
    class Program
    {
        /// <summary>
        /// Main 
        /// </summary>
        static public int menu = 0;
        static void Main(string[] args)
        {
            Boolean cheatmode = printMenu();
            while (menu == 1)
            {
                play(cheatmode);
            }
            if (menu == 3)
            {
                Console.WriteLine("Quitting");
            }
        }

        /// <summary>
        /// When menu selects play 
        /// </summary>
        /// <param name="cheatmode">If hides the ships or not</param>
        static void play(Boolean cheatmode)
        {
            Console.Clear();
            Gameboard.makeBoard();
            Game.placeShips();
            Boolean shipsLeft = true;
            while (shipsLeft == true)
            {
                Gameboard.printBoard(cheatmode);
                Game.shoot();
                Console.Clear();
                if ((Globals.battleshipLeft == 0) && (Globals.carriersLeft == 0) && (Globals.destroyersLeft == 0) && (Globals.subsLeft == 0))
                {
                    shipsLeft = false;
                }
            }
            Console.WriteLine("You won");
            cheatmode = printMenu();

            Console.ReadLine();


        }

        /// <summary>
        /// Prints the menu and turns on or off cheat mode
        /// </summary>
        /// <returns></returns>
        static Boolean printMenu()
        {
            Boolean cheatmode = false;
            while (true)
            {
                Console.WriteLine("Welcome to Battleship\n");
                Console.Write("Cheatmode is curently");
                if (cheatmode == false)
                {
                    Console.Write(" OFF\n");
                }
                else
                {
                    Console.Write(" ON\n");
                }
                Console.WriteLine("1) Play");
                Console.WriteLine("2) Swap Cheat mode");
                Console.WriteLine("3) Quit");
                menu = int.Parse(Console.ReadLine());
                if (menu == 2)
                {
                    if (cheatmode == false)
                    {
                        cheatmode = true;
                    }
                    else
                    {
                        cheatmode = false;
                    }
                    Console.Clear();
                }
                else if (menu == 1 || menu == 3)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }
            return cheatmode;
        }
    }
    #endregion
}

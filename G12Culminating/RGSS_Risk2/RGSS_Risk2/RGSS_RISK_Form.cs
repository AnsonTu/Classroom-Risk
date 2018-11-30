/* Anson Tu
 * January 25, 2017
 * RGSS_RISK_FORM
 * The form in which the game takes place
 * Players take turns placing down students
 * and directing them to compete with other
 * classrooms
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGSS_Risk2
{
    public partial class RGSS_RISK_Form : Form
    {
        //World model
        WorldModel board;

        //The size of each tile in the board
        Size tileSize;

        //Store the names of the players
        string[] departmentNames;

        //Font for strings
        Font font = new Font("Times New Roman", 16.0f);

        public RGSS_RISK_Form(string[] departments)
        {
            InitializeComponent();
            //Create the board for the game
            board = new WorldModel();
            //Create the size for the tiles
            tileSize = new Size(board.TileSize, board.TileSize);
            //Store the array of department names as a global array
            departmentNames = new string[departments.Length];
            //Transfer the department names from the local array
            //to the global array
            for (int i = 0; i < departments.Length; i++)
            {
                departmentNames[i] = departments[i];
            }
        }

        //Store the values of each department
        //to use to determine whose turn it is
        int turnCompSci = (int)Department.ComputerScience;
        int turnEnglish = (int)Department.English;
        int turnScience = (int)Department.Science;
        int turnMath = (int)Department.Mathematics;
        int turnGeography = (int)Department.Geography;
        //Save whose turn it currently is
        int currentTurn;
        //Save the number of students the player can still place 
        //at the beginning of the turn
        int studentsLeft = 3;
        //Save the tile's department type, the X coordinate,
        //and the Y coordinate of the tile the cursor is currently over
        int tileType, X, Y;
        //Check if the player has entered the battle phase
        bool battleReady = false;
        //Check if the player is still in the student placement phase
        bool placeStudents = false;
        //Get the X and Y coordinates of the attacking tile
        //and the defending tile
        int xAttacker = 0, yAttacker = 0, xDefender = 0, yDefender = 0;
        //Create a bool array to keep track of which department heads
        //have been created to act as players
        bool[] playerCreated = new bool[5];

        /// <summary>
        /// Draw the map for the game
        /// </summary>
        private void DrawMap()
        {
            //Get the number of rows and columns from the game board
            int numRows = board.Board.GetLength(0);
            int numCols = board.Board.GetLength(1);
            Tile[,] map = board.Board;

            //Create backbuffer for double-buffered graphics
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            //Iterate through the rows
            for (int row = 0; row < numRows; row++)
            {
                //Iterate through the columns
                for (int col = 0; col < numCols; col++)
                {
                    //Create the size for the tiles
                    Rectangle tile = new Rectangle(col * tileSize.Width, row * tileSize.Height, tileSize.Width, tileSize.Height);
                    //Change the colour of the tiles depending on the tile's department type
                    switch (map[row, col].Department)
                    {
                        case Department.Void:
                            bg.Graphics.FillRectangle(Brushes.LightGray, tile);
                            break;
                        case Department.ComputerScience:
                            bg.Graphics.FillRectangle(Brushes.Green, tile);
                            break;
                        case Department.English:
                            bg.Graphics.FillRectangle(Brushes.Orange, tile);
                            break;
                        case Department.Science:
                            bg.Graphics.FillRectangle(Brushes.Red, tile);
                            break;
                        case Department.Mathematics:
                            bg.Graphics.FillRectangle(Brushes.SkyBlue, tile);
                            break;
                        case Department.Geography:
                            bg.Graphics.FillRectangle(Brushes.SandyBrown, tile);
                            break;
                    }
                    //turnPrompt: tells the user whose turn it is
                    //tileName: the tile department type,
                    //indicates which player it belongs to
                    //numberOfStudents: tells the user how many students are
                    //on the tile
                    string turnPrompt, tileName, numberOfStudents;
                    //Depending on whose turn it is, tell the user
                    if (currentTurn == turnCompSci)
                    {
                        turnPrompt = "It is Mr.Hsiung's turn (Department of Computer Science)";
                    }
                    else if (currentTurn == turnEnglish)
                    {
                        turnPrompt = "It is Ms.Cunningham's turn (Department of English)";
                    }
                    else if (currentTurn == turnScience)
                    {
                        turnPrompt = "It is Ms.Mocherla's turn (Department of Science)";
                    }
                    else if (currentTurn == turnMath)
                    {
                        turnPrompt = "It is Mr.Mohammadi's turn (Department of Mathematics)";
                    }
                    else
                    {
                        turnPrompt = "It is Mr.Morris' turn (Department of Geography)";
                    }
                    //Indicate the tileType to the user
                    if (tileType == turnCompSci)
                    {
                        tileName = "Department: Computer Science";
                    }
                    else if (tileType == turnEnglish)
                    {
                        tileName = "Department: English";
                    }
                    else if (tileType == turnScience)
                    {
                        tileName = "Department: Science";
                    }
                    else if (tileType == turnMath)
                    {
                        tileName = "Department: Mathematics";
                    }
                    else if (tileType == turnGeography)
                    {
                        tileName = "Department: Geography";
                    }
                    else
                    {
                        tileName = "That's not a classroom";
                    }
                    //Change the cursor based on whose turn it is
                    ChangeCursor();
                    //Tell the user how many students are on the tile that the cursor is currently on
                    numberOfStudents = "Number of Students: " + board.Board[X, Y].Students.ToString();
                    //Draw the UI information
                    bg.Graphics.DrawString(turnPrompt, font, Brushes.White, 0, ClientSize.Height - 30);
                    bg.Graphics.DrawString(tileName, font, Brushes.White, 0, ClientSize.Height - 90);
                    bg.Graphics.DrawString(numberOfStudents, font, Brushes.White, 0, ClientSize.Height - 60);
                    //Draw the background
                    bg.Graphics.DrawRectangle(Pens.Black, tile);
                    //Check if a player has won
                    CheckForWinner();
                }
            }
            //Draw the graphics
            bg.Render();
            bg.Dispose();
        }

        /// <summary>
        /// Place students in each tile at the beginning of the game
        /// </summary>
        void InitialStudentPlacement()
        {
            //Get the rows and columns from the game board
            int numRows = board.Board.GetLength(0);
            int numCols = board.Board.GetLength(1);
            Tile[,] map = board.Board;
            //Iterate through the rows
            for (int row = 0; row < numRows; row++)
            {
                //Iterate through the columns
                for (int col = 0; col < numCols; col++)
                {
                    //Place students for all tiles that are classrooms
                    if (map[row, col].Department != Department.Void)
                    {
                        map[row, col].Students = 3;
                    }
                }
            }
        }

        /// <summary>
        /// Runs when the game's form first appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGSS_RISK_Form_Shown(object sender, EventArgs e)
        {
            //Determine the first player based on the 
            //departments that have been selected
            if (departmentNames[0] == "Computer Science")
            {
                currentTurn = turnCompSci;
            }
            else if (departmentNames[0] == "English")
            {
                currentTurn = turnEnglish;
            }
            else if (departmentNames[0] == "Science")
            {
                currentTurn = turnScience;
            }
            else if (departmentNames[0] == "Mathematics")
            {
                currentTurn = turnMath;
            }
            else if (departmentNames[0] == "Geography")
            {
                currentTurn = turnGeography;
            }
            //Give 3 students to each classroom tile
            InitialStudentPlacement();
            //Create the players
            CreatePlayers();
            //Create a random number generator to create random tiles
            Random tileGenerator = new Random();
            //Iterate through the rows
            for (int row = 0; row < 7; row++)
            {
                //Iterate through the columns
                for (int col = 0; col < 16; col++)
                {
                    //Run the random number generator
                    int t = tileGenerator.Next(0, 5);
                    //Check if the player has been created and the department issn't void
                    if (playerCreated[t] == true && board.Board[row, col].Department != Department.Void)
                    {
                        t++;
                        //Change the department
                        board.Board[row, col].Department = (Department)t;
                    }
                }
            }
            //Draw the graphics
            DrawMap();
        }

        /// <summary>
        /// Create the players of the game
        /// </summary>
        void CreatePlayers()
        {
            //Iterate through the array that contains all of the selected players
            for (int i = 0; i < departmentNames.Length; i++)
            {
                if (departmentNames[i] == "Computer Science")
                {
                    Player hsiung = new Player("Computer Science", Department.ComputerScience);
                    playerCreated[0] = true;
                }
                if (departmentNames[i] == "English")
                {
                    Player cunningham = new Player("English", Department.English);
                    playerCreated[1] = true;
                }
                if (departmentNames[i] == "Science")
                {
                    Player mocherla = new Player("Science", Department.Science);
                    playerCreated[2] = true;
                }
                if (departmentNames[i] == "Mathematics")
                {
                    Player mohammadi = new Player("Mathematics", Department.Mathematics);
                    playerCreated[3] = true;
                }
                if (departmentNames[i] == "Geography")
                {
                    Player morris = new Player("Geography", Department.Geography);
                    playerCreated[4] = true;
                }
            }
        }

        /// <summary>
        /// Move on to the next turn
        /// </summary>
        void ChangeTurns()
        {
            //Change turns based on which department heads have been 
            //created, and whose turn has just ended
            if (currentTurn == turnCompSci)
            {
                if (playerCreated[1] == true)
                {
                    currentTurn = turnEnglish;
                }
                else if (playerCreated[2] == true)
                {
                    currentTurn = turnScience;
                }
                else if (playerCreated[3] == true)
                {
                    currentTurn = turnMath;
                }
                else if (playerCreated[4] == true)
                {
                    currentTurn = turnGeography;
                }
            }
            else if (currentTurn == turnEnglish)
            {
                if (playerCreated[2] == true)
                {
                    currentTurn = turnScience;
                }
                else if (playerCreated[3] == true)
                {
                    currentTurn = turnMath;
                }
                else if (playerCreated[4] == true)
                {
                    currentTurn = turnGeography;
                }
                else if (playerCreated[0] == true)
                {
                    currentTurn = turnCompSci;
                }
            }
            else if (currentTurn == turnScience)
            {
                if (playerCreated[3] == true)
                {
                    currentTurn = turnMath;
                }
                else if (playerCreated[4] == true)
                {
                    currentTurn = turnGeography;
                }
                else if (playerCreated[0] == true)
                {
                    currentTurn = turnCompSci;
                }
                else if (playerCreated[1] == true)
                {
                    currentTurn = turnEnglish;
                }
            }
            else if (currentTurn == turnMath)
            {
                if (playerCreated[4] == true)
                {
                    currentTurn = turnGeography;
                }
                else if (playerCreated[0] == true)
                {
                    currentTurn = turnCompSci;
                }
                else if (playerCreated[1] == true)
                {
                    currentTurn = turnEnglish;
                }
                else if (playerCreated[2] == true)
                {
                    currentTurn = turnScience;
                }
            }
            else if (currentTurn == turnGeography)
            {
                if (playerCreated[0] == true)
                {
                    currentTurn = turnCompSci;
                }
                else if (playerCreated[1] == true)
                {
                    currentTurn = turnEnglish;
                }
                else if (playerCreated[2] == true)
                {
                    currentTurn = turnScience;
                }
                else if (playerCreated[3] == true)
                {
                    currentTurn = turnMath;
                }
            }
            //Battle phase has not started
            battleReady = false;
            //New students have not been placed
            placeStudents = false;
            //Reset number of new students that can be placed
            studentsLeft = 3;
        }

        /// <summary>
        /// Do something whenever the mouse is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The movement of the mouse.</param>
        private void RGSS_RISK_Form_MouseMove(object sender, MouseEventArgs e)
        {
            //Save the X and Y coordinates of the mouse, divided by the tileSize
            int xValue = e.Y / 65;
            int yValue = e.X / 65;
            //Check if the mouse is within the map
            if (yValue < 16 && xValue < 7)
            {
                //Check the tile department type 
                if (board.Board[xValue, yValue].Department == Department.ComputerScience)
                {
                    //Tile type is computer science
                    tileType = turnCompSci;
                }
                else if (board.Board[xValue, yValue].Department == Department.English)
                {
                    //Tile type is English
                    tileType = turnEnglish;
                }
                else if (board.Board[xValue, yValue].Department == Department.Science)
                {
                    //Tile type is science
                    tileType = turnScience;
                }
                else if (board.Board[xValue, yValue].Department == Department.Mathematics)
                {
                    //Tile type is math
                    tileType = turnMath;
                }
                else if (board.Board[xValue, yValue].Department == Department.Geography)
                {
                    //Tile type is geography
                    tileType = turnGeography;
                }
                else if (board.Board[xValue, yValue].Department == Department.Void)
                {
                    //Tile is not a classroom
                    tileType = (int)Department.Void;
                }
                //Save the X and Y coordinates of the mouse
                X = xValue;
                Y = yValue;
            }
            //Refresh the graphics
            DrawMap();
        }

        /// <summary>
        /// Take the user's keyboard input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGSS_RISK_Form_KeyDown(object sender, KeyEventArgs e)
        {
            //To move onto the next turn, press the spacebar.
            if (e.KeyCode == Keys.Space)
            {
                ChangeTurns();
                DrawMap();
            }
            //To quit the game, press the Q key.
            if (e.KeyCode == Keys.Q)
            {
                Application.Exit();
            }
            //To unselect the attacking tile, press the Z key.
            if (e.KeyCode == Keys.Z)
            {
                battleReady = false;
                DrawMap();
            }
        }

        /// <summary>
        /// Do something whenever the user clicks something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGSS_RISK_Form_MouseClick(object sender, MouseEventArgs e)
        {
            //Check if the battle phase has been initiated
            if (battleReady == false)
            {
                //Students have been placed, and the selected tile has more than 1 student
                if (placeStudents == true && board.Board[X, Y].Students > 1)
                {
                    //If the tile that was selected is the same department
                    //as the department head's turn, begin the battle phase
                    if (currentTurn == turnCompSci)
                    {
                        if (board.Board[X, Y].Department == Department.ComputerScience)
                        {
                            battleReady = true;
                            Cursor = cursorSelectAttack;
                        }
                    }
                    else if (currentTurn == turnEnglish)
                    {
                        if (board.Board[X, Y].Department == Department.English)
                        {
                            battleReady = true;
                            Cursor = cursorSelectAttack;
                        }
                    }
                    else if (currentTurn == turnScience)
                    {
                        if (board.Board[X, Y].Department == Department.Science)
                        {
                            battleReady = true;
                            Cursor = cursorSelectAttack;
                        }
                    }
                    else if (currentTurn == turnMath)
                    {
                        if (board.Board[X, Y].Department == Department.Mathematics)
                        {
                            battleReady = true;
                            Cursor = cursorSelectAttack;
                        }
                    }
                    else if (currentTurn == turnGeography)
                    {
                        if (board.Board[X, Y].Department == Department.Geography)
                        {
                            battleReady = true;
                            Cursor = cursorSelectAttack;
                        }
                    }
                    //Save the value of the tile as the coordinates of the attacker
                    xAttacker = X;
                    yAttacker = Y;
                }
                else
                {
                    //Not all new students have been placed
                    if (studentsLeft > 0)
                    {
                        //If the tile selected is the same department as the current department 
                        //head's turn, add a student to that tile, and subtract 1 from the 
                        //new students remaining
                        if (currentTurn == turnCompSci)
                        {
                            if (board.Board[X, Y].Department == Department.ComputerScience)
                            {
                                board.Board[X, Y].Students += 1;
                                studentsLeft--;
                            }
                        }
                        else if (currentTurn == turnEnglish)
                        {
                            if (board.Board[X, Y].Department == Department.English)
                            {
                                board.Board[X, Y].Students += 1;
                                studentsLeft--;
                            }
                        }
                        else if (currentTurn == turnScience)
                        {
                            if (board.Board[X, Y].Department == Department.Science)
                            {
                                board.Board[X, Y].Students += 1;
                                studentsLeft--;
                            }
                        }
                        else if (currentTurn == turnMath)
                        {
                            if (board.Board[X, Y].Department == Department.Mathematics)
                            {
                                board.Board[X, Y].Students += 1;
                                studentsLeft--;
                            }
                        }
                        else if (currentTurn == turnGeography)
                        {
                            if (board.Board[X, Y].Department == Department.Geography)
                            {
                                board.Board[X, Y].Students += 1;
                                studentsLeft--;
                            }
                        }
                        //If all new students have been placed, the initial placement
                        //phase is over
                        if (studentsLeft == 0)
                        {
                            placeStudents = true;
                        }
                    }
                }
            }
            //Check if the battle phase has been initiated.
            if (battleReady == true)
            {
                //Check that the tile selected is within the map, and that the tile has more than 1 student
                if (X < 7 && Y < 16 && xAttacker < 7 && yAttacker < 16 && board.Board[xAttacker, yAttacker].Students > 1)
                {
                    //Check if the attacking tile's X coordinate is 0
                    if (xAttacker == 0 && yAttacker != 0)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker + 1, yAttacker]
                            || board.Board[X, Y] == board.Board[xAttacker, yAttacker - 1] || board.Board[X, Y] == board.Board[xAttacker, yAttacker + 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //Check if the attacking tile's Y coordinate is 0
                    else if (yAttacker == 0 && xAttacker != 0)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker - 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker + 1, yAttacker]
                            || board.Board[X, Y] == board.Board[xAttacker, yAttacker + 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //Check if the attacking tile's X and Y coordinates are 0
                    else if (xAttacker == 0 && yAttacker == 0)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker + 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker, yAttacker + 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //Check if the attacking tile's X coordinate is at the limit of the map
                    else if (xAttacker >= 6)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker - 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker, yAttacker - 1]
                            || board.Board[X, Y] == board.Board[xAttacker, yAttacker + 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //Check if the attacking tile's Y coordinate is at the limit of the map
                    else if (yAttacker >= 15)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker - 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker + 1, yAttacker]
                            || board.Board[X, Y] == board.Board[xAttacker, yAttacker - 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //Check if the attacking tile's X and Y coordinates are at the limit of the map
                    else if (yAttacker >= 15)
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker - 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker, yAttacker - 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                    //The attacking tile is in the middle of the map somewhere
                    else
                    {
                        //Check all possible adjacent tiles
                        if (board.Board[X, Y] == board.Board[xAttacker - 1, yAttacker] || board.Board[X, Y] == board.Board[xAttacker + 1, yAttacker]
                            || board.Board[X, Y] == board.Board[xAttacker, yAttacker - 1] || board.Board[X, Y] == board.Board[xAttacker, yAttacker + 1])
                        {
                            //Get the defender's X and Y coordinates
                            xDefender = X;
                            yDefender = Y;
                            //Check if the defending tile's department is different, and is actually a classroom
                            if (board.Board[xAttacker, yAttacker].Department != board.Board[xDefender, yDefender].Department
                                && board.Board[xDefender, yDefender].Department != Department.Void)
                            {
                                //Create the battle form and end the battle phase
                                BattleForm frmBattleForm = new BattleForm(board.Board[xAttacker, yAttacker], board.Board[xDefender, yDefender]);
                                frmBattleForm.Show();
                                battleReady = false;
                            }
                        }
                    }
                }
            }
        }

        //Make a new cursor for each different department.
        Cursor cursorCompSci = new Cursor(Properties.Resources.CompSciCursor.GetHicon());
        Cursor cursorEnglish = new Cursor(Properties.Resources.EnglishCursor.GetHicon());
        Cursor cursorScience = new Cursor(Properties.Resources.ScienceCursor.GetHicon());
        Cursor cursorMath = new Cursor(Properties.Resources.MathCursor.GetHicon());
        Cursor cursorGeography = new Cursor(Properties.Resources.GeographyCursor.GetHicon());
        //Create a new cursor for the different options that the user can do.
        Cursor cursorSelectAttack = new Cursor(Properties.Resources.SwordCursor.GetHicon());

        /// <summary>
        /// Change the cursor based on whose turn it is.
        /// </summary>
        void ChangeCursor()
        {
            //Check if the battle phase has not been initiated
            if (battleReady == false)
            {
                //The cursor depends on whose turn it is
                if (currentTurn == turnCompSci)
                {
                    Cursor = cursorCompSci;
                }
                else if (currentTurn == turnEnglish)
                {
                    Cursor = cursorEnglish;
                }
                else if (currentTurn == turnScience)
                {
                    Cursor = cursorScience;
                }
                else if (currentTurn == turnMath)
                {
                    Cursor = cursorMath;
                }
                else if (currentTurn == turnGeography)
                {
                    Cursor = cursorGeography;
                }
            }
        }

        /// <summary>
        /// Check for who has won the game
        /// </summary>
        void CheckForWinner()
        {
            //Get the number of rows of columns for the game board
            int numRows = 7;
            int numCols = 16;
            //Create a counter to save the number of classrooms
            int numberOfNonVoidTiles = 0;
            Tile[,] map = board.Board;
            //Create an array to store all of the possible departments, and their total number of classrooms
            int[] departments = new int[5];
            //Store the winner
            string winner;

            //Iterate through the rows
            for (int row = 0; row < numRows; row++)
            {
                //Iterate through the columns
                for (int col = 0; col < numCols; col++)
                {
                    //For every classroom, add 1 to the counter
                    if (map[row, col].Department != Department.Void)
                    {
                        numberOfNonVoidTiles++;
                    }
                }
            }

            //Iterate through the rows
            for (int row = 0; row < numRows; row++)
            {
                //Iterate through the columns
                for (int col = 0; col < numCols; col++)
                {
                    //For every classroom that corresponds to its department, add one
                    //to that department element
                    if (map[row, col].Department == Department.ComputerScience)
                    {
                        departments[0]++;
                    }
                    else if (map[row, col].Department == Department.English)
                    {
                        departments[1]++;
                    }
                    else if (map[row, col].Department == Department.Science)
                    {
                        departments[2]++;
                    }
                    else if (map[row, col].Department == Department.Mathematics)
                    {
                        departments[3]++;
                    }
                    else if (map[row, col].Department == Department.Geography)
                    {
                        departments[4]++;
                    }
                }
            }

            //The winner will have all of the possible classrooms
            //Restart the game
            if (departments[0] == numberOfNonVoidTiles)
            {
                winner = "Computer Science";
                Hide();
                PlayerCreationForm frmPCF = new PlayerCreationForm();
                frmPCF.Show();
                MessageBox.Show(winner);
            }
            else if (departments[1] == numberOfNonVoidTiles)
            {
                winner = "English";
                Hide();
                MessageBox.Show(winner);
                PlayerCreationForm frmPCF = new PlayerCreationForm();
                frmPCF.Show();
            }
            else if (departments[2] == numberOfNonVoidTiles)
            {
                winner = "Science";
                Hide();
                MessageBox.Show(winner);
                PlayerCreationForm frmPCF = new PlayerCreationForm();
                frmPCF.Show();
            }
            else if (departments[3] == numberOfNonVoidTiles)
            {
                winner = "Mathematics";
                Hide();
                MessageBox.Show(winner);
                PlayerCreationForm frmPCF = new PlayerCreationForm();
                frmPCF.Show();
            }
            else if (departments[4] == numberOfNonVoidTiles)
            {
                winner = "Geography";
                Hide();
                MessageBox.Show(winner);
                PlayerCreationForm frmPCF = new PlayerCreationForm();
                frmPCF.Show();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grid
{
    public partial class Form1 : Form
    {
        // string used to store the name of the text file that will store highscores
        string highScoresFilePath = "highscores.txt";

        // boolean isBlackTurn to keep track of what players turn it is
        bool isBlackTurn = true;

        // Button array to initialise the board width and height
        Button[,] button = new Button[8, 8];

        // label to store the number of tiles for each player
        Label playerInfo;

        // Title
        Label Title;

        //Black score and white score initialised at 2 for the beginning of the game
        int blackScore = 2;
        int whiteScore = 2;
        int[] highScores = new int[5];

        // String to store what players turn it is
        string playerTurn = "Black";

        // Winner string for the winner
        string winner;

        // Panel used to place behind the grid to give the impression of the board
        private Panel boardBackgroundPanel;

        //Button to forefit turn
        Button forefitTurn = new Button();

        //Button to play again
        Button playAgain = new Button();

        //Button to close the game
        Button close = new Button();

        //High scores label
        Label highScoresLabel = new Label();

        public Form1()
        {
            InitializeComponent();

            //Initialisation of the highscore label
            highScoresLabel.AutoSize = true;
            highScoresLabel.Location = new Point(650, 450);
            highScoresLabel.ForeColor = Color.White;
            highScoresLabel.Font = new Font("Arial", 15, FontStyle.Bold);
            Controls.Add(highScoresLabel);
            LoadHighScores();

            //space set from the board to the top of the form
            int initialTop = 150;

            // Title Initialisation
            Title = new Label();
            Title.ForeColor = Color.White;
            Title.Text = "Othello";
            Title.Font = new Font("Arial", 35, FontStyle.Bold);
            Title.SetBounds(50, 50, 200, 100);
            Controls.Add(Title);

            // Initialisation of forefit button
            forefitTurn = new Button();
            forefitTurn.FlatStyle = FlatStyle.Flat;
            forefitTurn.FlatAppearance.BorderColor = Color.Gray;
            forefitTurn.SetBounds(650, 350, 180, 60); 
            forefitTurn.Text = "Forefit turn";
            forefitTurn.Font = new Font("Arial", 15, FontStyle.Bold);
            forefitTurn.Click += new EventHandler(this.buttonEvent_Click);
            forefitTurn.BackColor = Color.White;
            Controls.Add(forefitTurn);

            // Initialisation of play again button
            playAgain = new Button();
            playAgain.FlatStyle = FlatStyle.Flat;
            playAgain.FlatAppearance.BorderColor = Color.Gray;
            playAgain.SetBounds(650, 350, 180, 60);
            playAgain.Text = "Play again?";
            playAgain.Font = new Font("Arial", 15, FontStyle.Bold);
            playAgain.Click += new EventHandler(this.buttonEvent_Click);
            playAgain.BackColor = Color.White;
            playAgain.Hide();
            Controls.Add(playAgain);

            // Initialisation of close button
            close = new Button();
            close.FlatStyle = FlatStyle.Flat;
            close.FlatAppearance.BorderColor = Color.Gray;
            close.SetBounds(650, 450, 180, 60);
            close.Text = "Close game?";
            close.Font = new Font("Arial", 15, FontStyle.Bold);
            close.Click += new EventHandler(this.buttonEvent_Click);
            close.BackColor = Color.White;
            close.Hide();
            Controls.Add(close);

            // Initialisation of the score label
            playerInfo = new Label();
            playerInfo.ForeColor = Color.White;
            playerInfo.Text = "Black tiles: " + blackScore + "\n" + "\nWhite tiles: " + whiteScore + "\n" + "\nPlayer turn: " + playerTurn;
            playerInfo.Font = new Font("Arial", 15, FontStyle.Bold);
            playerInfo.SetBounds(650, 200, 600, 600);
            Controls.Add(playerInfo);



            /* Panel set up to sit behind the grid to act as a board for all the squares */

            boardBackgroundPanel = new Panel();
            boardBackgroundPanel.SetBounds(55, initialTop, 450, 450); 
            boardBackgroundPanel.BackColor = Color.FromArgb(255, 87, 48, 21);
            Controls.Add(boardBackgroundPanel);

            /* For loop to draw the board, set the colour of the squares on the board, 
             * set spacing of buttons on the board, size of the buttons on the board,
             * set writing on the buttons */

            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                    button[i, j] = new Button();
                    button[i, j].FlatStyle = FlatStyle.Flat;
                    button[i, j].SetBounds(25 + (50 * i), 25 + (50 * j), 50, 50);
                    button[i, j].FlatAppearance.BorderColor = Color.Gray;
                    button[i, j].BackColor = Color.Green;
                    button[i, j].ForeColor = Color.Green;
                    button[i, j].Text = Convert.ToString((i) + "," + (j));
                    button[i, j].Click += new EventHandler(this.buttonEvent_Click);
                    boardBackgroundPanel.Controls.Add(button[i, j]);
                }
            }

            //four buttons used to set the centeral starting squares to begin the game

            button[4, 4].BackColor = Color.Black;
            button[4, 4].ForeColor = Color.Black;
           // button[4, 4].BackgroundImage = global::grid.Properties.Resources.;

            button[4, 3].BackColor = Color.White;
            button[4, 3].ForeColor = Color.White;

            button[3, 3].BackColor = Color.Black;
            button[3, 3].ForeColor = Color.Black;

            button[3, 4].BackColor = Color.White;
            button[3, 4].ForeColor = Color.White;
        }

        /* method that begins the game */

        void buttonEvent_Click(object sender, EventArgs e)
        {
            /*
             if and else if statements to handle if a turn is forefitted by checking if a turn is either black or white
             and by checking if the forfiet button has been pressed. 
             */

            if (sender == forefitTurn && isBlackTurn)
            {
                Console.WriteLine("Turn forfeited!");
                isBlackTurn = false;
                playerTurn = "white";
                countTiles();
            }
            else if(sender == forefitTurn && !isBlackTurn)
            {
                Console.WriteLine("Turn forfeited!");
                isBlackTurn = true;
                playerTurn = "Black";
                countTiles();
            }

            /*
             Play again function
             */

            if (sender == playAgain)
            {
                playAgain.Hide();
                highScoresLabel.Show();
                close.Hide();
                resetBoard();               
            }

            //close button function
            if (sender == close)
            {
                Application.Exit();
            }

            //button variable to allow the users input on the board to be stored
            Button pressedButton = sender as Button;

            /* if statement to stop the user being able to place tiles on buttons 
             * that are either black or white */

            if (pressedButton.BackColor != Color.Green)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            /* integer x, y and a string called colour to store the x and 
               y coordinate and the colour to be extracted from the button
               pressed */

            int x;
            int y;
            string colour;

            if (isBlackTurn)
            {
                
                Console.WriteLine(((Button)sender).Text);

                // sets the colour of the button to black when the user who is black presses the button 

                pressedButton.BackColor = Color.Black;

                /* code to allow the x and y coordinates to be extracted 
                 * from the string containing them */

                x = int.Parse(pressedButton.Text[0].ToString());
                y = int.Parse(pressedButton.Text[2].ToString());

                /* Colour extracted from the buttons
                 * back colour and stored into a string to be passed
                 * into the search method in order to tell whos turn 
                 * it is */

                colour = pressedButton.BackColor.ToString();
                pressedButton.ForeColor = Color.Black;

                //up search
                bool validMove = search(x, y, 0, -1, colour);
                //down search
                validMove |= search(x, y, 0, 1, colour);
                //right search
                validMove |= search(x, y, 1, 0, colour);
                //left search
                validMove |= search(x, y, -1, 0, colour);
                //Top right search 
                validMove |= search(x, y, 1, -1, colour);
                //bottom left search
                validMove |= search(x, y, -1, 1, colour);
                //bottom right search
                validMove |= search(x, y, 1, 1, colour);
                //top left search
                validMove |= search(x, y, -1, -1, colour);

                

                /* if statement that ensures that if an invalid move is
                   selected by the user then the tile they clicked on is 
                   reverted to its original colour */

                if (!validMove)
                {
                    pressedButton.BackColor = Color.Green;
                    pressedButton.ForeColor = Color.Green;
                    return;
                }

                playerTurn = "White";
                countTiles();
                // at the end of blacks turn the booloean isBlackTurn is set to false in order to let it progress to whites turn

                isBlackTurn = false;
            }
            else
            {
                
                Console.WriteLine(((Button)sender).Text);

                // sets the colour of the button to white when the user who is black presses the button 

                pressedButton.BackColor = Color.White;

                /* code to allow the x and y coordinates to be extracted 
                 * from the string containing them */

                x = int.Parse(pressedButton.Text[0].ToString());
                y = int.Parse(pressedButton.Text[2].ToString());

                /* Colour extracted from the buttons
                 * back colour and stored into a string to be passed
                 * into the search method in order to tell whos turn 
                 * it is */

                colour = pressedButton.BackColor.ToString();
                pressedButton.ForeColor = Color.White;

                //up search
                bool validMove = search(x, y, 0, -1, colour);
                //down search
                validMove |= search(x, y, 0, 1, colour);
                //right search
                validMove |= search(x, y, 1, 0, colour);
                //left search
                validMove |= search(x, y, -1, 0, colour);
                //Top right search 
                validMove |= search(x, y, 1, -1, colour);
                //bottom left search
                validMove |= search(x, y, -1, 1, colour);
                //bottom right search
                validMove |= search(x, y, 1, 1, colour);
                //top left search
                validMove |= search(x, y, -1, -1, colour);

                /* if statement that ensures that if an invalid move is
                   selected by the user then the tile they clicked on is 
                   reverted to its original colour */

                if (!validMove)
                {
                    pressedButton.BackColor = Color.Green;
                    pressedButton.ForeColor = Color.Green;
                    return;
                }

                playerTurn = "Black";
                countTiles();
                // at the end of blacks turn the booloean isBlackTurn is set to true in order to let it progress to Blacks turn

                isBlackTurn = true;
            }
        }

        /* Search method that allows the board to be searched in all directions that there could be a valid move. It takes input of 
           an x and y coordinate, deltaX, deltaY and finally a colour. The x and y coordinate are self explanitory and take the x and 
           y coordinates of the button pressed. The deltaX and deltaY variables are used to determine the direction that the search method
           traverses through the tiles in */

        public bool search(int x, int y, int deltaX, int deltaY, string colour)
        {
            // boolean used to determine if a tile of the same colour has been found from where a tile has been placed

            bool foundSameColor = false;

            /* Integers foundX and foundY are created and initialised at the values of the x and y coordinates passed into the 
               functions. They will then be incremented by the deltaX and deltaY, which is how the game searches across the 
               board */

            int foundX = x;
            int foundY = y;

            while (true)
            {
                /* incrementatioin of foundX and foundY by deltaX and deltaY */

                foundX += deltaX;
                foundY += deltaY;

                /* if statement that allows the game to tell if the edge of the board is reached
                 * in which case the same colour has not been found and so found same colour is 
                   set to false, and is returned to the valid move boolean from before.*/

                if (foundX < 0 || foundY < 0 || foundX >= 8 || foundY >= 8)
                {
                    foundSameColor = false;
                    return foundSameColor;
                }

                /* New button called currentButton thats used to keep track of the current button 
                   as the board is being traverssed. This is then followed by an if statement that
                   works under the condition that if the button at foundX and foundY is equal to Green
                   then the user has given an invalid input. there then is an else if staement that works
                   by checking if the current button is then equal to the colour initially passed into the method.
                   If this is the case then found same colour then found same colour is set to true. */

                Button currentButton = button[foundX, foundY];

                if (button[foundX, foundY].BackColor == Color.Green)
                {
                    Console.WriteLine("Invalid input");
                    foundSameColor = false;
                    return foundSameColor;
                }
                else if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine($"Found at: X{foundX} Y:{foundY}");
                    break;
                }
            }

            //innt counter used to count the space between a tile placed and a tile of the same colour

            int count = 0;

            /* while loop set up so that while the x or y coordinates passed into the method are not equal
             * to foundX and foundY then the x and y coordinates are incremented by deltaX and deltaY. Within
             * this while loop is an if statement that states that ifBlackTurn is true then the backcolour of the 
               button is changed to black and the same is done for the else if staement except ifBlackTurn is
               set to false and the backcolour is then set to white.*/

            while (x != foundX || y != foundY)
            {
                x += deltaX;
                y += deltaY;

                Console.WriteLine($"replacing at: X{x} Y:{y}");


                if (isBlackTurn)
                {
                    button[x, y].BackColor = Color.Black;
                    button[x, y].ForeColor = Color.Black;
                    count++;
                    Console.WriteLine("Changed color at " + x + "," + y + " to " + button[x, y].BackColor);
                }

                else if (!isBlackTurn)
                {
                    button[x, y].BackColor = Color.White;
                    button[x, y].ForeColor = Color.White;
                    count++;
                    Console.WriteLine("Changed color at " + x + "," + y + " to " + button[x, y].BackColor);
                }
            }

            //if statement that returns false if a button of the same colour has been found next to one placed

            if (count == 1)
            {
                Console.WriteLine("Invalid input");
                return false;
            }

            return foundSameColor;
        }

        /*
         count tiles method to cound the total number 
         of tiles on the board after each turn
         */
        public void countTiles()
        {

            /*Setting black and white scores to 0 at the start of each call of the mehtod so 
             that all the tiles can be counted on the board and added correctly
             */

            blackScore = 0;
            whiteScore = 0;
            int foundGreen = 0;
            /*
             Nested for loop that itterates through the board and checks all the tiles 
             on the board. If the tile is green it ignores anything related to the scores,
             if the tiles are either black or white it increments the appropriate score
             */

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (button[x, y].BackColor == Color.Green)
                    {
                        foundGreen++;
                        continue;
                    }
                    else if (button[x, y].BackColor == Color.Black)
                    {
                        blackScore++;
                    }
                    else if (button[x, y].BackColor == Color.White)
                    {
                        whiteScore++;
                    }
                    
                }
            }

            //if statement to check and see if there are no more green tiles on the board, in which case the game is over and the gameOver function is called

            if (foundGreen == 0)
            {
                gameOver();
                return;
            }

            //updating the label with the new score

            playerInfo.Text = "Black tiles: " + blackScore + "\n" + "\nWhite tiles: " + whiteScore + "\n" + "\nPlayer turn: " + playerTurn;
        }

        /*
         Game over function that processes when a game is finished, it does this by using if statements to see what colour of tile had a higher score at
         the end of the board being cleared. After doing this it then displays the winner to the player info label and shows the winners score also. The 
         UpdateHighScore method is called and the winning score is passed as a parameter. At the end of the method there is also a for loop that iterates 
         through the current high scores stored and stores the score if applicable and also allows for the list to be rearranged if a new highscore is set
         */

        public void gameOver()
        {
            int winningScore = 0;

            if (blackScore > whiteScore)
            {
                winner = "Black wins!";
                Console.WriteLine("Black wins!");
                playerInfo.Font = new Font("Arial", 20, FontStyle.Bold);
                playerInfo.Text = winner + "\n" + "score: " + blackScore;
                winningScore = blackScore;
            }
            else if(whiteScore > blackScore)
            {
                winner = "White wins!";
                Console.WriteLine("White wins!");
                playerInfo.Font = new Font("Arial", 20, FontStyle.Bold);
                playerInfo.Text = winner + "\n" + "score: " + whiteScore;
                winningScore = whiteScore;
            }

            UpdateHighScores(winningScore);

            forefitTurn.Hide();
            highScoresLabel.Hide();
            playAgain.Show();
            close.Show();

            /*
             for loop to update the highscores when a new highscore 
             */

            for (int i = 0; i < highScores.Length; i++)
            {
                if (winningScore > highScores[i])
                {
                    for (int j = highScores.Length - 1; j > i; j--)
                    {
                        highScores[j] = highScores[j - 1];
                    }
                    highScores[i] = winningScore;
                    break;
                }
            }
            saveHighScores();
        }

        private void UpdateHighScores(int winningScore)
        {
            // Check if the winning score is higher than any of the high scores
            if (isBlackTurn && winningScore > highScores[0])
            {
                // Shift the existing high scores to make room for the new one
                for (int j = highScores.Length - 1; j > 0; j--)
                {
                    highScores[j] = highScores[j - 1];
                }

                // Insert the new score at the appropriate position
                highScores[0] = winningScore;

                // Save high scores after updating them
                saveHighScores();
            }
        }

        /*
         Save High Score method to allow the highscore to be saved into a text file. This is done using Streamwriter
         and searching through the array of highScores in order to store every element to a text file
         */

        private void saveHighScores()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(highScoresFilePath))
                {
                    foreach (int score in highScores)
                    {
                        writer.WriteLine(score);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving high scores: {ex.Message}");
            }
        }

        /* 
         LoadHighScores method used to load the high scores from the text file. This is done by first checking if the file that is to be searched
         exists, if it doesnt exist an error message is then written to the console window. If the file does exist then a string array called lines is
         used to store the lines stored in the highScoresFilePath. A for loop is then used to iterate through lines and highScores, at each point of 
         this iteration the lines at i are then changed to strings and passed into the highscores array at i.
         */

        private void LoadHighScores()
        {
            try
            {
                if (File.Exists(highScoresFilePath))
                {
                    string[] lines = File.ReadAllLines(highScoresFilePath);

                    for (int i = 0; i < lines.Length && i < highScores.Length; i++)
                    {
                        if (int.TryParse(lines[i], out int score))
                        {
                            highScores[i] = score;
                        }
                    }

                    //for loop to iterate through the highscore array and print them onto the highscore label text

                    highScoresLabel.Text = "High Scores:\n";
                    for (int i = 0; i < highScores.Length; i++)
                    {
                        highScoresLabel.Text += $"{i + 1}. {highScores[i]}\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading high scores: {ex.Message}");
            }
        }

        /*
         resetBoard method used to reset the board if the user decides they want to play again. The mehtod simply
         resets all the black and white squares to green and hides the plat again button. It also resets the scores
         and the turn back to being the black players turn
         */

        public void resetBoard()
        {
            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                    button[i, j].BackColor = Color.Green;
                    button[i, j].ForeColor = Color.Green;
                }
            }

            button[4, 4].BackColor = Color.Black;
            button[4, 4].ForeColor = Color.Black;

            button[4, 3].BackColor = Color.White;
            button[4, 3].ForeColor = Color.White;

            button[3, 3].BackColor = Color.Black;
            button[3, 3].ForeColor = Color.Black;

            button[3, 4].BackColor = Color.White;
            button[3, 4].ForeColor = Color.White;

            isBlackTurn = true;
            playerTurn = "Black";
            blackScore = 2;
            whiteScore = 2;

            playerInfo.Font = new Font("Arial", 15, FontStyle.Bold);
            playerInfo.Text = "Black tiles: " + blackScore + "\n" + "\nWhite tiles: " + whiteScore + "\n" + "\nPlayer turn: " + playerTurn;

            playAgain.Hide();
            forefitTurn.Show();
            LoadHighScores();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
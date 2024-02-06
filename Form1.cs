using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grid
{
    public partial class Form1 : Form
    {
        // boolean isBlackTurn to keep track of what players turn it is
        bool isBlackTurn = true;

        // Button array to initialise the board width and height
        Button[,] button = new Button[8, 8];

        Label scoreLabel;

        int blackScore = 2;
        int whiteScore = 2;

        public Form1()
        {
            InitializeComponent();

            scoreLabel = new Label();

            scoreLabel.Text = "Black tiles: " + blackScore + "\nWhite tiles: " + whiteScore;
            scoreLabel.SetBounds(550, 75, 500, 1000);
            scoreLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            Controls.Add(scoreLabel);

            /* For loop to draw the board, set the colour of the squares on the board, 
             * set spacing of buttons on the board, size of the buttons on the board,
             * set writing on the buttons */

            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                    button[i, j] = new Button();
                    button[i, j].SetBounds(55 + (55 * i), 55 + (55 * j), 45, 45);
                    button[i, j].BackColor = Color.Green;
                    button[i, j].Text = Convert.ToString((i) + "," + (j));
                    button[i, j].Click += new EventHandler(this.buttonEvent_Click);
                    Controls.Add(button[i, j]);
                }
            }

            //four buttons used to set the centeral starting squares to begin the game

            button[4, 4].BackColor = Color.Black;
            button[4, 3].BackColor = Color.White;
            button[3, 3].BackColor = Color.Black;
            button[3, 4].BackColor = Color.White;
        }

        /* method that begins the game */

        void buttonEvent_Click(object sender, EventArgs e)
        {
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
                    return;
                }

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
                    return;
                }

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
                    count++;
                    Console.WriteLine("Changed color at " + x + "," + y + " to " + button[x, y].BackColor);
                }

                else if (!isBlackTurn)
                {
                    button[x, y].BackColor = Color.White;
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

        public void countTiles()
        {
            blackScore = 0;
            whiteScore = 0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (button[x, y].BackColor == Color.Green)
                    {
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

            scoreLabel.Text = "Black tiles: " + blackScore + "\nWhite tiles: " + whiteScore;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
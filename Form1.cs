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

        //ArrayList used to store colours when traversing the board, used mostly for troubleshooting
        ArrayList storedColours = new ArrayList();

        //Stores colours to add to the ArrayList
        string storedColour;

        public Form1()
        {
            InitializeComponent();

            
           /* For loop to draw the board, set the colour of the squares on the board, 
            * set spacing of buttons on the board, size of the buttons on the board,
            * set writing on the buttons */
             

            for (int i = 0; i < button.GetLength(0); i++)
            {
                for (int j = 0; j < button.GetLength(1); j++)
                {
                    button[i,j]= new Button();
                    button[i,j].SetBounds(55 + (55*i), 55 + (55 * j), 45, 45);
                    button[i, j].BackColor = Color.Green;
                    button[i, j].Text = Convert.ToString((i) + "," + (j));
                    button[i, j].Click += new EventHandler(this.buttonEvent_Click);
                    Controls.Add(button[i,j]);
                }
            }

            //four buttons used to set the centeral starting squares to begin the game

            button[4, 4].BackColor = Color.Black;
            button[4, 3].BackColor = Color.White;
            button[3, 3].BackColor = Color.Black;
            button[3, 4].BackColor = Color.White;
        }

        /* method that begins the game,  */

        void buttonEvent_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if(pressedButton.BackColor != Color.Green)
            {
                Console.WriteLine("Wrong tile chosen, you nasty man!");
                return;
            }

            string name = pressedButton.Name;

            int x;
            int y;
            string colour;

            if (isBlackTurn)
            {
                Console.WriteLine(((Button)sender).Text);
                pressedButton.BackColor = Color.Black;
                Console.WriteLine(name);

                x = int.Parse(pressedButton.Text[0].ToString());
                y = int.Parse(pressedButton.Text[2].ToString());
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

                storedColours.Clear();

                if(!validMove)
                {
                    pressedButton.BackColor = Color.Green;
                    return;
                }


                isBlackTurn = false;
            }
            else
            {
                Console.WriteLine(((Button)sender).Text);
                pressedButton.BackColor = Color.White;
                Console.WriteLine(pressedButton.Name);

                x = int.Parse(pressedButton.Text[0].ToString());
                y = int.Parse(pressedButton.Text[2].ToString());
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

                storedColours.Clear();

                if (!validMove)
                {
                    pressedButton.BackColor = Color.Green;
                    return;
                }

                isBlackTurn = true;
            }
        }

        public bool search(int x, int y, int deltaX, int deltaY, string colour)
        {
            bool foundSameColor = false;

            int foundX = x;
            int foundY = y;

            while (true)
            {
                foundX += deltaX;
                foundY += deltaY;

                if (foundX < 0 || foundY < 0 || foundX >= 8 || foundY >= 8)
                {
                    foundSameColor = false;
                    return foundSameColor;
                }

                Button currentButton = button[foundX, foundY];
                if (button[foundX, foundY].BackColor == Color.Green)
                {
                    Console.WriteLine("NO! ");
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

            while (x != foundX || y != foundY)
            {
                x += deltaX;
                y += deltaY;
                
                Console.WriteLine($"replacing at: X{x} Y:{y}");


                if (isBlackTurn)
                {
                    button[x, y].BackColor = Color.Black;
                    Console.WriteLine("Changed color at " + x + "," + y + " to " + button[x, y].BackColor);
                }

                else if (!isBlackTurn)
                {
                    button[x, y].BackColor = Color.White;
                    Console.WriteLine("Changed color at " + x + "," + y + " to " + button[x, y].BackColor);
                }
            }
            return foundSameColor;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
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

        bool isBlackTurn = true;
        Button[,] button = new Button[8, 8];
        ArrayList storedColours = new ArrayList();
        string storedColour;

        public Form1()
        {
            InitializeComponent();

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

            button[4,4].BackColor = Color.Black;
            button[4,3].BackColor = Color.White;
            button[3,3].BackColor = Color.Black;
            button[3,4].BackColor = Color.White;
        }

        void buttonEvent_Click(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

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

                searchUp(x, y, colour);
                searchDown(x, y, colour);
                searchRight(x, y, colour);
                searchLeft(x, y, colour);
                searchTopLeft(x, y, colour);
                searchTopRight(x, y, colour);
                searchBottomLeft(x, y, colour);
                searchBottomRight(x, y, colour);

                storedColours.Clear();

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

                searchUp(x, y, colour);
                searchDown(x, y, colour);
                searchRight(x, y, colour);
                searchLeft(x, y, colour);
                searchTopLeft(x, y, colour);  
                searchTopRight(x, y, colour);
                searchBottomLeft(x,y, colour);
                searchBottomRight(x,y, colour);

                storedColours.Clear();

                isBlackTurn = true;
            }
        }

        public void searchUp(int x, int y, string colour)
        {
            bool foundSameColor = false;

            //Vertical search to find etiher a tile of the same colour or the top of the board

            for (int i = y - 1; i >= 0; i--)
            {
                Button currentButton = button[x, i];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + x + "," + i + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int startPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int j = i + 1; j < startPOS; j++)
                    {
                        if (isBlackTurn)
                        {
                            button[x, j].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + x + "," + j + " to " + button[x, j].BackColor);
                        }

                        else if (!isBlackTurn)
                        {
                            button[x, j].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + x + "," + j + " to " + button[x, j].BackColor);
                        }

                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }

                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + x + "," + i + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Top edge at " + x + ", 0");
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchDown(int x, int y, string colour)
        {
            bool foundSameColor = false;

            //Vertical search to find etiher a tile of the same colour or the top of the board

            for (int i = y + 1; i < 8; i++)
            {
                Button currentButton = button[x, i];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + x + "," + i + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int startPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int j = i - 1; j > startPOS; j--)
                    {
                        if (isBlackTurn)
                        {
                            button[x, j].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + x + "," + j + " to " + button[x, j].BackColor);
                        }

                        else if (!isBlackTurn)
                        {
                            button[x, j].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + x + "," + j + " to " + button[x, j].BackColor);
                        }

                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }

                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + x + "," + i + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Top edge at " + x + ", 0");
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchRight(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the right edge of the board
            for (int i = x + 1; i < button.GetLength(0); i++)
            {
                Button currentButton = button[i, y];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + y + " " + currentButton.BackColor.ToString());
                
                    int startPOS = x;

                    // Loop to change the color of buttons between the current and target positions
                    for (int j = i - 1; j > startPOS; j--)
                    {
                        if (isBlackTurn)
                        {
                            button[j, y].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + j + "," + y + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[j, y].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + j + "," + y + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Right edge at " + (button.GetLength(0) - 1) + "," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchLeft(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the left edge of the board
            for (int i = x - 1; i >= 0; i--)
            {
                Button currentButton = button[i, y];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + y + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int startPOS = x;

                    // Loop to change the color of buttons between the current and target positions
                    for (int j = i + 1; j < startPOS; j++)
                    {
                        if (isBlackTurn)
                        {
                            button[j, y].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + j + "," + y + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[j, y].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + j + "," + y + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Left edge at 0," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchTopLeft(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the left edge of the board
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                Button currentButton = button[i, j];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + j + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int xStartPOS = x;
                    int yStartPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int k = i + 1, l = j+1; k < xStartPOS && l < yStartPOS; k++, l++)
                    {
                        if (isBlackTurn)
                        {
                            button[k, l].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[k, l].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Left edge at 0," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchTopRight(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the left edge of the board
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                Button currentButton = button[i, j];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + j + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int xStartPOS = x;
                    int yStartPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int k = i - 1, l = j + 1; k > xStartPOS && l < yStartPOS; k--, l++)
                    {
                        if (isBlackTurn)
                        {
                            button[k, l].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[k, l].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Left edge at 0," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchBottomLeft(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the left edge of the board
            for (int i = x - 1, j = y + 1; i >= 0 && j < 8 ; i--, j++)
            {
                Button currentButton = button[i, j];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + j + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int xStartPOS = x;
                    int yStartPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int k = i + 1, l = j - 1; k < xStartPOS && l > yStartPOS; k++, l--)
                    {
                        if (isBlackTurn)
                        {
                            button[k, l].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[k, l].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Left edge at 0," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

        public void searchBottomRight(int x, int y, string colour)
        {
            bool foundSameColor = false;

            // Horizontal search to find either a tile of the same colour or the left edge of the board
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
            {
                Button currentButton = button[i, j];

                if (currentButton.BackColor.ToString() == colour)
                {
                    foundSameColor = true;
                    Console.WriteLine("Tile of the same colour found at position " + i + "," + j + " " + currentButton.BackColor.ToString());

                    // Set startPOS before entering the loop
                    int xStartPOS = x;
                    int yStartPOS = y;

                    // Loop to change the color of buttons between the current and target positions
                    for (int k = i - 1, l = j - 1; k > xStartPOS && l > yStartPOS; k--, l--)
                    {
                        if (isBlackTurn)
                        {
                            button[k, l].BackColor = Color.Black;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                        else if (!isBlackTurn)
                        {
                            button[k, l].BackColor = Color.White;
                            Console.WriteLine("Changed color at " + k + "," + l + " to " + button[j, y].BackColor);
                        }
                    }

                    Console.WriteLine("Stored colors:");
                    foreach (var stored in storedColours)
                    {
                        Console.WriteLine(stored);
                    }
                    break;
                }
                else
                {
                    storedColour = currentButton.BackColor.ToString();
                    Console.WriteLine("Processing tile at " + i + "," + y + " " + storedColour);
                    storedColours.Add(storedColour);
                }
            }

            if (!foundSameColor)
            {
                Console.WriteLine("Left edge at 0," + y);
                Console.WriteLine("Stored colors:");
                foreach (var stored in storedColours)
                {
                    Console.WriteLine(stored);
                }
            }
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
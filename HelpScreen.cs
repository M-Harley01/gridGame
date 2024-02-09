using System;
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
    public partial class HelpScreen : Form
    {
        String help1 = "The objective of the game is to have the majority of the board displaying "
                       + " your chosen colour at the end of the game. At the beginning of the game "
                       + " there are 2 white and black squares on the board and black places their "
                       + " square first. A valid move is where you place a square across one of  "
                       + " the same colour but with tiles of the opposite players colours between "
                       + " the squares. An example can be seen below. Further examples of valid  "
                       + " moves can be seen further into this help form.";

        Button moreExamples;
        Button back;
        
        // Label and buttons set for the examples
        Label exampleTitle;
        Button[] firstExampleStart;
        Button[] firstExampleEnd;
        int length = 5;

        Label Title;

        Label helpText;
        public HelpScreen()
        {
            InitializeComponent();

            //next button set up
            back = new Button();
            back.Location = new System.Drawing.Point(400, 600);
            back.Size = new System.Drawing.Size(200, 50);
            back.FlatStyle = FlatStyle.Flat;
            back.FlatAppearance.BorderColor = Color.White;
            back.BackColor = Color.White;
            back.ForeColor = Color.Black;
            back.Text = "Back to menu";
            back.Font = new Font("Arial", 10, FontStyle.Bold);
            back.Click += new EventHandler(this.buttonEvent_Click);
            this.Controls.Add(back);

            //more examples button used to reveal more example moves 
            moreExamples = new Button();
            moreExamples.Location = new System.Drawing.Point(425, 500);
            moreExamples.Size = new System.Drawing.Size(150,50);
            moreExamples.FlatStyle = FlatStyle.Flat;
            moreExamples.FlatAppearance.BorderColor = Color.White;
            moreExamples.BackColor = Color.White;
            moreExamples.ForeColor = Color.Black;
            moreExamples.Text = "More examples";
            moreExamples.Font = new Font("Arial", 10, FontStyle.Bold);
            moreExamples.Click += new EventHandler(this.buttonEvent_Click);
            this.Controls.Add(moreExamples);

            firstExampleStart = new Button[length];
            firstExampleEnd = new Button[length];

            for(int i = 0; i< firstExampleStart.Length; i++)
            {
                firstExampleStart[i] = new Button();
                firstExampleStart[i].Location = new System.Drawing.Point(350 + i * 60, 350);
                firstExampleStart[i].Size = new System.Drawing.Size(50, 50);
                firstExampleStart[i].FlatStyle = FlatStyle.Flat;
                firstExampleStart[i].FlatAppearance.BorderColor = Color.Gray;

                this.Controls.Add(firstExampleStart[i]);
            }

            firstExampleStart[0].BackColor = Color.Black;
            firstExampleStart[1].BackColor = Color.White;
            firstExampleStart[2].BackColor = Color.White;
            firstExampleStart[3].BackColor = Color.White;
            firstExampleStart[4].BackColor = Color.Black;

            for (int i = 0; i < firstExampleEnd.Length; i++)
            {
                firstExampleEnd[i] = new Button();
                firstExampleEnd[i].Location = new System.Drawing.Point(350 + i * 60, 425);
                firstExampleEnd[i].Size = new System.Drawing.Size(50, 50);
                firstExampleEnd[i].FlatStyle = FlatStyle.Flat;
                firstExampleEnd[i].FlatAppearance.BorderColor = Color.Gray;
                firstExampleEnd[i].BackColor = Color.Black;

                this.Controls.Add(firstExampleEnd[i]);
            }

            // Example Title Initialisation
            exampleTitle = new Label();
            exampleTitle.ForeColor = Color.White;
            exampleTitle.Text = "Example 1";
            exampleTitle.Font = new Font("Arial", 15, FontStyle.Bold);
            exampleTitle.SetBounds(450, 300, 600, 300);
            Controls.Add(exampleTitle);

            //first help paragraph
            helpText = new Label();
            helpText.ForeColor = Color.White;
            helpText.Text = help1;
            helpText.Font = new Font("Arial", 12, FontStyle.Bold);
            helpText.SetBounds(200, 150, 600, 600);
            Controls.Add(helpText);

            // Title Initialisation
            Title = new Label();
            Title.ForeColor = Color.White;
            Title.Text = "Othello Rules";
            Title.Font = new Font("Arial", 35, FontStyle.Bold);
            Title.SetBounds(360, 50, 600, 300);
            Controls.Add(Title);
        }

        void buttonEvent_Click(object sender, EventArgs e)
        {

            //if statement to check if the sendeer is the button for more examples

            if(sender == moreExamples)
            {
                /*
                  Elements are hidden that are no longer needed if the more examples button
                  is pressed, and example boards are generated in order to give more in 
                  depth examples of valid moves.
                 */

                helpText.Hide();
                exampleTitle.Hide();
                moreExamples.Location = new System.Drawing.Point(300, 550);

                back.Location = new System.Drawing.Point(500, 550);

                Title.Text = "Further examples";
                Title.SetBounds(30, 30, 308, 50);
                Title.Font = new Font("Arial", 25, FontStyle.Bold);

                for (int i = 0; i < firstExampleStart.Length && i < firstExampleEnd.Length; i++)
                {
                    firstExampleStart[i].Hide();
                    firstExampleEnd[i].Hide();
                }
            }

            else if(sender == back)
            {
                GameStart gameStartForm = new GameStart();

                gameStartForm.Show();

                this.Hide();
            }
           


            //board set up similar to the one in form 1 to show the board before valid moves

            Button[,] beforeMove = new Button[8, 8];

            int buttonSize = 40;

            for (int i = 0; i <beforeMove.GetLength(0); i++)
            {
                for (int j = 0; j < beforeMove.GetLength(1); j++)
                {
                    beforeMove[i, j] = new Button();
                    beforeMove[i, j].FlatStyle = FlatStyle.Flat;
                    beforeMove[i, j].FlatAppearance.BorderColor = Color.Gray;
                            
                    int X = 100 + (buttonSize * i) + 10; 
                    int Y = 140 + (buttonSize * j) + 10;

                    beforeMove[i, j].SetBounds(X, Y, buttonSize, buttonSize);
                    beforeMove[i, j].BackColor = Color.Green;
                    beforeMove[i, j].ForeColor = Color.Green;
                    beforeMove[i, j].Click += new EventHandler(this.buttonEvent_Click);
                    Controls.Add(beforeMove[i, j]);
                }
            }

            beforeMove[2,6].BackColor = Color.White;
            beforeMove[2,6].ForeColor = Color.Black;
            beforeMove[2,5].BackColor = Color.Black;
            beforeMove[2,4].BackColor = Color.Black;
            beforeMove[2,3].BackColor = Color.Black;
            beforeMove[2,2].BackColor = Color.White;
            beforeMove[2,6].Font = new Font("Arial", 10, FontStyle.Bold);
            beforeMove[2, 6].Text = "p";

            beforeMove[3, 6].BackColor = Color.Black;
            beforeMove[3, 5].BackColor = Color.Black;
            beforeMove[3, 6].BackColor = Color.Black;
            beforeMove[4, 4].BackColor = Color.Black;
            beforeMove[5, 3].BackColor = Color.Black;
            beforeMove[6, 2].BackColor = Color.White;
            beforeMove[4, 6].BackColor = Color.White;

            Button[,] afterMove = new Button[8, 8];

            for (int i = 0; i < afterMove.GetLength(0); i++)
            {
                for (int j = 0; j < afterMove.GetLength(1); j++)
                {
                    afterMove[i, j] = new Button();
                    afterMove[i, j].FlatStyle = FlatStyle.Flat;
                    afterMove[i, j].FlatAppearance.BorderColor = Color.Gray;

                    int X = 500 + (buttonSize * i) + 10; 
                    int Y = 140 + (buttonSize * j) + 10;

                    afterMove[i, j].SetBounds(X, Y, buttonSize, buttonSize);
                    afterMove[i, j].BackColor = Color.Green;
                    afterMove[i, j].ForeColor = Color.Green;
                    afterMove[i, j].Click += new EventHandler(this.buttonEvent_Click);
                    Controls.Add(afterMove[i, j]);
                }
            }

            afterMove[2, 6].BackColor = Color.White;
            afterMove[2, 6].ForeColor = Color.Black;
            afterMove[2, 5].BackColor = Color.White;
            afterMove[2, 4].BackColor = Color.White;
            afterMove[2, 3].BackColor = Color.White;
            afterMove[2, 2].BackColor = Color.White;
            afterMove[2, 6].Font = new Font("Arial", 10, FontStyle.Bold);
            afterMove[2, 6].Text = "p";

            afterMove[3, 6].BackColor = Color.White;
            afterMove[3, 5].BackColor = Color.White;
            afterMove[3, 6].BackColor = Color.White;
            afterMove[4, 4].BackColor = Color.White;
            afterMove[5, 3].BackColor = Color.White;
            afterMove[6, 2].BackColor = Color.White;
            afterMove[4, 6].BackColor = Color.White;

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

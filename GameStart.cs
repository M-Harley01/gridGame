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
    public partial class GameStart : Form
    {
        public GameStart()
        {
            InitializeComponent();
        }

        private void LoadGame(object sender, EventArgs e)
        {
            Form1 gameWindow = new Form1();

            gameWindow.Show();
        }

        private void LoadHelp(object sender, EventArgs e)
        {
            HelpScreen helpWindow = new HelpScreen();

            helpWindow.Show();
        }
    }
}

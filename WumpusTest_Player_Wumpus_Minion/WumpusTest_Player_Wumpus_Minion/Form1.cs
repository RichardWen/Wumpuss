using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WumpusTest_Player_Wumpus_Minion
{
    public partial class Player_Wumpus_Minion_Form : Form
    {

        private Player _Player;
        private Wumpus _Wumpus;
        private Minion _Minion;

        
        public Player_Wumpus_Minion_Form()
        {
            InitializeComponent();
        }

        private void PlayerButton_Click(object sender, EventArgs e)
        {
            _Player = new Player(0, 0, 0, 0);
        }

        private void WumpusButton_Click(object sender, EventArgs e)
        {
            _Wumpus = new Wumpus(0, 0);
        }

        private void MinionButton_Click(object sender, EventArgs e)
        {
            _Minion = new Minion(0, 0);
        }
    }
}

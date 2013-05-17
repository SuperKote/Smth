using System;
using System.Windows.Forms;
using System.Threading;

namespace Clicker
{
    public partial class MainForm : Form
    {
        private Thread _thread;

        public MainForm()
        {
            InitializeComponent();
            LRoundLeft.Text = string.Format("Циклов осталось: {0}", TBRoundCount.Text);
            Validate();
        }      

        private void SalvageButton_Click(object sender, EventArgs e)
        {
        }
        
        private void MiningButton_Click(object sender, EventArgs e)
        {
            _thread = new Thread(Mining) {IsBackground = false};
            _thread.Start(); 
        }

        private void Mining()
        {
            var actions = new Actions();
            actions.InitialCLick();
            int roundCount;
            if (!Int32.TryParse(TBRoundCount.Text, out roundCount))
                roundCount = 10;
            for (var i = 0; i < roundCount; i++)
            {
                DisplayProgress(roundCount - i);
                actions.Undock();
                actions.WarpToBookmark();
                actions.DeployDrones();
                try
                {
                    actions.PerformMiningCycle();
                }
                finally 
                {
                    actions.SubstituteAsteroidBookmark();
                    actions.ScopeDrones();
                    actions.DockToStation();
                }
                actions.UnloadCargo();
            }
            if (QuitCheck.Checked)
                actions.QuitGame();
            }

        private void DisplayProgress(int i)
        {
            LRoundLeft.Text = string.Format("Циклов осталось: {0}",i);
            Validate();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _thread.Abort();            
            Application.Exit();
        }

        private void StopButton2_Click(object sender, EventArgs e)
        {
            _thread.Abort();
            Application.Exit();
        }
    }
}

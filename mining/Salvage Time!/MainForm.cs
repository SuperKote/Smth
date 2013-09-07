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
            timer.Enabled = true;
        }      

        private void SalvageButton_Click(object sender, EventArgs e)
        {
        }

        private void ChangeStateButton_Click(object sender, EventArgs e)
        {
            if (ChangeStateButton.Text == "Start(F8)")
            {
                ChangeStateButton.Text = "Stop(F8)";
                _thread = new Thread(Mining) {IsBackground = false};
                _thread.Start();
            }
            else
            {
                ChangeStateButton.Text = "Start(F8)";
                _thread.Abort();
            }
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

        private void StopButton2_Click(object sender, EventArgs e)
        {
            _thread.Abort();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Keys.F8.Equals(Helpers.KeyBoard.CurrentKey))
            {
                ChangeStateButton.PerformClick();
                Helpers.KeyBoard.ResetCurrentKey();
            }
        }
    }
}

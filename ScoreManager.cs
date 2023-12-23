using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGame
{
    public class ScoreManager
    {
        private Label scoreTxt;

        public ScoreManager(Label scoreTxt)
        {
            this.scoreTxt = scoreTxt;
        }

        public void UpdateScoreText(int score)
        {
            scoreTxt.Text = "Score:" + score;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGame
{
    public class CollisionManager
    {
        private Form1 formReference;
        private Control player;
        private List<Control> platforms;
        private List<Control> coins;
        private Control monster;
        private Control key;
        private Control chest;
        public int score;
        private int force;
        private int jumpSpeed;
        private bool jumping, keyPresence;
        private Timer gameTimer;
        private Action gameRestartAction;
        private Label ScoreTxt;

        public CollisionManager(Form1 form, Timer gameTimer, Action gameRestartAction, Control player, List<Control> platforms, List<Control> coins, Control monster, Control key, Control chest, Label ScoreTxt)
        {
            this.formReference = form;
            this.player = player;
            this.platforms = platforms;
            this.coins = coins;
            this.monster = monster;
            this.key = key;
            this.chest = chest;
            this.gameTimer = gameTimer;
            this.gameRestartAction = gameRestartAction;
            this.ScoreTxt = ScoreTxt;
        }

        public void CheckCollisions()
        {
            foreach (Control x in formReference.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    UpdatePlatformCollision(x);
                }

                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    UpdateCoinCollision(x);
                }

                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    UpdateMonsterCollision(x);
                }
            }

            UpdateKeyCollision();
            UpdateChestCollision();
        }

        private void UpdatePlatformCollision(Control platform)
        {
            if (player.Bounds.IntersectsWith(platform.Bounds) && !jumping)
            {
                force = 8;
                player.Top = platform.Top - player.Height;
                jumpSpeed = 0;
            }

            platform.BringToFront();

            if (player.Top + player.Height < 50)
            {
                player.Top += 3;
            }
        }

        private void UpdateCoinCollision(Control coin)
        {
            if (player.Bounds.IntersectsWith(coin.Bounds) && coin.Visible)
            {
                coin.Visible = false;
                score += 1;
            }
        }

        private void UpdateMonsterCollision(Control monster)
        {
            if (player.Bounds.IntersectsWith(monster.Bounds))
            {
                gameTimer.Stop();
                MessageBox.Show("Game over! You lost. Click OK to play again.");
                gameRestartAction();
            }
        }

        private void UpdateKeyCollision()
        {
            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                key.Visible = false;
                keyPresence = true;
            }
        }

        private void UpdateChestCollision()
        {
            if (player.Bounds.IntersectsWith(chest.Bounds) && keyPresence && score == 30)
            {
                ((PictureBox)chest).Image = Properties.Resources.chest_opened;
                gameTimer.Stop();
                MessageBox.Show("Well done, your Journey completed!");
                gameRestartAction();

            }
            else
            {
                ScoreTxt.Text = "Score: " + score + Environment.NewLine + "Collect all the coins";
            }
        }
    }
}

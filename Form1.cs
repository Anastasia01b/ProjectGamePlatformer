using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace FormGame
{
    public enum Direction
    {
        Forward,
        Backward
    }
    public partial class Form1 : Form
    {
        private GameManager gameManager = new GameManager();
        private GameLogic gameLogic = new GameLogic();
        private InputHandler inputHandler = new InputHandler();
        private ScoreManager scoreManager;
        private const int BackgroundSpeed = 8;
        private const int PlayerSpeed = 7;
        private CollisionManager collisionManager;

        private bool jumping, movementLeft, movementRight;
        private int jumpSpeed;
        private int score = 0;
        int force = 8;
        private List<Control> platforms = new List<Control>();
        private List<Control> coins = new List<Control>();
        private Control monster;


        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        public void InitializeGame()
        {
            gameManager = new GameManager();
            inputHandler = new InputHandler();
            collisionManager = new CollisionManager(this, GTimer, GameRestart, player, platforms, coins, monster, key, chest, scoreTxt);
            gameLogic = new GameLogic();
            scoreManager = new ScoreManager(scoreTxt);
        }
        private void EventTimer(object sender, EventArgs e)
        {
            UpdateBackground();
            UpdatePlayerPosition();
            gameLogic.HandleJumping(jumping, ref jumpSpeed, ref force);
            collisionManager.CheckCollisions();
            scoreManager.UpdateScoreText(score);
        }
        private void UpdateBackground()
        {
            if (movementRight && background.Left > -1092)
            {
                background.Left -= BackgroundSpeed;
                GameElementMove(Direction.Backward);
            }

            if (movementLeft && background.Left < 0)
            {
                background.Left += BackgroundSpeed;
                GameElementMove(Direction.Forward);
            }
        }
        private void UpdatePlayerPosition()
        {
            player.Top += jumpSpeed;

            foreach (Control platform in platforms)
            {
                if (player.Bounds.IntersectsWith(platform.Bounds) && jumpSpeed >= 0)
                {
                    jumping = false;
                    force = 8;
                    player.Top = platform.Top - player.Height;
                    jumpSpeed = 0;
                    break;
                }
            }
            if (movementRight && player.Left + (player.Width + 20) < this.ClientSize.Width)
                player.Left += PlayerSpeed;

            if (movementLeft && player.Left > 20)
                player.Left -= PlayerSpeed;
            if (player.Top + player.Height > this.ClientSize.Height)
            {
                GTimer.Stop();
                MessageBox.Show("You Died!" + Environment.NewLine + "Click OK to play again");
                GameRestart();
            }
        }
        private void KeyDownIs(object sender, KeyEventArgs e) => inputHandler.HandleKeyDown(e, ref movementRight, ref movementLeft, ref jumping);
        private void KeyUpIs(object sender, KeyEventArgs e) => inputHandler.HandleKeyUp(e, ref movementRight, ref movementLeft, ref jumping);
        private void GameElementMove(Direction direction) => gameLogic.MoveGameElements(this.Controls.Cast<Control>(), direction);
        private void GameRestart() => gameManager.RestartGame(this);
        private void GameClosed(object sender, FormClosedEventArgs e) => gameManager.CloseGame();

    }
}


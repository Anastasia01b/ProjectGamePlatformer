using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGame
{
    public class GameLogic
    {
        private const int BackgroundSpeed = 8;
        private bool jumping;
        private int jumpSpeed;
        private int force;

        public GameLogic()
        {
            jumping = false;
            jumpSpeed = 0;
            force = 8;
        }
        public void MoveGameElements(IEnumerable<Control> controls, Direction direction)
        {
            foreach (Control x in controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "monster" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "chest")
                {
                    if (direction == Direction.Backward)
                    {
                        x.Left -= BackgroundSpeed;
                    }
                    if (direction == Direction.Forward)
                    {
                        x.Left += BackgroundSpeed;
                    }
                }
            }
        }
        public void HandleJumping(bool jumping, ref int jumpSpeed, ref int force)
        {
            if (jumping)
            {
                jumpSpeed = -10;
                force -= 1;

                if (force < 0)
                {
                    jumping = false;
                    force = 8;
                }
            }
            else
            {
                jumpSpeed = 10;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGame
{
    public class InputHandler
    {
        public void HandleKeyDown(KeyEventArgs e, ref bool movementRight, ref bool movementLeft, ref bool jumping)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    movementRight = true;
                    break;
                case Keys.Left:
                    movementLeft = true;
                    break;
                case Keys.Up when !jumping:
                    jumping = true;
                    break;
            }
        }

        public void HandleKeyUp(KeyEventArgs e, ref bool movementRight, ref bool movementLeft, ref bool jumping)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    movementRight = false;
                    break;
                case Keys.Left:
                    movementLeft = false;
                    break;
                case Keys.Up:
                    jumping = false;
                    break;
            }
        }
    }
}

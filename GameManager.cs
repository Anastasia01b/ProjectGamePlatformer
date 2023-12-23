using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormGame
{
    public class GameManager
    {
        public void RestartGame(Form1 form)
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            form.Hide();
        }

        public void CloseGame()
        {
            Application.Exit();
        }
    }
}

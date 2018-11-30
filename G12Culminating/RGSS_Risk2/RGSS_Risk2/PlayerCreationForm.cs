/* Anson Tu
 * Start Menu
 * Allow the user to start a new
 * game, load a previous game, or
 * quit the game
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RGSS_Risk2
{
    public partial class PlayerCreationForm : Form
    {
        public PlayerCreationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            //Save all of the players that will be created to an array
            CheckedListBox.CheckedItemCollection departments = chklstDepartmentChoices.CheckedItems;
            string[] departmentNames = new string[departments.Count];

            //Make sure at least 2 players will be made
            if (departments.Count >= 2)
            {
                //Launch the game
                for (int i = 0; i < departments.Count; i++)
                {
                    departmentNames[i] = (string)departments[i];
                }
                Hide();
                RGSS_RISK_Form frmRgssRisk = new RGSS_RISK_Form(departmentNames);
                frmRgssRisk.Show();
            }
            //Tell the user that they need at least 2 players to play
            else
            {
                MessageBox.Show("You need at least two players to play!");
            }
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

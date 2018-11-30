// Jusman Hung
// January 25, 2017
// This form is used whenever a battle is engaged to determine the winner. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGSS_Risk2
{
    public partial class BattleForm : Form
    {
        // Store how many students the player sends to battle
        private int _wager;
        // Use random number generator to decide winner
        private Random numberGenerator = new Random();
        // Determine the attacking and defending tile
        private Tile _attackingTile, _defendingTile;

        /// <summary>
        /// Load BattleForm when player attacks another tile
        /// </summary>
        /// <param name="attackingTile">The tile that's sending students to attack</param>
        /// <param name="defendingTile">The defending tile</param>
        public BattleForm(Tile attackingTile, Tile defendingTile)
        {
            InitializeComponent();
            // Save tiles as private variables that can be accessed by entire class
            _attackingTile = attackingTile;
            _defendingTile = defendingTile;
        }

        /// <summary>
        /// Store player's choice as heads
        /// </summary>
        private void picHeads_Click(object sender, EventArgs e)
        {
            // Check if player inputted a valid wager amount
            if (int.TryParse(txtWagerAmount.Text, out _wager) == true &&
                _wager < _attackingTile.Students && _wager > 0)
            {
                // Pass in the player's choice and wager amount
                Attack(Coin.heads, _wager);
                pnlBattleSummary.Show();
            }
            else
            {
                lblPromptChoice.Text = "Please input valid wager amount.";
            }
        }

        /// <summary>
        /// Store the player's choice as tails
        /// </summary>
        private void picTails_Click(object sender, EventArgs e)
        {
            // Check if player inputted a valid wager amount
            if (int.TryParse(txtWagerAmount.Text, out _wager) == true &&
                _wager < _attackingTile.Students && _wager > 0)
            {
                // Pass in the player's choice and wager amount
                Attack(Coin.tails, _wager);
                pnlBattleSummary.Show();
            }
            else
            {
                lblPromptChoice.Text = "Please input valid wager amount.";
            }
        }

        /// <summary>
        /// Determine the winner of the battle based on the player's choice
        /// </summary>
        /// <param name="choice">Heads or tails</param>
        /// <param name="wager">Amount of students sent</param>
        /// <returns>True if the attacker won; false if defender won</returns>
        private bool Attack(Coin choice, int wager)
        {
            // Choose either 0 or 1 to win (heads or tails)
            int random = numberGenerator.Next(0, 2);
            // Check who won
            if (random == (int)choice)
            {
                lblWinner.Text = "Winner!";
                // Deduct students from defending tile
                _defendingTile.Students -= wager;
                // Check if there are students left in the defending tile
                if (_defendingTile.Students <= 0)
                {
                    // Take over the defending tile
                    _attackingTile.Students -= wager;
                    // Check if the tile has a negative amount of students
                    if (_defendingTile.Students < 0)
                    {
                        _defendingTile.Students = 0;
                    }
                    _defendingTile.Department = _attackingTile.Department;
                    _defendingTile.Students += wager;
                    lblBattleSummary.Text = "Enemy Tile Has Been Taken Over!";
                }
                else
                {
                    lblBattleSummary.Text = "Enemy Tile Loses: " + wager.ToString() + " Students\r\nStudents Remaining: " + _defendingTile.Students.ToString();
                }
                return true;
            }
            lblWinner.Text = "Loser!";
            // Check if the wagered amount is more than the defending tile's amount of students
            if (wager > _defendingTile.Students)
            {
                // Deduct students from attacking tile
                _attackingTile.Students -= _defendingTile.Students;
            }
            else
            {
                // Deduct students from attacking tile
                _attackingTile.Students -= wager;
            }
            lblBattleSummary.Text = "Ally Tile Loses: " + wager.ToString() + " Students\r\nStudents Remaining: " + _attackingTile.Students.ToString();
            return false;
        }

        /// <summary>
        /// Close BattleForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseBattleSummary_Click(object sender, EventArgs e)
        {
            pnlBattleSummary.Hide();
            this.Close();
        }
    }
}

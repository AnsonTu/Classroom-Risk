// Jusman Hung
// January 25, 2017
// Constructor for players
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGSS_Risk2
{
    public class Player
    {
        // Check whose turn it is
        protected Department _currentPlayer;
        // Store the amount of classrooms a player has
        protected int _classroomsOwned;
        // Store player's name
        private string _name;
        // Store player's department
        private Department _department;

        /// <summary>
        /// Constructor for player
        /// </summary>
        /// <param name="name">Player's selected teacher's name</param>
        /// <param name="dep">Player's selected department</param>
        public Player(string name, Department dep)
        {
            _name = name;
            _department = dep;
        }

        /// <summary>
        /// Return player's name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Return player's department
        /// </summary>
        public Department Department
        {
            get
            {
                return _department;
            }
        }

        /// <summary>
        /// Return the amount of classrooms a player has
        /// Change the number of classrooms a player has
        /// </summary>
        public int ClassroomsOwned
        {
            get
            {
                return _classroomsOwned;
            }
            set
            {
                _classroomsOwned = value;
            }
        }

        /// <summary>
        /// Check which player is currently making their turn
        /// Pass a player's turn to the next player
        /// </summary>
        public Department CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
            set
            {
                _currentPlayer = value;
            }
        }
    }
}

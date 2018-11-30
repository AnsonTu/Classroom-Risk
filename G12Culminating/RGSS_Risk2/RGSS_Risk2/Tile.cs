// Jusman Hung
// January 25, 2017
// Data type for storing a tile's department and amount of students 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGSS_Risk2
{
    public class Tile
    {
        // Store a tile's department
        private Department _department;
        // Store a tile's amount of students
        private int _students;

        /// <summary>
        /// Constructor for tile
        /// </summary>
        /// <param name="department">Tile's department</param>
        /// <param name="students">Tile's number of students</param>
        public Tile(Department department, int students)
        {
            _department = department;
            _students = students;
        }

        /// <summary>
        /// Return tile's department
        /// Change tile's department
        /// </summary>
        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
            }
        }

        /// <summary>
        /// Return tile's number of students
        /// Change tile's number of students
        /// </summary>
        public int Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
            }
        }
    }
}

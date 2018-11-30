/* Anson Tu
 * January 25, 2017
 * WorldModel
 * Save the map from the text file
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace RGSS_Risk2
{
    /// <summary>
    /// This class is used to create the "world"
    /// It provides a code interface between the 
    /// user and the tiles in the "world"
    /// </summary>
    class WorldModel
    {
        /// <summary>
        /// The grid of tiles for the board
        /// </summary>
        private Tile[,] board;

        /// <summary>
        /// The length of the DepartmentType enumeration
        /// </summary>
        private const int NUMBER_OF_DEPARTMENT_TYPES = 6;

        /// <summary>
        /// The default tile size for the board
        /// </summary>
        private const int DEFAULT_TILE_SIZE = 65;

        /// <summary>
        /// The tile size in pixels for the world
        /// </summary>
        private int _tileSize = DEFAULT_TILE_SIZE;

        /// <summary>
        /// Get or set the tile size for the world. The default
        /// tileSize is 20. Any attempts to assign a non-
        /// positive value for the tile size will cause the 
        /// tiles to be reset to the default size
        /// </summary>
        public int TileSize
        {
            get
            {
                return _tileSize;
            }
            set
            {
                //Allow only positive values for the tile size
                if (value > 0)
                {
                    _tileSize = value;
                }
                else
                {
                    _tileSize = DEFAULT_TILE_SIZE;
                }
            }
        }

        /// <summary>
        /// Get the board 2D array for the world
        /// </summary>
        public Tile[,] Board
        {
            get
            {
                return board;
            }
        }

        /// <summary>
        /// Creates the board using the default board map
        /// </summary>
        public WorldModel()
        {
            LoadMapString(Properties.Resources.MapFile);
        }

        /// <summary>
        /// Loads the board string. 
        /// </summary>
        /// <param name="mapString">The board map</param>
        private void LoadMapString(string mapString)
        {
            //The number of rows and columns in the map
            int rows, cols;
            //Split the board string into its rows
            string[] lines = mapString.Split(new char[] { '\n' });

            //If it is not an empty world
            if (lines.Length > 4)
            {
                int.TryParse(lines[0], out rows);
                int.TryParse(lines[1], out cols);

                //Invalid map size
                if (rows <= 0 || cols <= 0)
                {
                    return;
                }

                //Create the board
                board = new Tile[rows, cols];

                //Load the different tiles into the board
                for (int r = 0; r < rows; r++)
                {
                    lines[r + 2] = lines[r + 2].Trim();

                    //The next row in the array offset by 2 
                    //because the first 2 elements in the array
                    //are the size of the board
                    char[] currentLine = lines[r + 2].ToCharArray();

                    for (int c = 0; c < cols; c++)
                    {
                        int val = (int)char.GetNumericValue(currentLine[c]);

                        if (val < NUMBER_OF_DEPARTMENT_TYPES && currentLine[c] != '\r' && currentLine[c] != '\n')
                        {
                            board[r, c] = new Tile((Department)val, 0);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load the board from a text file
        /// </summary>
        /// <param name="mapFile">The name of the map file</param>
        private void LoadBoard(string mapFile)
        {
            //Check that the file exists before attempting to open
            if (File.Exists(mapFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(mapFile))
                    {
                        LoadMapString(sr.ReadToEnd());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Could not read the map file.");
                }
            }
        }
    }
}

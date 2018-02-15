using System;
using System.IO;

namespace PizzaCutter
{
    class Pizza
    {
        public const bool TOMATO = true;
        public const bool MUSHROOM = false;

        public int Height { get; private set; }
        public int Width { get; private set; }
        public int MinIngredients { get; private set; }
        public int MaxCells { get; private set; }

        private bool[,] contents;
        private bool[,] usedCells;

        public Pizza(string inputFilePath)
        {
            string[] fileContents = File.ReadAllLines(inputFilePath);

            //Obtain the metadata from the first line of the input file
            string[] metaData = fileContents[0].Split(' ');

            //Save the metadata to their corresponding variables
            Height = int.Parse(metaData[0]);
            Width = int.Parse(metaData[1]);
            MinIngredients = int.Parse(metaData[2]);
            MaxCells = int.Parse(metaData[3]);

            //Initialise the contents and usedCells arrays for this pizza
            contents = new bool[Width, Height];
            usedCells = new bool[Width, Height];

            //Populate the contents array from the input file
            for(int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    if(fileContents[y + 1][x] == 'T')
                    {
                        contents[x, y] = TOMATO;
                    }
                    else if(fileContents[y + 1][x] == 'M')
                    {
                        contents[x, y] = MUSHROOM;
                    }
                    else
                    {
                        throw new Exception("Unknown ingredient encountered!");
                    }
                }
            }
        }

        public bool CellTaken(int x, int y)
        {
            return usedCells[x, y];
        }

        public bool GetCell(int x, int y)
        {
            return contents[x, y];
        }
    }
}

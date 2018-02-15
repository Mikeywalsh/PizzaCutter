using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCutter
{
    class Slice
    {
        private Pizza pizza;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Slice(Pizza p, int x, int y, int width, int height)
        {
            pizza = p;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            if(!ValidSlice())
            {
                throw new Exception("Slice is not valid!");
            }
        }

        public bool ValidSlice()
        {
            //If the slice does not fit in the pizza, return false
            if (X > pizza.Width || Y > pizza.Height || X + Width > pizza.Width || Y + Height > pizza.Height)
            {
                return false;
            }

            //If this slice shares space with another slice, return false
            for (int y = Y; y < Y + Height; y++)
            {
                for (int x = X; x < X + Width; x++)
                {
                    if (pizza.CellTaken(x, y))
                    {
                        return false;
                    }
                }
            }

            //Return true otherwise
            return true;
        }

        public bool CorrectSlice()
        {
            int tomatos = 0;
            int mushrooms = 0;

            //If this slice is too big, return false
            if(Width * Height > pizza.MaxCells)
            {
                return false;
            }

            //If this slice does not satisfy the minimum ingredient values, return false
            for (int y = Y; y < Y + Height; y++)
            {
                for (int x = X; x < X + Width; x++)
                {
                    if (pizza.GetCell(x, y) == Pizza.TOMATO)
                    {
                        tomatos++;
                    }
                    else if (pizza.GetCell(x, y) == Pizza.MUSHROOM)
                    {
                        mushrooms++;
                    }
                    else
                    {
                        throw new Exception("Unknown ingredient encountered!");
                    }
                }
            }

            //Return true otherwise
            return true;
        }
    }
}

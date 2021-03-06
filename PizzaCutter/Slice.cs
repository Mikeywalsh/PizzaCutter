﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCutter
{
    class Slice
    {
        private PizzaCutter cutter;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Slice(PizzaCutter pc, int x, int y, int width, int height)
        {
            cutter = pc;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool CorrectSlice()
        {
            int tomatos = 0;
            int mushrooms = 0;

            //If this slice is too big, return false
            if(Area > cutter.CurrentPizza.MaxCells)
            {
                return false;
            }

            //Get the amount of tomatoes and mushrooms on this pizza
            for (int y = Y; y < Y + Height; y++)
            {
                for (int x = X; x < X + Width; x++)
                {
                    if (cutter.CurrentPizza.GetCell(x, y) == Pizza.TOMATO)
                    {
                        tomatos++;
                    }
                    else if (cutter.CurrentPizza.GetCell(x, y) == Pizza.MUSHROOM)
                    {
                        mushrooms++;
                    }
                    else
                    {
                        throw new Exception("Unknown ingredient encountered!");
                    }
                }
            }

            //If this slice does not satisfy the minimum ingredient values, return false
            if (tomatos < cutter.CurrentPizza.MinIngredients || mushrooms < cutter.CurrentPizza.MinIngredients)
            {
                return false;
            }

            //Return true otherwise
            return true;
        }

        public int Area
        {
            get { return Width * Height; }
        }

        public override string ToString()
        {
            return Y + " " + X + " " + (Y + Height - 1) + " " + (X + Width - 1);
        }
    }
}

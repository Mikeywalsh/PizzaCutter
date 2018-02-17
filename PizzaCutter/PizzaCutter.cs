using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCutter
{
    class PizzaCutter
    {
        public Pizza CurrentPizza { get; private set; }
        private List<Slice> slices;
        private Random r = new Random();

        private bool[,] usedCells;
        private List<IntVector2> PossibleCuts;

        public PizzaCutter(Pizza p)
        {
            CurrentPizza = p;
            usedCells = new bool[p.Width, p.Height];
            PossibleCuts = new List<IntVector2>();

            //Generate a list of possible cuts based on the min ingredients and max cell count for the pizza
            for (int y = 1; y <= p.MaxCells; y++)
            {
                for (int x = 1; x <= p.MaxCells; x++)
                {
                    if (x * y >= p.MinIngredients * 2 && x * y <= p.MaxCells)
                    {
                        PossibleCuts.Add(new IntVector2(x, y));
                    }
                    else if (x * y > p.MaxCells)
                    {
                        break;
                    }
                }
            }

            //Reset all values of the slicer to their initial values
            ResetSlicer();

            //Shuffle the list of possible cuts
            PossibleCuts.Shuffle();
        }

        public void GenerateSlices()
        {
            for (int y = 0; y < CurrentPizza.Height; y++)
            {
                for (int x = 0; x < CurrentPizza.Width; x++)
                {
                    //If the current cell is being used by a slice, continue
                    if (usedCells[x, y])
                    {
                        continue;
                    }

                    //Shuffle the list of possible cuts
                    PossibleCuts.Shuffle();

                    //Try and find a valid slice to put in this position from the list of possible cuts
                    foreach (IntVector2 cut in PossibleCuts)
                    {
                        //Check if this slice is valid
                        int tomatosInSlice = 0;
                        int mushroomsInSlice = 0;
                        bool correctSlice = true;

                        for (int sliceY = y; sliceY < y + cut.Y; sliceY++)
                        {
                            for (int sliceX = x; sliceX < x + cut.X; sliceX++)
                            {
                                if (sliceX >= CurrentPizza.Width || sliceY >= CurrentPizza.Height || usedCells[sliceX, sliceY])
                                {
                                    correctSlice = false;
                                    break;
                                }
                                else
                                {
                                    if (CurrentPizza.GetCell(sliceX, sliceY) == Pizza.TOMATO)
                                    {
                                        tomatosInSlice++;
                                    }
                                    else if (CurrentPizza.GetCell(sliceX, sliceY) == Pizza.MUSHROOM)
                                    {
                                        mushroomsInSlice++;
                                    }
                                    else
                                    {
                                        throw new Exception("Unknown ingredient encountered!");
                                    }
                                }
                            }

                            if (!correctSlice)
                            {
                                break;
                            }
                        }

                        //If there is not enough ingredients on the pizza, invalidate it
                        if (tomatosInSlice < CurrentPizza.MinIngredients || mushroomsInSlice < CurrentPizza.MinIngredients)
                        {
                            correctSlice = false;
                        }

                        //If this slice is not valid, continue
                        if (!correctSlice)
                        {
                            continue;
                        }

                        //If this slice is valid, add it to the pizza and break out of the foreach loop
                        AddSlice(new Slice(this, x, y, cut.X, cut.Y));
                        break;
                    }
                }
            }
        }

        public void AddSlice(Slice s)
        {
            slices.Add(s);

            for (int y = s.Y; y < s.Y + s.Height; y++)
            {
                for (int x = s.X; x < s.X + s.Width; x++)
                {
                    usedCells[x, y] = true;
                }
            }
        }

        public void ResetSlicer()
        {
            //Set all cells to not being used
            for (int y = 0; y < CurrentPizza.Height; y++)
            {
                for (int x = 0; x < CurrentPizza.Width; x++)
                {
                    usedCells[x, y] = false;
                }
            }

            //Reset the slices list
            slices = new List<Slice>();
        }

        public int Score
        {
            get
            {
                int score = 0;

                foreach (Slice s in slices)
                {
                    if (s.CorrectSlice())
                    {
                        score += s.Area;
                    }
                    else
                    {
                        throw new Exception("Invalid slice in slices list!");
                    }
                }

                Console.WriteLine("Obtained " + score + " from " + slices.Count + " slices! ");

                return score;
            }
        }

        public List<Slice> CopySlices()
        {
            return new List<Slice>(slices);
        }
    }
}

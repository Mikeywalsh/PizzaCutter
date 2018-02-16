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
        Random r = new Random();

        private Slice[,] usedCells;

        public PizzaCutter(Pizza p)
        {
            CurrentPizza = p;
            usedCells = new Slice[p.Width, p.Height];

            ResetSlicer();
        }

        public void GenerateInitialSlices()
        {
            int sliceCount = (int)Math.Ceiling((double)CurrentPizza.Area / CurrentPizza.MaxCells);
            int currentSlices = 0;

            while (currentSlices != sliceCount)
            {
                int x = r.Next(0, CurrentPizza.Width);
                int y = r.Next(0, CurrentPizza.Height);

                if (CellTaken(x, y) != null)
                {
                    continue;
                }

                Slice newSlice = new Slice(this, x, y, 1, 1);
                if (newSlice.ValidSlice())
                {
                    AddSlice(newSlice);
                    currentSlices++;
                }
            }
        }

        public void ExpandSlices()
        {
            List<Slice> newSlices = new List<Slice>(slices);

            for(int i = 0; i < slices.Count; i++)
            {
                Slice lastCorrect = null;

                //Temp remove
                for (int y = slices[i].Y; y < slices[i].Y + slices[i].Height; y++)
                {
                    for (int x = slices[i].X; x < slices[i].X + slices[i].Width; x++)
                    {
                        usedCells[x, y] = null;
                    }
                }

                while (true)
                {
                    //Generate possible expansions
                    List<Slice> possibleExpansions = slices[i].PossibleExpansions();

                    //Check validity for each possible slice
                    foreach (Slice pos in possibleExpansions)
                    {
                        if (!pos.ValidSlice())
                        {
                            possibleExpansions.Remove(pos);
                        }
                    }

                    //If no possible next slices and this slice is not correct, remove it from the slices list
                    if (possibleExpansions.Count == 0)
                    {
                        break;
                    }

                    //Choose a random possible slice
                    slices[i] = possibleExpansions[r.Next(0, possibleExpansions.Count)];

                    if(slices[i].CorrectSlice())
                    {
                        lastCorrect = new Slice(this, slices[i].X, slices[i].Y, slices[i].Width, slices[i].Height);
                    }
                }

                if(lastCorrect != null)
                {
                    newSlices.Add(lastCorrect);
                }
                else
                {
                    newSlices.Remove(slices[i]);
                }
            }
        }

        public void AddSlice(Slice s)
        {
            if (s.ValidSlice())
            {
                slices.Add(s);
            }

            for (int y = s.Y; y < s.Y + s.Height; y++)
            {
                for (int x = s.X; x < s.X + s.Width; x++)
                {
                    usedCells[x, y] = s;
                }
            }
        }

        public void RemoveSlice(Slice s)
        {
            if (slices.Contains(s))
            {
                slices.Remove(s);
            }

            for (int y = s.Y; y < s.Y + s.Height; y++)
            {
                for (int x = s.X; x < s.X + s.Width; x++)
                {
                    usedCells[x, y] = null;
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
                    usedCells[x, y] = null;
                }
            }

            //Reset the slices list
            slices = new List<Slice>();
        }

        public Slice CellTaken(int x, int y)
        {
            return usedCells[x, y];
        }

        public int Score
        {
            get
            {
                int score = 0;
                int incorrectSlices = 0;

                foreach (Slice s in slices)
                {
                    if (s.ValidSlice() && s.CorrectSlice())
                    {
                        score += s.Area;
                    }
                    else if (s.ValidSlice())
                    {
                        incorrectSlices++;
                    }
                    else
                    {
                        throw new Exception("Invalid slice in slices list!");
                    }
                }

                Console.WriteLine("Obtained " + score + " from " + slices.Count + " slices! " + incorrectSlices + " slices were incorrect!");

                return score;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace PizzaCutter
{
    class Program
    {
        static string FileName = "medium";
        static int Iterations = 10000;

        static void Main(string[] args)
        {
            //Read the pizza from the input file
            Pizza p = new Pizza(FileName + ".in");

            //Display metadata about the pizza
            Console.WriteLine("Loaded Pizza:");
            Console.WriteLine("Height: " + p.Height);
            Console.WriteLine("Width: " + p.Width);
            Console.WriteLine("Min Ingredients Per Slice: " + p.MinIngredients);
            Console.WriteLine("Max Cells Per Slice: " + p.MaxCells);

            //Create a pizza cutter
            PizzaCutter cutter = new PizzaCutter(p);

            //Initialise the best score and the best slice list
            int bestScore = 0;
            List<Slice> bestSlices = null;

            //Generate random valid slices on the pizza and obtain the score a predetermined amount of times
            for (int i = 0; i < Iterations; i++)
            {
                //Reset the pizza slicer
                cutter.ResetSlicer();

                //Generate slices on the pizza
                cutter.GenerateSlices();

                //Get the score of the slicer
                Console.WriteLine("Iteration: " + i);
                int score = cutter.Score;

                //If this score is better than the best score, replace the best score with it and save the slice list
                if(score > bestScore)
                {
                    bestScore = score;
                    bestSlices = cutter.CopySlices();
                }
            }

            //Display the best score
            Console.WriteLine();
            Console.WriteLine("Best score " + bestScore + " from " + Iterations + " iterations!");

            //Create a valid output file containing a list of the best slices found
            List<string> outputContents = new List<string>();
            outputContents.Add(bestSlices.Count.ToString());

            foreach(Slice s in bestSlices)
            {
                outputContents.Add(s.ToString());
            }

            File.WriteAllLines(FileName + ".out", outputContents);
            Console.ReadLine();
        }
    }
}

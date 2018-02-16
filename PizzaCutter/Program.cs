using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCutter
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizza p = new Pizza("example.in");
            Console.WriteLine("Loaded Pizza:");
            Console.WriteLine("Height: " + p.Height);
            Console.WriteLine("Width: " + p.Width);
            Console.WriteLine("Min Ingredients Per Slice: " + p.MinIngredients);
            Console.WriteLine("Max Cells Per Slice: " + p.MaxCells);

            PizzaCutter cutter = new PizzaCutter(p);

            cutter.GenerateInitialSlices();
            cutter.ExpandSlices();
            //p.AddSlice(new Slice(p, 0, 0, 2, 3));
            //p.AddSlice(new Slice(p, 2, 1, 1, 3));
            //p.AddSlice(new Slice(p, 3, 0, 2, 3));

            Console.WriteLine(cutter.Score);
            Console.ReadLine();
        }
    }
}

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
            Console.ReadLine();
        }
    }
}

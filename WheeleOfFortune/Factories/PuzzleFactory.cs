using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheeleOfFortune.Models;

namespace WheeleOfFortune.Factories
{
    public class PuzzleFactory
    {
        public Puzzle CreatePuzzle()
        {
            Console.WriteLine("What category is the new puzzle?");
            var newCategory = Console.ReadLine();
            Console.WriteLine("What is the password for this new puzzle?");
            var newPassword = Console.ReadLine();
            Puzzle puzzle = new Puzzle()
            {
                Category = newCategory.ToUpper(),
                Password = newPassword.ToUpper()
            };
            return puzzle;
        }

        public Puzzle CreatePuzzle(string category, string password)
        {
            Puzzle puzzle = new Puzzle()
            {
                Category = category.ToUpper(),
                Password = password.ToUpper()
            };
            return puzzle;
        }
    }
}

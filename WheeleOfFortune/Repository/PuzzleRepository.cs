using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheeleOfFortune.Models;

namespace WheeleOfFortune.Repository
{
    public class PuzzleRepository
    {
        private static List<Puzzle> _puzzleList = new List<Puzzle>();
        Random rand = new Random();

        public void Add(Puzzle puzzle)
        {
            _puzzleList.Add(puzzle);
        }

        public Puzzle RandomPuzzleFromRepository()
        {
            var r = rand.Next(_puzzleList.Count);
            var puzzle = _puzzleList[r];
            return puzzle;
        }

        public int NumberOfPuzzles()
        {
            return _puzzleList.Count;
        }
        
    }
}

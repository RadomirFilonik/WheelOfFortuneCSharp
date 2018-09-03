using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheeleOfFortune.Factories;
using WheeleOfFortune.Helpers;
using WheeleOfFortune.Models;
using WheeleOfFortune.Repository;

namespace WheeleOfFortune
{
    class Program
    {
        private static readonly PuzzleFactory _puzzleFactory = new PuzzleFactory();
        private static readonly PuzzleRepository _puzzleRepository = new PuzzleRepository();
        private static readonly HidePassword _hidepassword = new HidePassword();
        private static readonly DisplayPasswod _displayPasswod = new DisplayPasswod();
        private static readonly LetterInPassword _letterInPassword = new LetterInPassword();
        private static readonly CheckUserPassword _checkUserPassword = new CheckUserPassword();
        private static readonly string[] _mainMenu = File.ReadAllLines("TextFiles/MainMenu.txt");
        private static readonly string[] _playOption = File.ReadAllLines("TextFiles/PlayOption.txt");
        private static readonly string[] _youWon = File.ReadAllLines("TextFiles/YouWon.txt");
        private static string _secretPassword = "";
        private static string _randomPassword = "";
        private static string _randomCategory = "";
        

        static void Main(string[] args)
        {
            try
            {
                LoadPuzzlesFromTextFile();
                ConsoleKeyInfo optionMainMenuKey;
                do
                {
                    Console.Clear();
                    DisplayMenu();
                    optionMainMenuKey = Console.ReadKey(true);

                    switch(optionMainMenuKey.Key)
                    {
                        case ConsoleKey.D1:
                            PlayGame();
                            break;
                        case ConsoleKey.D2:
                            CreatePuzzleForUser();
                            break;
                        case ConsoleKey.D3:
                            break;
                        default:
                            Console.WriteLine("Wrong option.");
                            Console.ReadKey();
                            break;
                    }
                }
                while (optionMainMenuKey.Key != ConsoleKey.D3);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.ReadKey();
            }
        }

        

        private static void PlayGame()
        {

            if(_puzzleRepository.NumberOfPuzzles() != 0)
            {
                var availableLetters = new AvailableLetters();
                var puzzle = _puzzleRepository.RandomPuzzleFromRepository();
                _randomPassword = puzzle.Password;
                _randomCategory = puzzle.Category;
                _secretPassword = _hidepassword.ChangeToSecretPassword(_randomPassword);
                ConsoleKeyInfo optionGameMenuKey;
                bool playIsOn = true;

                do
                {
                    DisplayCategoryAndPassword();
                    DisplayPlayOption();
                    optionGameMenuKey = Console.ReadKey(true);

                    switch (optionGameMenuKey.Key)
                    {
                        case ConsoleKey.D1:
                            playIsOn = SelectLetter(availableLetters);
                            break;
                        case ConsoleKey.D2:
                            playIsOn = GuessPassword();
                            break;
                        case ConsoleKey.D3:
                            break;
                        default:
                            Console.WriteLine("Wrong option.");
                            break;
                    }

                    if(playIsOn == false)
                    {
                        break;
                    }

                }
                while (optionGameMenuKey.Key != ConsoleKey.D3);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sorry, ther is no puzzles in memory");
                Console.ReadKey();
            }
        }
        
        private static bool SelectLetter(AvailableLetters availableLetters)
        {
            Console.Clear();
            DisplayCategoryAndPassword();
            Console.WriteLine("Choose from these letter:");
            Console.WriteLine(availableLetters.GetAvailableLetters());
            char chooseKey = char.ToUpper(Console.ReadKey(true).KeyChar);
            availableLetters.CheckAndRemoveLetterFromAvailableLetters(chooseKey);
            Console.WriteLine();

            var howManyLetter = _letterInPassword.HowManyUserLetterInPassword(chooseKey, _randomPassword);
            Console.WriteLine($"There is {howManyLetter} '{chooseKey}' hidden in password.");

            _secretPassword = _displayPasswod.DisplayPasswordFromHide(_secretPassword, chooseKey, _randomPassword);
            Console.WriteLine(_secretPassword);
            Console.WriteLine();
            Console.ReadKey();

            var isWin = _checkUserPassword.ComparePasswords(_secretPassword, _randomPassword);
            if (isWin)
            {
                DisplayYouWon();
                Console.ReadKey();
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private static void DisplayMenu()
        {
            foreach (var line in _mainMenu)
            {
                Console.WriteLine(line);
            }
        }

        private static void DisplayPlayOption()
        {
            foreach (var line in _playOption)
            {
                Console.WriteLine(line);
            }
        }

        private static void CreatePuzzleForUser()
        {
            Console.Clear();
            var puzzle = _puzzleFactory.CreatePuzzle();
            _puzzleRepository.Add(puzzle);
        }

        private static bool GuessPassword()
        {
            Console.Clear();
            Console.WriteLine("Guess password");
            Console.WriteLine(_randomPassword);
            Console.WriteLine(_secretPassword);
            var userPassword = Console.ReadLine().ToUpper();
            var userWon = _checkUserPassword.ComparePasswords(userPassword, _randomPassword);

            if(userWon)
            {
                Console.Clear();
                DisplayYouWon();
                Console.ReadKey();
                return false;
            }
            else
            {
                Console.WriteLine();
                Console.ReadKey();
                return true;
            }
            
        }

        private static void DisplayYouWon()
        {
            foreach (var line in _youWon)
            {
                Console.WriteLine(line);
            }
        }

        private static void DisplayCategoryAndPassword()
        {
            Console.Clear();
            Console.WriteLine($"Category : {_randomCategory}.");
            Console.WriteLine(_secretPassword);
            Console.WriteLine();
        }

        private static void LoadPuzzlesFromTextFile()
        {
            try
            {
                string[] puzzlesFromTextFile = File.ReadAllLines("TextFiles/Puzzles.txt");
                var puzzlesInMemory = puzzlesFromTextFile.Select(x =>
                {
                    string[] splitted = x.Split(',');
                    return new Puzzle
                    {
                        Category = splitted[0].ToUpper().Trim(),
                        Password = splitted[1].ToUpper().Trim()
                    };
                }).ToList();

                foreach (var puzzle in puzzlesInMemory)
                {
                    _puzzleRepository.Add(puzzle);
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("There is no Puzzles File");
                Console.WriteLine("Create you owne puzzles");
                Console.ReadKey();
            }
        }
    }
}

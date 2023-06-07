using System.Text.RegularExpressions;
using TicTacToe.Models;

namespace TicTacToe
{
	public static class GameSettings
	{
		public static GameType GetGameType()
		{
            string gameTypeInput;
            do
            {
                Console.WriteLine("Please choose a game type:");
                Console.WriteLine("    1. Play against the AI");
                Console.WriteLine("    2. Play against a friend");
                gameTypeInput = Console.ReadLine() ?? string.Empty;
            } while (!Regex.IsMatch(gameTypeInput, "^[1-2]$"));

            return (GameType) int.Parse(gameTypeInput);
        }

        public static int GetBoardSize()
        {
            string boardType;
            do
            {
                Console.WriteLine("Please choose a board size:");
                Console.WriteLine("    1. 3 By 3");
                Console.WriteLine("    2. 4 By 4");
                boardType = Console.ReadLine() ?? string.Empty;
            } while (!Regex.IsMatch(boardType, "^[1-2]$"));

            return int.Parse(boardType) + 2;
        }
    }
}
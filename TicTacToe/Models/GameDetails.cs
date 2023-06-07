namespace TicTacToe.Models
{
	public class GameDetails
	{
        public GameStatus GameStatus { get; set; }
        public GameResult? GameResult { get; set; }
        public string PlayerPieceThatWon { get; set; } = string.Empty;
    }
}
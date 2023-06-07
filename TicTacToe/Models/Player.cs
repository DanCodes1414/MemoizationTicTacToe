namespace TicTacToe.Models
{
	public class Player
	{
		public PlayerType Type { get; set; }
        public PlayerPiece Piece { get; set; }
        public bool IsPlayerTurn { get; set; }
        public string PlayerStartingMessage { get; set; }
        public string PlayerTurnMessage { get; set; }
        public string VictoryMessage { get; set; }
    }
}
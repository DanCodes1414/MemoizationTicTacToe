namespace TicTacToe.Models
{
	public class GameBoard
	{
        public Dictionary<int, GameBoardPlaceDetails> Places { get; set; } = new Dictionary<int, GameBoardPlaceDetails>();
        public int BoardSize { get; set; }

        public GameBoard(Dictionary<int, GameBoardPlaceDetails> places, int boardSize)
        {
            Places = places;
            BoardSize = boardSize;
        }

        public GameBoard(int boardSize)
        {
            BoardSize = boardSize;
        }
    }
}
namespace TicTacToe.Models
{
	public class GameBoardPlaceDetails
	{
        public int Column { get; set; }
        public int Row { get; set; }
        public string Value { get; set; }
        public bool IsPlaceTaken { get; set; }
        public bool IsInTopLeftToBottomRightRow { get; set; }
        public bool IsInTopRightToBottomLeftRow { get; set; }

        public GameBoardPlaceDetails(
            int column,
            int row,
            string value,
            bool isPlaceTaken,
            bool isInTopLeftToBottomRightRow,
            bool isInTopRightToBottomLeftRow)
        {
            Column = column;
            Row = row;
            Value = value;
            IsPlaceTaken = isPlaceTaken;
            IsInTopLeftToBottomRightRow = isInTopLeftToBottomRightRow;
            IsInTopRightToBottomLeftRow = isInTopRightToBottomLeftRow;
        }
    }
}
using TicTacToe.Helpers;
using TicTacToe.Models;

namespace TicTacToe
{
	public static class BoardSettings
	{
        public static void PlacePiece(this GameBoard board, int piecePosition, PlayerPiece pieceType)
        {
            board.Places[piecePosition].Value = pieceType.GetEnumDescription();
            board.Places[piecePosition].IsPlaceTaken = true;
        }

        public static void RemovePiece(this GameBoard board, int piecePosition)
        {
            board.Places[piecePosition].Value = $"{piecePosition}";
            board.Places[piecePosition].IsPlaceTaken = false;
        }

        public static string GetBoardPlaceValues(this GameBoard board)
        {
            return string.Join(" ", board.Places.Select(p => p.Value.Value).ToList());
        }

        public static void DisplayBoard(this GameBoard board)
        {
            var boardSize = board.BoardSize;
            var boardPosition = 1;
            for (var b = 0; b < boardSize; b++)
            {
                Console.Write("       |");
                for (var i = 3; i <= boardSize; i++)
                {
                    Console.Write("       |");
                }
                Console.Write("\n");

                DisplayBoardValues(board.Places[boardPosition].Value, true);
                boardPosition++;
                for (var i = 1; i < boardSize; i++)
                {
                    DisplayBoardValues(board.Places[boardPosition].Value, false);
                    boardPosition++;
                }
                Console.Write("\n");

                Console.Write("       |");
                for (var i = 3; i <= boardSize; i++)
                {
                    Console.Write("       |");
                }
                Console.Write("\n");

                if (b != boardSize - 1)
                {
                    Console.Write("-------");
                    for (var i = 2; i <= boardSize; i++)
                    {
                        Console.Write(" -------");
                    }
                    Console.Write("\n");
                }
            }
        }

        public static void DisplayBoardValues(string value, bool isFirstValueInRow)
        {
            if (isFirstValueInRow && value.Length == 1)
            {
                Console.Write($"   {value}   ");
            }
            else if (isFirstValueInRow && value.Length > 1)
            {
                Console.Write($"  {value}   ");
            }
            else if (!isFirstValueInRow && value.Length == 1)
            {
                Console.Write($"|   {value}   ");
            }
            else
            {
                Console.Write($"|  {value}   ");
            }
        }

        public static GameBoard CreateBoardPieces(int boardSize)
        {
            var board = new GameBoard(boardSize);
            var column = 1;
            int previousPositionInTopLeftToBottomRightRow = 1;
            int previousPositionInTopRightToBottomLeftRow = boardSize;
            for (var i = 1; i <= boardSize * boardSize; i++)
            {
                var row = (int)Math.Ceiling((decimal)i/ (decimal)boardSize);
                var isInTopLeftToBottomRightRow =
                    i == 1 ||
                    (i % (previousPositionInTopLeftToBottomRightRow + boardSize + 1) == 0);
                var isInTopRightToBottomLeftRow =
                    i == boardSize ||
                    ((i % (previousPositionInTopRightToBottomLeftRow + boardSize - 1) == 0) && i != boardSize * boardSize);

                board.Places.Add(i, new(column, row, $"{i}", false, isInTopLeftToBottomRightRow, isInTopRightToBottomLeftRow));

                previousPositionInTopLeftToBottomRightRow = isInTopLeftToBottomRightRow ? i : previousPositionInTopLeftToBottomRightRow;
                previousPositionInTopRightToBottomLeftRow = isInTopRightToBottomLeftRow ? i : previousPositionInTopRightToBottomLeftRow;
                column++;
                if (column > boardSize)
                {
                    column = 1;
                }
            }

            return board;
        }
    }
}
using System.Text.RegularExpressions;
using TicTacToe.Helpers;
using TicTacToe.Models;

namespace TicTacToe
{
	public static class GamePlay
	{
        public static GameDetails Start(GameType gameType, int boardSize, List<Player> players)
        {
            var board = BoardSettings.CreateBoardPieces(boardSize);
            board.DisplayBoard();
            PlayerSettings.DisplayStartingPlayerMessage(players);

            while (GameEnd.FindEndOfGame(board).GameStatus == GameStatus.StillPlaying)
            {
                var currentPlayer = players.GetCurrentPlayer();
                if (currentPlayer.Type == PlayerType.Human)
                {
                    var position = GetUserInputForPiecePosition(board);
                    board.PlacePiece(position, currentPlayer.Piece);
                }
                else
                {
                    board = ComputerGamePlay.PlayComputerTurn(board, players, gameType);
                }

                board.DisplayBoard();
                PlayerSettings.SwitchPlayerTurn(players);
            }

            return GameEnd.FindEndOfGame(board);
        }

        private static int GetUserInputForPiecePosition(GameBoard board)
        {
            string position;
            do
            {
                var message = "\nPlace your piece ";
                Console.Write(message);
                position = Console.ReadLine() ?? string.Empty;
                if (IsUserInputForPiecePositionValid(position, board.BoardSize))
                {
                    do
                    {
                        if (board.Places[int.Parse(position)].IsPlaceTaken)
                        {
                            Console.Write(message);
                            position = Console.ReadLine() ?? string.Empty;
                        }

                        if (!IsUserInputForPiecePositionValid(position, board.BoardSize))
                        {
                            break;
                        }
                    } while (board.Places[int.Parse(position)].IsPlaceTaken);
                }
            } while (!IsUserInputForPiecePositionValid(position, board.BoardSize));

            var intPosition = int.Parse(position);
            return intPosition;
        }

        private static bool IsUserInputForPiecePositionValid(string place, int boardSize)
        {
            return boardSize switch
            {
                3 => Regex.IsMatch(place, "^[1-9]$"),
                4 => Regex.IsMatch(place, "^1[0-2]|[1-9]$"),
                _ => throw new ArgumentOutOfRangeException("Invalid board size"),
            };
        }
    }
}
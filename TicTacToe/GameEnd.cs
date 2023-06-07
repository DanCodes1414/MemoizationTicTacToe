using TicTacToe.Helpers;
using TicTacToe.Models;

namespace TicTacToe
{
    public static class GameEnd
    {
        // TODO: When playing 4x4, 3 in a row should win the game.
        public static GameDetails FindEndOfGame(GameBoard board)
        {
            for (int i = 1; i <= board.BoardSize; i++)
            {
                var rowValues = board.Places.Where(v => v.Value.Row == i).DistinctBy(v => v.Value.Value);
                if (rowValues.Count() == 1)
                {
                    return new GameDetails
                    {
                        GameResult = FindWinner(rowValues.First().Value.Value),
                        GameStatus = GameStatus.Finished,
                        PlayerPieceThatWon = rowValues.First().Value.Value,
                    };
                }

                var columnValues = board.Places.Where(v => v.Value.Column == i).DistinctBy(v => v.Value.Value);
                if (columnValues.Count() == 1)
                {
                    return new GameDetails
                    {
                        GameResult = FindWinner(columnValues.First().Value.Value),
                        GameStatus = GameStatus.Finished,
                        PlayerPieceThatWon = columnValues.First().Value.Value,
                    };
                }
            }

            var topLeftToBottomRightRowValues = board.Places
                .Where(v => v.Value.IsInTopLeftToBottomRightRow)
                .DistinctBy(v => v.Value.Value);
            var topRightToBottomLeftRowValues = board.Places
                .Where(v => v.Value.IsInTopRightToBottomLeftRow)
                .DistinctBy(v => v.Value.Value);
            if (topLeftToBottomRightRowValues.Count() == 1)
            {
                return new GameDetails
                {
                    GameResult = FindWinner(board.Places[1].Value),
                    GameStatus = GameStatus.Finished,
                    PlayerPieceThatWon = board.Places[1].Value,
                };
            }
            else if (topRightToBottomLeftRowValues.Count() == 1)
            {
                return new GameDetails
                {
                    GameResult = FindWinner(board.Places[board.BoardSize].Value),
                    GameStatus = GameStatus.Finished,
                    PlayerPieceThatWon = board.Places[board.BoardSize].Value,
                };
            }
            else if (board.Places.All(v => v.Value.IsPlaceTaken == true))
            {
                return new GameDetails
                {
                    GameResult = GameResult.Drew,
                    GameStatus = GameStatus.Finished,
                };
            }
            else
            {
                return new GameDetails
                {
                    GameResult = null,
                    GameStatus = GameStatus.StillPlaying,
                };
            }
        }

        public static GameResult FindWinner(string value)
        {
            return value == PlayerPiece.O.GetEnumDescription() ? GameResult.Player1Won : GameResult.Player2Won;
        }

        public static void DeclareWinner(List<Player> players, GameDetails gameDetails)
        {
            var winningPlayer = players.GetPlayerByPiece(gameDetails.PlayerPieceThatWon);
            if (winningPlayer == null)
            {
                Console.WriteLine("\nIt was a tie!");
            }
            else
            {
                Console.WriteLine(winningPlayer.VictoryMessage);
            }
        }
    }
}
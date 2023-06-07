using System.Collections.Generic;
using System.Numerics;
using TicTacToe.Helpers;
using TicTacToe.Models;
using static System.Formats.Asn1.AsnWriter;

namespace TicTacToe
{
    public class ComputerGamePlay
    {
        public static GameBoard PlayComputerTurn(GameBoard board, List<Player> players, GameType gameType)
        {
            var currentPlayerPiece = players.GetCurrentPlayer().Piece;
            var position = FindBestMove(board, players);
            board.PlacePiece(position, currentPlayerPiece);

            return board;
        }

        private static int FindBestMove(GameBoard board, List<Player> players)
        {
            var bestScore = int.MinValue;
            var bestMove = 0;

            var maximizingPiece = players.GetCurrentPlayer().Piece;
            var minimizingPiece = players.GetNonCurrentPlayer().Piece;

            var gamePositionScores = new Dictionary<string, int>();
            for (int i = 1; i < Math.Pow(board.BoardSize, 2) + 1; i++)
            {
                if (!board.Places[i].IsPlaceTaken)
                {
                    board.PlacePiece(i, maximizingPiece);
                    var score = Minimax(gamePositionScores, board, 0, false, maximizingPiece, minimizingPiece);
                    board.RemovePiece(i);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }

            return bestMove;
        }

        private static int Minimax(
            Dictionary<string, int> gamePositionScoreDictionary,
            GameBoard board,
            int depth,
            bool isMaximizing,
            PlayerPiece maximizingPiece,
            PlayerPiece minimizingPiece)
        {
            int currentScore;
            int bestScore;
            if (gamePositionScoreDictionary.ContainsKey(board.GetBoardPlaceValues()))
            {
                currentScore = gamePositionScoreDictionary.GetValueOrDefault(board.GetBoardPlaceValues());
                return currentScore;
            }

            var gameDetails = GameEnd.FindEndOfGame(board);
            if (gameDetails.GameStatus == GameStatus.Finished)
            {
                return Score(gameDetails.GameResult, depth);
            }

            if (isMaximizing)
            {
                bestScore = int.MinValue;
                for (int i = 1; i < Math.Pow(board.BoardSize, 2) + 1; i++)
                {
                    if (!board.Places[i].IsPlaceTaken)
                    {
                        board.PlacePiece(i, maximizingPiece);
                        currentScore = Minimax(gamePositionScoreDictionary, board, depth + 1, false, maximizingPiece, minimizingPiece);

                        board.RemovePiece(i);

                        bestScore = Math.Max(currentScore, bestScore);
                    }
                }

                gamePositionScoreDictionary.Add(string.Join(" ", board.Places.Select(p => p.Value.Value).ToList()), bestScore);
                return bestScore;
            }
            else
            {
                bestScore = int.MaxValue;
                for (int i = 1; i < Math.Pow(board.BoardSize, 2) + 1; i++)
                {
                    if (!board.Places[i].IsPlaceTaken)
                    {
                        board.PlacePiece(i, minimizingPiece);
                        currentScore = Minimax(gamePositionScoreDictionary, board, depth + 1, true, maximizingPiece, minimizingPiece);

                        board.RemovePiece(i);

                        bestScore = Math.Min(currentScore, bestScore);
                    }
                }

                gamePositionScoreDictionary.Add(board.GetBoardPlaceValues(), bestScore);
                return bestScore;
            }
        }

        private static int Score(GameResult? gameResult, int depth)
        {
            var score = 0;
            switch (gameResult)
            {
                case GameResult.Player1Won:
                    score = 1000 - depth;
                    break;
                case GameResult.Player2Won:
                    score = -1000 + depth;
                    break;
                default:
                    break;
            }

            return score;
        }
    }
}
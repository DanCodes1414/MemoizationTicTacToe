using TicTacToe.Helpers;
using TicTacToe.Models;

namespace TicTacToe
{
	public static class PlayerSettings
	{
        public static Player GetCurrentPlayer(this List<Player> players)
        {
            return players.Single(p => p.IsPlayerTurn);
        }

        public static Player GetNonCurrentPlayer(this List<Player> players)
        {
            return players.Single(p => !p.IsPlayerTurn);
        }

        public static Player? GetPlayerByPiece(this List<Player> players, string piece)
        {
            return players.SingleOrDefault(p => p.Piece.GetEnumDescription() == piece);
        }

        public static List<Player> CreatePlayers(GameType gameType)
        {
            var player1 = new Player();
            var player2 = new Player();
            if (gameType == GameType.PlayAi)
            {
                var randomNumber = new Random().Next(1, 3);

                player1.Type = PlayerType.Computer;
                player1.Piece = PlayerPiece.O;
                player1.PlayerStartingMessage = "The Computer is starting";
                player1.PlayerTurnMessage = "Computer's turn";
                player1.VictoryMessage = "\nYou lost the game :(";
                player1.IsPlayerTurn = randomNumber == 1 ? true : false;

                player2.Type = PlayerType.Human;
                player2.Piece = PlayerPiece.X;
                player2.PlayerStartingMessage = "You're starting";
                player2.PlayerTurnMessage = "Your turn";
                player2.VictoryMessage = "\nYou won the game :)";
                player2.IsPlayerTurn = randomNumber == 2 ? true : false;
            }
            else if (gameType == GameType.PlayFriend)
            {
                player1.Type = PlayerType.Human;
                player1.Piece = PlayerPiece.O;
                player1.PlayerStartingMessage = "Player 1 will start";
                player1.PlayerTurnMessage = "Player 1's turn";
                player1.VictoryMessage = "\nPlayer 1 won the game!";
                player1.IsPlayerTurn = true;

                player2.Type = PlayerType.Human;
                player2.Piece = PlayerPiece.X;
                player2.PlayerTurnMessage = "Player 2's turn";
                player2.VictoryMessage = "\nPlayer 2 won the game!";
                player2.IsPlayerTurn = false;
            }

            return new List<Player>()
            {
                player1,
                player2,
            };
        }

        public static void SwitchPlayerTurn(List<Player> players)
        {
            var oldCurrentPlayer = players.Single(p => p.IsPlayerTurn);
            var newCurrentPlayer = players.Single(p => !p.IsPlayerTurn);
            oldCurrentPlayer.IsPlayerTurn = false;
            newCurrentPlayer.IsPlayerTurn = true;
            Console.WriteLine($"\n{newCurrentPlayer.PlayerTurnMessage}");
        }

        public static void DisplayStartingPlayerMessage(List<Player> players)
        {
            var currentPlayer = players.GetCurrentPlayer();
            Console.WriteLine($"\n{currentPlayer.PlayerStartingMessage}");
        }
    }
}
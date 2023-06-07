namespace TicTacToe
{
    internal class Program
    {
        private static void Main()
        {
            var gameType = GameSettings.GetGameType();
            var boardSize = GameSettings.GetBoardSize();
            var players = PlayerSettings.CreatePlayers(gameType);

            var gameDetails = GamePlay.Start(gameType, boardSize, players);
            GameEnd.DeclareWinner(players, gameDetails);
        }
    }
}
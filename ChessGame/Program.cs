using ChessGame.tabuleiro;


namespace ChessGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Posicao P = new Posicao(3, 4);
            Console.WriteLine(P);

            Tabuleiro T = new Tabuleiro(8, 8);
        }
    }
}
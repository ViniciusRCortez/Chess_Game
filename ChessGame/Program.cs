using ChessGame.tabuleiro;


namespace ChessGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {


            Tabuleiro T = new Tabuleiro(8, 8);
            Tela.imprimirTabuleiro(T);
        }
    }
}
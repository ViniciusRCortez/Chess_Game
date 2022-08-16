using ChessGame.tabuleiro;
using ChessGame.xadrez;


namespace ChessGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Tabuleiro T = new Tabuleiro(8, 8);
                Tela.imprimirTabuleiro(T);
                Console.WriteLine("\n\n\n");

                T.colocarPeca(new Torre(Cor.Preta, T), new Posicao(0, 0));
                T.colocarPeca(new Cavalo(Cor.Preta, T), new Posicao(0, 1));
                T.colocarPeca(new Bispo(Cor.Preta, T), new Posicao(0, 2));
                T.colocarPeca(new Rainha(Cor.Preta, T), new Posicao(0, 3));
                T.colocarPeca(new Rei(Cor.Preta, T), new Posicao(0, 4));
                T.colocarPeca(new Bispo(Cor.Preta, T), new Posicao(0, 5));
                T.colocarPeca(new Cavalo(Cor.Preta, T), new Posicao(0, 6));
                T.colocarPeca(new Torre(Cor.Preta, T), new Posicao(0, 7));

                for (int i = 0; i < T.Linhas; i++)
                {
                    T.colocarPeca(new Peao(Cor.Preta, T), new Posicao(1, i));
                }
                Tela.imprimirTabuleiro(T);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
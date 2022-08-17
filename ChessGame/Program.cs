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
                
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();
                while (!partidaDeXadrez.Terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.Tab);

                    Console.Write("\n\nOrigem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partidaDeXadrez.Tab.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.Tab, posicoesPossiveis);

                    Console.Write("\n\nDestino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partidaDeXadrez.executarMovimento(origem, destino);
                }
                
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
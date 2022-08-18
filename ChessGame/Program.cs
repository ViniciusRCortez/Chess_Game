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
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partidaDeXadrez);
                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.validarOrigem(origem);

                        bool[,] posicoesPossiveis = partidaDeXadrez.Tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaDeXadrez.Tab, posicoesPossiveis);

                        Console.WriteLine($"\n\nTurno {partidaDeXadrez.Turno}\nAguardando movimento das"
                            + $" {partidaDeXadrez.JogadorAtual}s");
                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.validarDestino(origem, destino);

                        partidaDeXadrez.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partidaDeXadrez);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
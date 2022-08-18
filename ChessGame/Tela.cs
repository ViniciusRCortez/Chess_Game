using ChessGame.tabuleiro;
using ChessGame.xadrez;
using System.Collections.Generic;

namespace ChessGame
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i}  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.Write($"\n   a b c d e f g h");
        }

        public static void imprimirPartida(PartidaDeXadrez partidaDeXadrez)
        {
            imprimirTabuleiro(partidaDeXadrez.Tab);
            imprimirPecasCapturadas(partidaDeXadrez);
            Console.WriteLine($"\n\nTurno {partidaDeXadrez.Turno}\nAguardando movimento das"
                + $" {partidaDeXadrez.JogadorAtual}s");
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partidaDeXadrez)
        {
            Console.Write($"\n\nPeças Capturadas:\nBrancas: ");
            imprimirConjunto(partidaDeXadrez.pecasCapturadas(Cor.Branca));
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\nPretas: ");
            imprimirConjunto(partidaDeXadrez.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;

        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach (Peca x in conjunto)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i}  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = fundoOriginal;
            }
            Console.Write($"\n   a b c d e f g h");
            
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else if (peca.Cor == Cor.Preta)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    throw new TabuleiroException("Peça de cor invalida para Xadrez");
                }
                Console.Write(" ");
            }
        }
    }
}

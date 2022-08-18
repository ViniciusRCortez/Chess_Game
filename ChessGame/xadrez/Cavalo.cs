
using ChessGame.tabuleiro;

namespace ChessGame.xadrez
{
    internal class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}

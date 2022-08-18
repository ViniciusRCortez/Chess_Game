using ChessGame.tabuleiro;

namespace ChessGame.xadrez
{
    internal class Rei : Peca
    {
        private PartidaDeXadrez Partida;

        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            Partida = partida;
        }

        private bool testaTorreParaRoque(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);

            //N
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //NE
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna +1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //L
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //SE
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //S
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //SO
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //S
            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //NO
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //JOGADA ESPECIAL ROQUE
            if (QteMovimentos == 0 && !Partida.Xeque)
            {
                //ROQUE PEQUENO
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (testaTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tabuleiro.peca(p1) == null && Tabuleiro.peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                //ROQUE GRANDE
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (testaTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, pos.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.peca(p1) == null && Tabuleiro.peca(p2) == null 
                        && Tabuleiro.peca(p3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }

            }
            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}

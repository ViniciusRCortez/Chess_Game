namespace ChessGame.tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QteMovimentos = 0;
        }

        public void imcrementarQteMovimentos()
        {
            QteMovimentos++;
        }

        public void decrementarQteMovimentos()
        {
            QteMovimentos--;
        }

        protected bool podeMover(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao destino)
        {
            return movimentosPossiveis()[destino.Linha, destino.Coluna];
        }
        public abstract bool[,] movimentosPossiveis();
    }
}

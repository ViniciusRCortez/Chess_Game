using ChessGame.tabuleiro;

namespace ChessGame.xadrez
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] movimentosPossiveis()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
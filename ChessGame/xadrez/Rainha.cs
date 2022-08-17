
using ChessGame.tabuleiro;

namespace ChessGame.xadrez
{
    internal class Rainha : Peca
    {
        public Rainha(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] movimentosPossiveis()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}

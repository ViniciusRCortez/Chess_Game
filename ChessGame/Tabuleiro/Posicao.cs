using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Linha);
            sb.Append(", ");
            sb.Append(Coluna);
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame.tabuleiro;
using ChessGame.xadrez;

namespace ChessGame.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            colocarPecas();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {            
            Peca p = Tab.retirarPeca(origem);
            
            p.imcrementarQteMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            validarOrigem(origem);
            executarMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }

        public void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else if (JogadorAtual == Cor.Preta)
            {
                JogadorAtual = Cor.Branca;
            }
            else
            {
                throw new TabuleiroException("Jogador de Cor Invalida");
            }
        }

        public void validarOrigem(Posicao origem)
        {
            if (!Tab.existePeca(origem))
            {
                throw new TabuleiroException("Origem invalida, não existe peça nessa posição");
            }
        }
       


        private void colocarPecas()
        {
            /*Tab.colocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('a', 8).toPosicao());
            Tab.colocarPeca(new Cavalo(Cor.Preta, Tab), new PosicaoXadrez('b', 8).toPosicao());
            Tab.colocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('c', 8).toPosicao());
            Tab.colocarPeca(new Rainha(Cor.Preta, Tab), new PosicaoXadrez('d', 8).toPosicao());
            Tab.colocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('e', 8).toPosicao());
            Tab.colocarPeca(new Bispo(Cor.Preta, Tab), new PosicaoXadrez('f', 8).toPosicao());
            Tab.colocarPeca(new Cavalo(Cor.Preta, Tab), new PosicaoXadrez('g', 8).toPosicao());
            Tab.colocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('h', 8).toPosicao());

            Tab.colocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('a', 1).toPosicao());
            Tab.colocarPeca(new Cavalo(Cor.Branca, Tab), new PosicaoXadrez('b', 1).toPosicao());
            Tab.colocarPeca(new Bispo(Cor.Branca, Tab), new PosicaoXadrez('c', 1).toPosicao());
            Tab.colocarPeca(new Rainha(Cor.Branca, Tab), new PosicaoXadrez('d', 1).toPosicao());
            Tab.colocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('e', 1).toPosicao());
            Tab.colocarPeca(new Bispo(Cor.Branca, Tab), new PosicaoXadrez('f', 1).toPosicao());
            Tab.colocarPeca(new Cavalo(Cor.Branca, Tab), new PosicaoXadrez('g', 1).toPosicao());
            Tab.colocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('h', 1).toPosicao());

            char[] linhasLetras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
 
            for (int i = 0; i < Tab.Linhas; i++)
            {
                Tab.colocarPeca(new Peao(Cor.Preta, Tab), new PosicaoXadrez(linhasLetras[i], 7).toPosicao());
                Tab.colocarPeca(new Peao(Cor.Branca, Tab), new PosicaoXadrez(linhasLetras[i], 2).toPosicao());
            }*/
            Tab.colocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('h', 1).toPosicao());
            Tab.colocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('h', 5).toPosicao());
            Tab.colocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('f', 1).toPosicao());
            Tab.colocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('e', 1).toPosicao());
            Tab.colocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('e', 8).toPosicao());
        }
    }
}

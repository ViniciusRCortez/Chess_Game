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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {            
            Peca p = Tab.retirarPeca(origem);
            
            p.imcrementarQteMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
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
            if (Tab.peca(origem) == null)
            {
                throw new TabuleiroException("Origem invalida, não existe peça nessa posição");
            }
            if (JogadorAtual != Tab.peca(origem).Cor)
            {
                throw new TabuleiroException($"Jogador da cor errada," +
                    $" era esperado uma jogada das {JogadorAtual}s");
            }
            if (!Tab.peca(origem).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Peça sem movimentos possíveis");
            }
        }

        public void validarDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de Destino Invalida");
            }
        }
       
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
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
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('h', 5, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('f', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab));
        }
    }
}

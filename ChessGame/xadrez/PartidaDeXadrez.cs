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
        public bool Xeque { get; private set; }
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
            Xeque = false;
            colocarPecas();
        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {            
            Peca p = Tab.retirarPeca(origem);
            
            p.imcrementarQteMovimentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);
            
            if (emXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se por em Xeque!");
            }
            if (emXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (emXequeMate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }           
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);
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
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                    break;
                }
            }
            return null;
        }

        public bool emXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não há Rei {cor} nessa partida");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool emXequeMate(Cor cor)
        {
            if (!emXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for ( int i = 0; i < x.Posicao.Linha; i++)
                {
                    for (int j = 0; j < x.Posicao.Coluna; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao destino = new Posicao(i, j);
                            Posicao origem = x.Posicao;
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = emXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            colocarNovaPeca('h', 7, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('b', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));
            colocarNovaPeca('a', 8, new Rei(Cor.Preta, Tab));
        }
    }
}

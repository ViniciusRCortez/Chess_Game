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
        public Peca vuneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            vuneravelEnPassant = null;
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
            
            //JOGADA ESPECIAL ROQUE PEQUENO
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tab.retirarPeca(OrigemT);
                T.imcrementarQteMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }

            //JOGADA ESPECIAL ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tab.retirarPeca(OrigemT);
                T.imcrementarQteMovimentos();
                Tab.colocarPeca(T, DestinoT);
            }
            //JOGADA ESPECIAL ENPASSANT
            if(p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.retirarPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
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
            Peca p = Tab.peca(destino);

            //JOGADA ESPECIAL PROMOÇÃO
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.retirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(p.Cor, Tab);
                    Tab.colocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
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

            //JOGADA ESPECIAL ENPASSANT
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vuneravelEnPassant = p;
            }
            else
            {
                vuneravelEnPassant = null;
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
            
            //JOGADA ESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tab.retirarPeca(DestinoT);
                T.decrementarQteMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }
            //JOGADA ESPECIAL ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao OrigemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tab.retirarPeca(DestinoT);
                T.decrementarQteMovimentos();
                Tab.colocarPeca(T, OrigemT);
            }
            //JOGADA ESPECIAL ENPASSANT
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vuneravelEnPassant)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4 , destino.Coluna);
                    }
                    Tab.colocarPeca(peao, posP);                    
                }
            }
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
            if (!Tab.peca(origem).movimentoPossivel(destino))
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
            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));

            colocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));

            char[] linhasLetras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            foreach (char linha in linhasLetras)
            {
                colocarNovaPeca(linha, 7, new Peao(Cor.Preta, Tab, this));
                colocarNovaPeca(linha, 2, new Peao(Cor.Branca, Tab, this));
            }             

        }
    }
}

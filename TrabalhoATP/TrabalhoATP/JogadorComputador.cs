using System;

namespace TrabalhoATP
{
    internal class JogadorComputador
    {
        private char[,] tabuleiro;
        private int pontuacao;
        private int numTirosDados;
        private Posicao[] posTirosDados;

        public JogadorComputador(int linhas, int colunas)
        {
            tabuleiro = new char[linhas, colunas];
            for(int i = 0; i < linhas; i++)
            {
                for(int j = 0; j < colunas; j++)
                {
                    tabuleiro[i, j] = 'A';
                }
            }
            pontuacao = 0;
            numTirosDados = 0;
            posTirosDados = new Posicao[linhas*colunas];
        }

        public char[,] Tabuleiro
        {
            get { return tabuleiro; } set { tabuleiro = value; }
        }
        public int Pontuacao
        {
            get { return pontuacao; } set { pontuacao = value; }
        }

        public int NumTirosDados
        {
            get { return numTirosDados; } set { numTirosDados = value; }
        }

        public Posicao[] PosTirosDados
        {
            get { return posTirosDados; } set { posTirosDados = value; }
        }

        public Posicao EscolherAtaque()
        {
            Random r = new Random();
            Posicao pos = new Posicao();
            bool repetido = false;

            pos.Linha = r.Next(0, 11);
            pos.Coluna = r.Next(0, 11);
            for (int i = 0; i < posTirosDados.Length && !repetido; i++)
            {

                if (posTirosDados[i] == pos)
                {
                    repetido = true;
                }
            }

            while (repetido)
            {
                pos.Linha = r.Next(0, 11);
                pos.Coluna = r.Next(0, 11);
                int cont = 0;
                for (int i = 0; i < posTirosDados.Length; i++)
                {

                    if (posTirosDados[i] == pos)
                    {
                        repetido = true;
                        cont++;
                    }
                }
                if (cont == 0)
                {
                    repetido = false;
                }
            }

            for(int i = 0; i<posTirosDados.Length; i++)
            {
                if (posTirosDados[i] == null)
                {
                    posTirosDados[i] = pos;
                    i = posTirosDados.Length; // so pra sair (?)
                }
            }
           
            return pos;
        }

        public bool ReceberAtaque(Posicao pos)
        {
            char[] embarcacoes = {'S', 'H', 'C', 'E', 'P'};

            for(int i=0; i<embarcacoes.Length; i++)
            {
                if (tabuleiro[pos.Linha, pos.Coluna] == embarcacoes[i])
                {
                    tabuleiro[pos.Linha, pos.Coluna] = 'T';
                    return true;
                } 
            }
            if (tabuleiro[pos.Linha,pos.Coluna] == 'A')
            {
                tabuleiro[pos.Linha, pos.Coluna] = 'X';
            }
            return false;
        }

        public void ImprimirTabuleiroJogador()
        {
            for(int l = 0; l< tabuleiro.GetLength(0); l++)
            {
                for(int c =0; c<tabuleiro.GetLength(1); c++)
                {
                    Console.Write(tabuleiro[l, c] + "\t");
                }
                Console.WriteLine();
            }
        }

        public void ImprimirTabuleiroAdversario()
        {
            for (int l = 0; l < tabuleiro.GetLength(0); l++)
            {
                for(int c=0; c < tabuleiro.GetLength(1); c++)
                {
                    if (tabuleiro[l,c] == 'S' || tabuleiro[l, c] == 'H' || tabuleiro[l, c] == 'C' || tabuleiro[l, c] == 'E' || tabuleiro[l, c] == 'P')
                    {
                        tabuleiro[l, c] = 'A';
                    }
                    Console.Write(tabuleiro[l,c] + "\t");
                }
                Console.WriteLine();
            }
        }

        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao pos)
        {
            if (tabuleiro[pos.Linha, pos.Coluna] == 'A') // se n tiver outra embarc
            {
                if (tabuleiro.GetLength(1) - pos.Coluna >= embarcacao.Tamanho) // se couber
                {
                    // se as proximas pos tbm n tiverem embarc
                    int cont = 1; bool vazio = true;
                    for (int i = pos.Coluna + 1; cont < embarcacao.Tamanho && vazio; i++) // se n puder embarcacao do lado da outra, mudar pra cont <= embarcacao.Tamanho
                    {
                        if (tabuleiro[pos.Linha, i] != 'A')
                        {
                            vazio = false;
                        }
                    }
                    if (vazio)
                    {
                        tabuleiro[pos.Linha, pos.Coluna] = embarcacao.Nome[0];
                        return true;
                    }
                    else // se outra posiçao necessaria ja tiver embarcacao
                    {
                        return false;
                    }

                }
                else // se n couber
                {
                    return false;
                }
            }
            else // se ja estiver ocupado
            {
                return false;
            }
        }
    }
}

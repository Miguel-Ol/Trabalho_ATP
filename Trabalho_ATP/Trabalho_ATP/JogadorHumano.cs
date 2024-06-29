using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoATP
{
    internal class JogadorHumano
    {
        private char[,] tabuleiro;
        private int pontuacao;
        private int numTirosDados;
        private Posicao[] posTirosDados;
        private string nickname;
        public JogadorHumano(int linha, int coluna, string nickname)
        {
            tabuleiro = GerarTabuleiro(linha, coluna);
            pontuacao = 0;
            numTirosDados = 0;
            posTirosDados = new Posicao[linha * coluna]; // limite de posições
            this.nickname = nickname;
        }
        public char[,] GerarTabuleiro(int linha, int coluna)
        {
            char[,] tab = new char[linha, coluna];
            for (int i = 0; i < linha; i++)
            {
                for (int j = 0; j < coluna; j++)
                {
                    tab[i, j] = 'A';
                }
            }
            return tab;
        }
        public string GerarNickname(string nomeCompleto) //nome fornecido no main
        {
            string[] nome = nomeCompleto.Split(' '); //tranformo em um vetor, fazendo o split dos espaços
            if (nome.Length == 0) //como, sempre o primeiro nome é SEMPRE a primeira posição
            {
                return string.Empty; //devolvo um vazio, para armazenar no proximo for
            }
            string nickname = nome[0];
            for (int i = 1; i < nome.Length; i++)
            {
                nickname += nome[i][0];
            }
            return nickname;
        }
        public Posicao EscolherAtaque()
        {
            bool posicaoValida = false;
            int linha, coluna;
            Posicao posicaoTiro = new Posicao();
            while (!posicaoValida)
            {
                Console.WriteLine("Escolha a LINHA da posição do tiro: "); //arrumar para esse modelo
                linha = int.Parse(Console.ReadLine());
                Console.WriteLine("Escolha a COLUNA da posição do tiro: ");
                coluna = int.Parse(Console.ReadLine());
                if (linha >= 0 && linha < tabuleiro.GetLength(0) && coluna >= 0 && coluna < tabuleiro.GetLength(1)) //olha se está no lim do tab
                {
                    posicaoTiro.Linha = linha;
                    posicaoTiro.Coluna = coluna;
                    posicaoValida = true;
                    for (int i = 0; i < numTirosDados; i++)
                    {
                        if (posTirosDados[i].Linha==posicaoTiro.Linha && posTirosDados[i].Coluna == posicaoTiro.Coluna) //para comparar
                        {
                            Console.WriteLine("Posição já utilizada, tente novamente!!");
                            posicaoValida = false;
                            i = numTirosDados;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Posição INVÁLIDA! Informe novamenete.");
                }
            }
            posTirosDados[numTirosDados] = posicaoTiro; //se valida, bota no vet 
            numTirosDados++;
            return posicaoTiro;
        }
        public bool ReceberAtaque(Posicao pos)
        {
            char[] embarcacoes = { 'S', 'H', 'C', 'E', 'P' };

            for (int i = 0; i < embarcacoes.Length; i++)
            {
                if (tabuleiro[pos.Linha, pos.Coluna] == embarcacoes[i])
                {
                    tabuleiro[pos.Linha, pos.Coluna] = 'T';
                    return true;
                }
            }
            if (tabuleiro[pos.Linha, pos.Coluna] == 'A')
            {
                tabuleiro[pos.Linha, pos.Coluna] = 'X';
            }
            return false;
        }
        public void ImprimirTabuleiroJogador()
        {
            Console.WriteLine($"Tabuleiro {nickname}: ");
            for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
                {
                    Console.Write(tabuleiro[linha, coluna] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void ImprimirTabuleiroAdversario()
        {
            for (int l = 0; l < tabuleiro.GetLength(0); l++)
            {
                for (int c = 0; c < tabuleiro.GetLength(1); c++)
                {
                    if (tabuleiro[l, c] == 'T')
                    {
                        Console.Write('T' + "\t");
                    }
                    else if (tabuleiro[l, c] == 'X')
                    {
                        Console.Write('X' + "\t");
                    }
                    else
                    {
                        Console.Write('A' + "\t");
                    }
                }
                Console.WriteLine();
            }
        }
        public bool AdicionarEmbarcacao(Embarcacao embarcacao, Posicao pos)
        {
            if (tabuleiro[pos.Linha, pos.Coluna] == 'A')
            {
                if (tabuleiro.GetLength(1) - pos.Coluna >= embarcacao.Tamanho)
                {
                    int cont = 1;
                    bool vazio = true;
                    for (int i = pos.Coluna + 1; cont < embarcacao.Tamanho && vazio; i++)
                    {
                        if (tabuleiro[pos.Linha, i] != 'A')
                        {
                            vazio = false;
                        }
                        cont++;
                    }
                    if (vazio)
                    {
                        int cont2 = 0; int i = pos.Coluna;
                        while (cont2 < embarcacao.Tamanho)
                        {
                            tabuleiro[pos.Linha, i] = embarcacao.Nome[0];
                            i++;
                            cont2++;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public char[,] Tabuleiro
        {
            get { return tabuleiro; }
            set { tabuleiro = value; }
        }
        public int Pontuacao
        {
            get { return pontuacao; }
            set { pontuacao = value; }
        }
        public int NumTirosDados
        {
            get { return numTirosDados; }
            set { numTirosDados = value; }
        }
        public Posicao[] PosTirosDados
        {
            get { return posTirosDados; }
            set { posTirosDados = value; }
        }
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }


    }
}

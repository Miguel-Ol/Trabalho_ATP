using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_ATP;
using TrabalhoATP;

namespace TrabalhoATP
{
    internal class BatalhaNaval
    {
        static void Main(string[] args)
        {
            Embarcacao embarcaçãoVez = new Embarcacao("Submarino", 1);
            Posicao posiçãoVez = new Posicao();
            bool segue;
            int linhaPosição, colunaPosição, cont = 0, contEmbarcações = 0, quantEmbarcaçãoVez = 3, pontuaçãoTotal = 22;
            Random posiçãoAleatoria = new Random();

            string[] jogadasJogador = new string[100];
            string[] jogadasComputador = new string[100];

            Console.WriteLine("Informe seu nome completo: ");
            string nomeCompleto = Console.ReadLine();

            JogadorHumano jogadorMain = new JogadorHumano(10, 10, nomeCompleto);
            jogadorMain.Nickname = jogadorMain.GerarNickname(jogadorMain.Nickname);
            JogadorComputador computadorMain = new JogadorComputador(10, 10);


            while (contEmbarcações < 5)
            {
                switch (contEmbarcações)
                {
                    case 0:
                        embarcaçãoVez = new Embarcacao("Submarino", 1);
                        quantEmbarcaçãoVez = 3;
                        break;
                    case 1:
                        embarcaçãoVez = new Embarcacao("Hidroavião", 2);
                        quantEmbarcaçãoVez = 2;
                        break;
                    case 2:
                        embarcaçãoVez = new Embarcacao("Cruzador", 3);
                        quantEmbarcaçãoVez = 2;
                        break;
                    case 3:
                        embarcaçãoVez = new Embarcacao("Encouraçado", 4);
                        quantEmbarcaçãoVez = 1;
                        break;
                    case 4:
                        embarcaçãoVez = new Embarcacao("Porta-aviões", 5);
                        quantEmbarcaçãoVez = 1;
                        break;
                }

                while (cont < quantEmbarcaçãoVez)
                {
                    Console.WriteLine($"{jogadorMain.Nickname}, Informe a linha do {cont + 1}º {embarcaçãoVez.Nome}");
                    linhaPosição = int.Parse(Console.ReadLine());
                    Console.WriteLine($"{jogadorMain.Nickname}, Informe a coluna do {cont + 1}º {embarcaçãoVez.Nome}");
                    colunaPosição = int.Parse(Console.ReadLine());
                    if (linhaPosição < 10 && colunaPosição < 10)
                    {
                        posiçãoVez.Linha = linhaPosição;
                        posiçãoVez.Coluna = colunaPosição;

                        segue = jogadorMain.AdicionarEmbarcacao(embarcaçãoVez, posiçãoVez);
                        if (segue)
                        {
                            cont++;
                            jogadorMain.ImprimirTabuleiroJogador();
                        }

                        else
                        {
                            Console.WriteLine("Posição já utilizada, tente novamente!!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Posição INVÁLIDA! Informe novamente.");
                    }
                }
                contEmbarcações++;
                cont = 0;
                
            }


            StreamReader frotaComputador = new StreamReader("frotaComputador.txt", Encoding.UTF8);
            string linhaFrota = frotaComputador.ReadLine();
            string EmbarcaçãoVez;
            string[] posiçãoEmbarcaçãoComputador;
            while (linhaFrota != null)
            {
                EmbarcaçãoVez = linhaFrota.Remove(1);
                switch (EmbarcaçãoVez) 
                {
                    case "S":
                        embarcaçãoVez = new Embarcacao("Submarino", 1);
                        break;
                    case "H":
                        embarcaçãoVez = new Embarcacao("Hidroavião", 2);
                        break;
                    case "C":
                        embarcaçãoVez = new Embarcacao("Cruzador", 3);
                        break;
                    case "E":
                        embarcaçãoVez = new Embarcacao("Encouraçado", 4);
                        break;
                    case "P":
                        embarcaçãoVez = new Embarcacao("Porta-aviões", 5);
                        break;
                }
                posiçãoEmbarcaçãoComputador = linhaFrota.Split(';');
                posiçãoVez.Linha = int.Parse(posiçãoEmbarcaçãoComputador[1]);
                posiçãoVez.Coluna = int.Parse(posiçãoEmbarcaçãoComputador[2]);
                segue = computadorMain.AdicionarEmbarcacao(embarcaçãoVez, posiçãoVez);
                if (segue)
                {
                    linhaFrota = frotaComputador.ReadLine();
                }
                else
                    Console.WriteLine("posição para embarcação do computador inválida");
            }



            int contTirosJogador = 1;
            int contTirosComputador = 1;
            Console.WriteLine("Pontuação total para vencer: " + pontuaçãoTotal);

            segue = true;
            while (jogadorMain.Pontuacao != pontuaçãoTotal && computadorMain.Pontuacao != pontuaçãoTotal)
            {

                if (jogadorMain.Pontuacao == pontuaçãoTotal || jogadorMain.Pontuacao == pontuaçãoTotal)
                    segue = false;
                else
                {
                    segue = true;
                    Console.WriteLine();
                }


                while (segue && jogadorMain.Pontuacao != pontuaçãoTotal)
                {
                    Console.WriteLine("Forma atual do tabuleiro computador:");
                    computadorMain.ImprimirTabuleiroAdversario();
                    Console.Write($"{jogadorMain.Nickname}, ");
                    posiçãoVez = jogadorMain.EscolherAtaque();
                    segue = computadorMain.ReceberAtaque(posiçãoVez);
                    if (segue)
                    {
                        Console.WriteLine("Embarcação acertada!");
                        jogadorMain.Pontuacao++;
                        Console.WriteLine($"Pontuação {jogadorMain.Nickname}: " + jogadorMain.Pontuacao);
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma embarcação acertada.");
                    }
                    jogadasJogador[contTirosJogador]=$"Tiro {contTirosJogador}: ({posiçãoVez.Linha},{posiçãoVez.Coluna})";
                    contTirosJogador++;
                }

                if (jogadorMain.Pontuacao == pontuaçãoTotal || jogadorMain.Pontuacao == pontuaçãoTotal)
                    segue = false;
                else
                {
                    segue = true;
                    Console.WriteLine();
                }

                while (segue && computadorMain.Pontuacao != pontuaçãoTotal)
                {
                    posiçãoVez = computadorMain.EscolherAtaque();
                    Console.WriteLine($"Ataque do computador em {posiçãoVez.Linha},{posiçãoVez.Coluna}");
                    segue = jogadorMain.ReceberAtaque(posiçãoVez);
                    if (segue)
                    {
                        Console.WriteLine($"Embarcação de {jogadorMain.Nickname} acertada!");
                        computadorMain.Pontuacao++;
                        Console.WriteLine($"Pontuação computador: " + computadorMain.Pontuacao);
                    }
                    else
                    {
                        Console.WriteLine($"Nenhuma embarcação de {jogadorMain.Nickname} acertada.");
                    }
                    Console.WriteLine("Forma atual tabuleiro jogador:");
                    jogadorMain.ImprimirTabuleiroAdversario();
                    jogadasComputador[contTirosComputador]=$"Tiro {contTirosComputador}: ({posiçãoVez.Linha},{posiçãoVez.Coluna})";
                    contTirosComputador++;
                }
            }

            Console.WriteLine();

            StreamWriter jogadas = new StreamWriter("jogadas.txt", false, Encoding.UTF8);
            int contLinhasArquivo = 1;

            if (jogadorMain.Pontuacao == pontuaçãoTotal)
            {
                Console.WriteLine($"{jogadorMain.Nickname} venceu!");
                while(jogadasJogador[contLinhasArquivo] != null)
                {
                    jogadas.WriteLine(jogadasJogador[contLinhasArquivo]);
                    contLinhasArquivo++;
                }

            }
            else
            {
                Console.WriteLine("Computador venceu!");
                while (jogadasComputador[contLinhasArquivo] != null)
                {
                    jogadas.WriteLine(jogadasComputador[contLinhasArquivo]);
                    contLinhasArquivo++;
                }
                
            }
            jogadas.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_ATP
{
    internal class BatalhaNaval
    {
        static void Main(string[] args)
        {
            Embarcacao embarcaçãoVez = new Embarcacao("Submarino",1);
            Posicao posiçãoVez = new Posicao();
            bool segue;
            int linhaPosição,colunaPosição, cont = 0,contEmbarcações=0, quantEmbarcaçãoVez=3, pontuaçãoTotal=0;
            Random posiçãoAleatoria = new Random();

            StreamWriter jogadasJogador = new StreamWriter("jogadasJogador.txt", false, Encoding.UTF8);
            StreamWriter jogadasComputador = new StreamWriter("jogadasComputador.txt", false, Encoding.UTF8);

            Console.WriteLine("informe seu nome completo");
            string nomeCompleto = Console.ReadLine();
            
            JogadorHumano jogadorMain = new JogadorHumano(10,10,nomeCompleto);
            jogadorMain.Nickname = jogadorMain.GerarNickname(jogadorMain.Nickname); 
            JogadorComputador computadorMain = new JogadorComputador(10,10);


            while (contEmbarcações < 5)
            {
                switch (contEmbarcações)
                {
                    case 0:
                        embarcaçãoVez = new Embarcacao("Submarino",1);
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
                    Console.WriteLine($"{jogadorMain.Nickname}, informe a linha do {cont + 1}º {embarcaçãoVez.Nome}");
                    linhaPosição = int.Parse(Console.ReadLine());
                    Console.WriteLine($"{jogadorMain.Nickname}, informe a coluna do {cont + 1}º {embarcaçãoVez.Nome}");
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
                            Console.WriteLine("posição inválida");
                    }
                    else
                        Console.WriteLine("posição inválida");
                }
                /*while (cont < quantEmbarcaçãoVez)
                {
                    posiçãoVez.Linha = posiçãoAleatoria.Next(0, 10);
                    posiçãoVez.Coluna = posiçãoAleatoria.Next(0, 10);
                    segue = jogadorMain.AdicionarEmbarcacao(embarcaçãoVez, posiçãoVez);
                    if (segue)
                    {
                        cont++;
                    }
                }*/
                cont = 0;

                while (cont < quantEmbarcaçãoVez)
                {
                    posiçãoVez.Linha = posiçãoAleatoria.Next(0,10);
                    posiçãoVez.Coluna = posiçãoAleatoria.Next(0, 10);
                    segue = computadorMain.AdicionarEmbarcacao(embarcaçãoVez,posiçãoVez);
                    if (segue)
                    {
                        cont++;
                    }
                }
                pontuaçãoTotal += embarcaçãoVez.Tamanho * quantEmbarcaçãoVez;
                contEmbarcações++;
                cont = 0;
            }

            int contTirosJogador=1;
            int contTirosComputador=1;
            Console.WriteLine("pontuação total para vencer: " + pontuaçãoTotal);


            while (jogadorMain.Pontuacao != pontuaçãoTotal && computadorMain.Pontuacao != pontuaçãoTotal)
            {
                /*Console.WriteLine("teste");
                computadorMain.ImprimirTabuleiroJogador();*/
                segue = true;


                while (segue && jogadorMain.Pontuacao != pontuaçãoTotal)
                {
                    Console.WriteLine("forma atual do tabuleiro computador:");
                    computadorMain.ImprimirTabuleiroAdversario();
                    Console.Write($"{jogadorMain.Nickname}, ");
                    posiçãoVez = jogadorMain.EscolherAtaque();
                    segue = computadorMain.ReceberAtaque(posiçãoVez);
                    if (segue)
                    {
                        Console.WriteLine("embarcação acertada!");
                        jogadorMain.Pontuacao++;
                        Console.WriteLine($"pontuação {jogadorMain.Nickname}: "+ jogadorMain.Pontuacao);
                    }
                    else
                    {
                        Console.WriteLine("nenhuma embarcação acertada.");
                    }
                    jogadasJogador.WriteLine($"tiro {contTirosJogador}: ({posiçãoVez.Linha},{posiçãoVez.Coluna})");
                    contTirosJogador++;
                } 

                Console.WriteLine();
                segue = true;


                while (segue && computadorMain.Pontuacao != pontuaçãoTotal)
                {
                    posiçãoVez = computadorMain.EscolherAtaque();
                    Console.WriteLine($"ataque do computador em {posiçãoVez.Linha},{posiçãoVez.Coluna}");
                    segue = jogadorMain.ReceberAtaque(posiçãoVez);
                    if (segue)
                    {
                        Console.WriteLine($"embarcação de {jogadorMain.Nickname} acertada!");
                        computadorMain.Pontuacao++;
                        Console.WriteLine($"pontuação computador: " + computadorMain.Pontuacao);
                    }
                    else
                    {
                        Console.WriteLine($"nenhuma embarcação de {jogadorMain.Nickname} acertada.");
                    }
                    Console.WriteLine("forma atual tabuleiro jogador:");
                    jogadorMain.ImprimirTabuleiroAdversario();
                    jogadasComputador.WriteLine($"tiro {contTirosComputador}: ({posiçãoVez.Linha},{posiçãoVez.Coluna})");
                    contTirosComputador++;
                } 
                Console.WriteLine();
            }
            jogadasJogador.Close();
            jogadasComputador.Close();

            if (jogadorMain.Pontuacao == pontuaçãoTotal)
            {
                Console.WriteLine($"{jogadorMain.Nickname} venceu!");
            }
            else
                Console.WriteLine("computador venceu!");

  

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoATP
{
    internal class JogadorHumano
    {
        private char [,] tabuleiro;
private int pontuacao;
private int numTirosDados;
private Posicao [] posTirosDados; //iv) o vetor com as posições dos tiros dados(posTirosDados) deve ser instanciado?????
private string nickname;
public JogadorHumano (int linha,int coluna, Posicao[] posTirosDados, string nickname)
{
    tabuleiro = GerarTabuleiro(linha, coluna);
    pontuacao = 0;
    numTirosDados = 0;
    this.posTirosDados = posTirosDados;
    this.nickname = nickname;
}
public char[,] GerarTabuleiro( int linha, int coluna)
{
    char[,] tab = new char[linha, coluna];
    for (int i = 0; i < linha; i++)
    {
        for(int j = 0; j < coluna; j++)
        {
            tab[i, j] = 'A' ;
        }
    }
    return tab;
} 
private string GerarNickname (string nomeCompleto)
{
    string[] nome;
    string nickname = " ";
    nome = nickname.Split (' '); //ou REMOVE+REPLACE
    for (int i = 0;i < nome.Length;i++)
    {
        Console.WriteLine(nome[i]);
    }
    return nickname;
}
public char[,] Tabuleiro
{
    get { return tabuleiro; }
    set { tabuleiro = value;}
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
    set { posTirosDados= value; }
}
public string Nickname 
{ 
    get { return nickname; } 
    set {  nickname = value; } 
}
    }
}

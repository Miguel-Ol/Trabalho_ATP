using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_ATP
{
    internal class Posicao
    {
        private int linha;
        public int Linha
        {
            get { return linha; }
            set { linha = value; }
        }
        private int coluna;
        public int Coluna
        {
            get { return coluna; }
            set { coluna = value; }
        }
    }
}

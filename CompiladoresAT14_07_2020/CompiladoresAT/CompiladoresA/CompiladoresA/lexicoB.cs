using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class lexicoB
    {
        string[] texto;
        string [] linea;
        string[,] TablaId;
        string[,] Tablanum;
        List<char> ligaId= new List<char>();
        List<char> ligaNum = new List<char>();
        List<char> EdosId = new List<char>();
        List<char> Edosnum = new List<char>();
        List<string> reservadas = new List<string>();
        List<string> Lexemas = new List<string>();
        List<string> Tokens = new List<string>();
        List<string> Errores = new List<string>();
        List<char> acepid = new List<char>();
        List<char> acenum = new List<char>();
        public lexicoB(string [] lineas,string [,] ids,string [,] nums,List<char>lid,List<char>lnum,List<char> edid,List<char> ednum, List<char> acid, List<char> acnum)
        {
            texto = lineas;
            TablaId = ids;
            Tablanum = nums;
            ligaId = lid;
            ligaNum = lnum;
            EdosId = edid;
            Edosnum = ednum;
            acepid = acid;
            acenum = acnum;
            reservadas.Add("-");
            reservadas.Add("(");
            reservadas.Add(")");
            reservadas.Add("*");
            reservadas.Add(":=");
            reservadas.Add("/");
            reservadas.Add(";");
            reservadas.Add("+");
            reservadas.Add("<");
            reservadas.Add("=");
            reservadas.Add(">");
            reservadas.Add("read");
            reservadas.Add("end");
            reservadas.Add("if");
            reservadas.Add("repeat");
            reservadas.Add("else");
            reservadas.Add("then");
            reservadas.Add("until");
            reservadas.Add("write");
        }

        public void Clasifica()
        {
            int index = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].Contains(";"))
                {
                    index = texto[i].IndexOf(';');
                    if (texto[i][index - 1] != ' ')
                    {
                        texto[i] = texto[i].Insert(index, " ");
                    }
                }
                if (texto[i].Length > 1)
                    while (texto[i][0] == ' ')
                    {
                        if (texto[i].Length == 1)
                            break;
                        texto[i] = texto[i].Remove(0, 1);
                    }
                linea = texto[i].Split(' ');

                for(int j=0;j<linea.Length;j++)
                {
                    if(reservadas.Contains(linea[j]))
                    {
                        Lexemas.Add(linea[j]);
                        Tokens.Add(linea[j]);
                    }
                    else
                    {
                        if(VerificaLenguajeId(TablaId,linea[j]))
                        {
                            Lexemas.Add("identificador");
                            Tokens.Add(linea[j]);
                        }
                        else
                        {
                            if (VerificaLenguajeNum(Tablanum, linea[j]))
                            {
                                Lexemas.Add("Numero");
                                Tokens.Add(linea[j]);
                            }
                            else
                            {
                                Lexemas.Add("Error");
                                Tokens.Add(linea[j]);
                                Errores.Add(linea[j]);
                            }
                        }
                    }
                }

            }
        }

        public bool VerificaLenguajeId(string[,] conjunto, string cadena)
        {
            bool res = false;
            int index = 0;
            string edo = "A";
            for (int i = 0; i < cadena.Length; i++)
            {
                if (ligaId.Contains(cadena[i]))
                    index = ligaId.IndexOf(cadena[i]);
                else
                {
                    res = false; break;
                }
                edo = conjunto[EdosId.IndexOf(Convert.ToChar(edo)), index];
                if (edo != " " && edo != null)
                {
                    res = true;
                }
                else
                { res = false; break; }
            }
            if (acepid.Contains(Convert.ToChar(edo)))
                return res;
            else
                return false;
        }

        public bool VerificaLenguajeNum(string[,] conjunto, string cadena)
        {
            bool res = false;
            int index = 0;
            string edo = "A";
            for (int i = 0; i < cadena.Length; i++)
            {
                if (ligaNum.Contains(cadena[i]))
                    index = ligaNum.IndexOf(cadena[i]);
                else
                {
                    res = false; break;
                }
                edo = conjunto[Edosnum.IndexOf(Convert.ToChar(edo)), index];
                if (edo != " " && edo != null)
                {
                    res = true;
                }
                else
                { res = false; break; }
            }
            if (acenum.Contains(Convert.ToChar(edo)))
                return res;
            else
                return false;
        }

        public List<string> Lexema
        {
            get { return Lexemas; }
        }

        public List<string> Token
        {
            get { return Tokens; }
        }

        public List<string> Error
        {
            get { return Errores; }
        }
    }
}

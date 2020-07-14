using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class AFD
    {
        List<string[]> Afn;
        List<char> Destados=new List<char>();
        List<char> trans ;
        Dictionary<string, char> Destados2 = new Dictionary<string, char>();
        List<string> nodosE = new List<string>();
        int e = 65;
        List<string> columnas;
        List<char> Aceptar = new List<char>(); 
        public AFD(List<string []> transAfn,List<char> lig,List<string> enc)
        {
            Afn = new List<string[]>(transAfn);
            trans = new List<char>(lig);
            columnas = new List<string>(enc);
        }

        public void Genera_Estados()
        {
            List<string> cerr = new List<string>();
            char t;
            int x;
            string transi="";
            int cont = 0;
            string nodos ,nodosaux= "";
             nodos=Cerradura(Afn[0][0].ToString());
            if(nodos!= "ɸ")
            {
                Destados2.Add(nodos,(char)e);
                Destados.Add((char)e);
                cerr.Add(nodos);
            }
            for (int j = 0; j < Destados.Count; j++)
            {
                nodos = cerr[j];
                for (int i = 0; i < trans.Count; i++)
                {
                    t = trans[i];
                    nodosaux = mover(Destados[j], t, nodos);
                   if (nodosaux != "ɸ" && nodosaux != "" && !cerr.Contains(nodosaux))
                    {
                        nodosaux = Cerradura(nodosaux);
                        if (!Destados2.ContainsKey(nodosaux))
                        {
                            transi += Destados[j].ToString() + t.ToString();
                            e++;
                            transi += ((char)e).ToString();
                            nodosE.Add(transi);
                            Destados2.Add(nodosaux, (char)e);
                            Destados.Add((char)e);
                            cerr.Add(nodosaux);
                            transi = "";
                        }
                        else
                        {
                            transi+= Destados[j].ToString() + t.ToString();
                            int index = cerr.IndexOf(nodosaux);
                            transi += Destados[index];
                            nodosE.Add(transi);
                            transi = "";
                        }
                    }
                }
            }
            for(int i=0;i<cerr.Count;i++)
            {
                if(cerr[i].Contains(Afn[Afn.Count-1][0]))
                {
                    Aceptar.Add(Destados[i]);
                }
            }
        }

        public string Cerradura(string edos)
        {
            string nodos = edos;
            string aux = "";
                string[] auxiliar = new string[50];
            string[] cad = { "" };
            if (nodos.Contains("{"))
            {
                nodos = nodos.Remove(0, 1);
                nodos = nodos.Remove(nodos.Length - 1, 1);
            }
            
            List<string> nods = new List<string>(nodos.Split(',')) ;
            
            for(int j=0;j<nods.Count;j++)
            {
                for (int i = 0; i < Afn.Count; i++)
                {
                    cad = Afn[i];
                    if (cad[0] == nods[j] && cad[cad.Length - 1] != "ɸ")
                    {
                        aux = cad[cad.Length - 1];
                        aux = aux.Remove(0, 1);
                        aux = aux.Remove(aux.Length - 1, 1);
                        nodos += ","+ aux;
                        auxiliar = aux.Split(',');
                        for (int x = 0; x < auxiliar.Length; x++)
                            nods.Add(auxiliar[x]);

                    }
                }
            }
            return nodos;
        }

        public string mover(char E,char t,string nodos)
        {
            string Ce="";
            string[] nods = nodos.Split(',');
            for (int i=0;i<Afn.Count;i++)
            {
                for(int j=0;j<nods.Length;j++)
                {
                    if (Afn[i][0] == nods[j])
                    {
                        for (int c = 0; c < columnas.Count; c++)
                        {
                            if (columnas[c] == t.ToString()&& Afn[i][c].ToString()!= "ɸ")
                            {
                                Ce += Afn[i][c];
                            }
                        }
                    }
                }
            }
            return Ce;
        }


        public bool VerificaLenguaje(string [,] conjunto,string cadena)
        {
            bool res = false;
            int index = 0;
            string edo = "A";
            for(int i=0;i<cadena.Length;i++)
            {
                if(trans.Contains(cadena[i]))
                index = trans.IndexOf(cadena[i]);
                else
                {
                    res = false; break;
                }
                edo = conjunto[Destados.IndexOf(Convert.ToChar(edo)),index+1];
                if (edo != " " && edo != null)
                {
                    res = true;
                } else
                { res = false; break; }
            }
            if (Aceptar.Contains(Convert.ToChar(edo)))
                return res;
            else
                return false;
        }
        public Dictionary<string,char> Estados
        {
            get { return Destados2; }
        }        
        public List<char> DestadosF
        {
            get { return Destados; }
        }
        public List<string> DestadosT
        {
            get { return nodosE; }
        }

        public List<char> Aceptacion
        {
            get { return Aceptar; }
        }
    }
}
;
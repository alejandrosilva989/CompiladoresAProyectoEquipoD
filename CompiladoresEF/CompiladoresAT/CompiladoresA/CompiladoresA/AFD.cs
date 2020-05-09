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
            int cont = 0;
            string nodos ,nodosaux= "";
             nodos=Cerradura0();
            nodosE.Add(nodos);
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
                    if (j == 5)
                         x = 10;
                    t = trans[i];
                    nodosaux = mover(Destados[j], t, nodos);
                    if (nodosaux != "ɸ" && nodosaux != "" && !cerr.Contains(nodosaux))
                    {
                        nodosaux = Cerradura(nodosaux);
                        if (!Destados2.ContainsKey(nodosaux))
                        {
                            e++;
                            Destados2.Add(nodosaux, (char)e);
                            Destados.Add((char)e);
                            cerr.Add(nodosaux);
                        }
                    }
                }
            }
        }

        public string Cerradura0()
        {
            string nodos="0";
            string [] cad= {""};
            for(int i=0;i<Afn.Count;i++)
            {
                cad = Afn[i];

                if(cad[i]=="0"&& cad[cad.Length - 1]!= "ɸ")
                {
                    nodos+=cad[cad.Length-1];
                }
            }
            return nodos;
        }

        public string Cerradura(string edos)
        {
            string nodos = edos;
            string[] cad = { "" };
            for (int i = 0; i < Afn.Count; i++)
            {
                cad = Afn[i];
                for(int j=0;j<edos.Length;j++)
                if (cad[0] == edos[j].ToString()&& cad[cad.Length - 1]!= "ɸ")
                {
                    nodos += cad[cad.Length - 1];
                }
            }
            return nodos;
        }

        public string mover(char E,char t,string nodos)
        {
            string Ce="";
            for(int i=0;i<Afn.Count;i++)
            {
                for(int j=0;j<nodos.Length;j++)
                {
                   if( Afn[i][0] ==nodos[j].ToString())
                    {
                        for(int c=0;c<columnas.Count;c++)
                        {
                           if( columnas[c]==t.ToString())
                            {
                                Ce = Afn[i][c];
                               
                            }
                        }
                    }
                }
            }
            return Ce;
        }

        public Dictionary<string,char> Estados
        {
            get { return Destados2; }
        }        
        public List<char> DestadosF
        {
            get { return Destados; }
        }
    }
}
;
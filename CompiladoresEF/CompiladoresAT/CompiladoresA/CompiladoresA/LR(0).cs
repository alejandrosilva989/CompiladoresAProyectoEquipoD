using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class LR_0_
    {
        List<string> gramatica;
        List<string> gramaticales=new List<string>();
        List<List<string>> res = new List<List<string>>();
        List<string> trans = new List<string>();

        public LR_0_()
        {
            gramatica = new List<string>();
             gramatica.Add("X->.P");
             gramatica.Add("P->S");
             gramatica.Add("S->S;T");
             gramatica.Add("S->T");
             gramatica.Add("T->A");
             gramatica.Add("T->B"); 
             gramatica.Add("T->C"); 
             gramatica.Add("T->D");
             gramatica.Add("T->H");
             gramatica.Add("A->iEtSe");
             gramatica.Add("A->iEtSsSe");
             gramatica.Add("B->rSuE");
             gramatica.Add("C->c,E");
             gramatica.Add("D->dc"); 
             gramatica.Add("H->wE");
             gramatica.Add("E->JKJ");
             gramatica.Add("E->J");
             gramatica.Add("K-><");
             gramatica.Add("K->>");
             gramatica.Add("K->=");
             gramatica.Add("J->JLM");
             gramatica.Add("J->M");
             gramatica.Add("L->+");
             gramatica.Add("L->-");
             gramatica.Add("M->MNF");
             gramatica.Add("M->F");
             gramatica.Add("N->*");
             gramatica.Add("N->/");
             gramatica.Add("F->(E)");
             gramatica.Add("F->n");
             gramatica.Add("F->c");
            gramaticales.Add("c");
            gramaticales.Add("e");
            gramaticales.Add("d");
            gramaticales.Add("i");
            gramaticales.Add("n");
            gramaticales.Add("r");
             gramaticales.Add("s");
             gramaticales.Add("t");
             gramaticales.Add("u");
             gramaticales.Add("w");
             gramaticales.Add(",");
            gramaticales.Add(";");
            gramaticales.Add("A");
            gramaticales.Add("B");
            gramaticales.Add("C");
            gramaticales.Add("D"); 
            gramaticales.Add("E");
            gramaticales.Add("F");
            gramaticales.Add("H");
            gramaticales.Add("J");
            gramaticales.Add("K");
            gramaticales.Add("L");
            gramaticales.Add("M");
            gramaticales.Add("N");
            gramaticales.Add("P");
            gramaticales.Add("S");
            gramaticales.Add("T");
            gramaticales.Add("<");
             gramaticales.Add(">");
             gramaticales.Add("=");
             gramaticales.Add("+");
             gramaticales.Add("-");
             gramaticales.Add("*");
             gramaticales.Add("/");
             gramaticales.Add("(");
             gramaticales.Add(")");

           /* gramatica.Add("X->.S");
            gramatica.Add("S->ABc");
            gramatica.Add("A->aA");
            gramatica.Add("A->");
            gramatica.Add("B->bB");
            gramatica.Add("B->");
            gramaticales.Add("a");
            gramaticales.Add("b");
            gramaticales.Add("c");
            gramaticales.Add("S");
            gramaticales.Add("A");
            gramaticales.Add("B");*/

        }

        public void Genera_Edos()
        {
            int cont = 0;
           List< string> aux = new List<string>();
            List<string> auxi = new List<string>();
            res.Add(Cerradura1(gramatica[0]));

           for(int i=0;i<res.Count;i++)
            {
                for(int j=0;j<gramaticales.Count;j++)
                {
                    aux = Ir_A(res[i],gramaticales[j]);
                    if (aux.Count>0)
                    {
                        for (int x = 0; x < aux.Count; x++)
                        {
                            auxi = Cerradura(aux);
                            for (int z = 0; z < res.Count; z++)
                                if (ChecaLista(res[z], auxi))
                                {
                                    break;
                                }
                                else
                                {
                                    if (z == res.Count - 1)
                                    {
                                        res.Add(auxi);
                                        trans.Add(i.ToString() + gramaticales[j] + (res.Count-1).ToString());

                                    }
                                }
                        }
                    }
                }
            }

        }

        public bool ChecaLista(List<string> a, List<string> b)
        {
            bool res = false;
            if(a.Count==b.Count)
            {
                for(int i=0;i<a.Count;i++)
                {
                    if(a[i]==b[i])
                    {
                        res = true;
                    }else
                    {
                        return false;
                    }
                }
            }
            return res;
        }
        public List<string> Cerradura1(string prod)
        {
            string aux;
            List<string> produ = new List<string>();
            produ.Add(prod);
            for (int i = 0; i < produ.Count; i++)
            {
                for (int k = 0; k < produ[i].Length; k++)
                {
                    if (produ[i][k] == '.' && k < produ[i].Length)
                        for (int j = 0; j < gramatica.Count; j++)
                        {
                            if (k + 1 < produ[i].Length)
                                if (produ[i][k + 1] == gramatica[j][0])
                                {
                                    aux = gramatica[j].Insert(3, ".");
                                    if (!produ.Contains(aux))
                                    {
                                        produ.Add(aux);
                                    }
                                }
                        }
                }
            }
            return produ;
        }

        public List<string> Cerradura(List <string> prod)
        {
            string aux;
            List<string> produ = new List<string>(prod);
            for(int i=0;i<produ.Count;i++)
            {
                for (int k = 0; k < produ[i].Length; k++)
                {
                        if (produ[i][k]=='.'&&k<produ[i].Length)
                        for (int j=0; j < gramatica.Count; j++)
                        {
                            if(k+1<produ[i].Length)
                             if (produ[i][k+1] == gramatica[j][0])
                             {
                                aux=gramatica[j].Insert(3, ".");
                                if(!produ.Contains(aux))
                                {
                                    produ.Add(aux);
                                }
                             }
                        }
                }
            }
            return produ;
        }

        public List<string> Ir_A(List<string> I,string g)
        {
            string res = "",res2="";
            int auxj = 0;
            List<string> aux = new List<string>();
            for (int i = 0; i < I.Count; i++)
            {
                for (int j =0;j<I[i].Length;j++)
                {
                    if (I[i][j] == '.' && j < I[i].Length)
                    {
                        auxj = j;
                    }
                        if(I[i][j].ToString()==g&&j-1==auxj&&j>1)
                        {
                            res2 = I[i].Remove(j-1, 1);
                            res = res2.Insert(j, ".");
                            if(!aux.Contains(res))
                            {
                                    aux.Add(res);
                            }
                        }
                   

                }
            }
            return aux;
        }

        public List<List<string>> Estados
        {
            get { return res; }
        }

        public List<string> Transiciones
        {
            get { return trans; }
        }
        public List<string> Grama
        {
            get { return gramaticales; }
        }
    }
}

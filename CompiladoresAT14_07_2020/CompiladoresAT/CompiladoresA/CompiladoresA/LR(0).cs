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
        List<string> terminales = new List<string>();
        List<string> no_terminales = new List<string>();
        List<List<string>> res = new List<List<string>>();
        List<string> trans = new List<string>();
        List<string> primTerminales = new List<string>();
        List<Tuple<string,string>> primNoTerminales = new List<Tuple<string, string>>();
        List<string> Siguientes = new List<string>();
        List<string> tabla = new List<string>();
        string[] cad = new string[100];

        public LR_0_()
        {
            gramatica = new List<string>();
            /* gramatica.Add("X->.P");
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
             gramatica.Add("F->c");*/
            gramatica.Add("x->.programa");
            gramatica.Add("programa->secuencia-sent");
            gramatica.Add("secuencia-sent->secuencia-sent ; sentencia");
            gramatica.Add("secuencia-sent->sentencia");
            gramatica.Add("sentencia->sent-if");
            gramatica.Add("sentencia->sent-repeat");
            gramatica.Add("sentencia->sent-assign");
            gramatica.Add("sentencia->sent-read");
            gramatica.Add("sentencia->sent-write");
            gramatica.Add("sent-if->if exp then secuencia-sent end");
            gramatica.Add("sent-if->if exp then secuencia-sent else secuencia-sent end");
            gramatica.Add("sent-repeat->repeat secuencia-sent until exp");
            gramatica.Add("sent-assign->identificador := exp");
            gramatica.Add("sent-read->read identificador");
            gramatica.Add("sent-write->write exp");
            gramatica.Add("exp->exp-simple op-comp exp-simple");
            gramatica.Add("exp->exp-simple");
            gramatica.Add("op-comp-><");
            gramatica.Add("op-comp->>");
            gramatica.Add("op-comp->=");
            gramatica.Add("exp-simple->exp-simple opsuma term");
            gramatica.Add("exp-simple->term");
            gramatica.Add("opsuma->+");
            gramatica.Add("opsuma->-");
            gramatica.Add("term->term opmult factor");
            gramatica.Add("term->factor");
            gramatica.Add("opmult->*");
            gramatica.Add("opmult->/");
            gramatica.Add("factor->( exp )");
            gramatica.Add("factor->numero");
            gramatica.Add("factor->identificador"); 
            terminales.Add("-");
            terminales.Add("(");
            terminales.Add(")");
            terminales.Add("*");
            terminales.Add(":=");
            terminales.Add("/");
            terminales.Add(";");
            terminales.Add("+");
            terminales.Add("<");
            terminales.Add("=");
            terminales.Add(">");
            terminales.Add("identificador");
            terminales.Add("read");
            terminales.Add("end");
            terminales.Add("if");
            terminales.Add("numero");
            terminales.Add("repeat");
            terminales.Add("else");
            terminales.Add("then");
            terminales.Add("until");
            terminales.Add("write");
            no_terminales.Add("sent-if");
            no_terminales.Add("sent-repeat");
            no_terminales.Add("sent-assign");
            no_terminales.Add("sent-read");
            no_terminales.Add("exp");
            no_terminales.Add("factor");
            no_terminales.Add("sent-write");
            no_terminales.Add("exp-simple");
            no_terminales.Add("op-comp");
            no_terminales.Add("opsuma");
            no_terminales.Add("term");
            no_terminales.Add("opmult");
            no_terminales.Add("programa");
            no_terminales.Add("secuencia-sent");
            no_terminales.Add("sentencia");

            foreach (string a in terminales)
            {
                gramaticales.Add(a);
                primTerminales.Add(a);
            }
            foreach (string a in no_terminales)
                gramaticales.Add(a);
            /* gramaticales.Add("c");
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
             gramaticales.Add("<");
             gramaticales.Add(">");
             gramaticales.Add("=");
             gramaticales.Add("+");
             gramaticales.Add("-");
             gramaticales.Add("*");
             gramaticales.Add("/");
             gramaticales.Add("(");
             gramaticales.Add(")");
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
             gramaticales.Add("T");*/
            
            primNoTerminales.Add(new Tuple<string, string>("programa", "if,repeat,identificador,read,write"));
            primNoTerminales.Add(new Tuple<string, string>("secuencia-sent", "if,repeat,identificador,read,write"));
            primNoTerminales.Add(new Tuple<string, string>("sentencia", "if,repeat,identificador,read,write"));
            primNoTerminales.Add(new Tuple<string, string>("sent-if", "if"));
            primNoTerminales.Add(new Tuple<string, string>("sent-repeat", "repeat"));
            primNoTerminales.Add(new Tuple<string, string>("sent-assign", "identificador"));
            primNoTerminales.Add(new Tuple<string, string>("sent-read", "read"));
            primNoTerminales.Add(new Tuple<string, string>("sent-write", "write"));
            primNoTerminales.Add(new Tuple<string, string>("exp", "(,numero,identificador"));
            primNoTerminales.Add(new Tuple<string, string>("op-comp", "<,>,="));
            primNoTerminales.Add(new Tuple<string, string>("ex-simple", "(,numero,identificador"));
            primNoTerminales.Add(new Tuple<string, string>("opsuma", "+,-"));
            primNoTerminales.Add(new Tuple<string, string>("term", "(,numero,identificador"));
            primNoTerminales.Add(new Tuple<string, string>("opmult", "*,/"));
            primNoTerminales.Add(new Tuple<string, string>("factor", "(,numero,identificador"));
            Siguientes.Add("$");//p
            Siguientes.Add("$,;,end,else,until");//ss
            Siguientes.Add("$,;,end,else,until");//ss
            Siguientes.Add("$,;,end,else,until");//s
            Siguientes.Add("$,;,end,else,until");//s
            Siguientes.Add("$,;,end,else,until");//s
            Siguientes.Add("$,;,end,else,until");//s
            Siguientes.Add("$,;,end,else,until");//s
            Siguientes.Add("$,;,end,else,until");//si
            Siguientes.Add("$,;,end,else,until");//si
            Siguientes.Add("$,;,end,else,until");//sr
            Siguientes.Add("$,;,end,else,until");//sa
            Siguientes.Add("$,;,end,else,until");//sread
            Siguientes.Add("$,;,end,else,until");//sw
            Siguientes.Add("$,;,end,else,until,then,)");//exp
            Siguientes.Add("$,;,end,else,until,then,)");//exp
            Siguientes.Add("(,numero,identificador");//op-comp//
            Siguientes.Add("(,numero,identificador");//op-comp
            Siguientes.Add("(,numero,identificador");//op-comp
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,)");//ex-simple
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,)");//ex-simple
            Siguientes.Add("(,numero,identificador");//opsuma
            Siguientes.Add("(,numero,identificador");//opsuma
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,),*,/");//term
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,),*,/");//term
            Siguientes.Add("(,numero,identificador");//opmult
            Siguientes.Add("(,numero,identificador");//opmult
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,),*,/");//factor
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,),*,/");//factor
            Siguientes.Add("<,>,=,+,-,$,;,end,else,until,then,),*,/");//factor
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
                for (int j=0;j<gramaticales.Count;j++)
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
                                    if(!trans.Contains(i.ToString() + gramaticales[j] + (z).ToString()))
                                    trans.Add(i.ToString() + gramaticales[j] + (z).ToString());
                                    break;
                                }
                                else
                                {
                                    if(z==res.Count-1)
                                    {
                                        res.Add(auxi);
                                        trans.Add(i.ToString() + gramaticales[j] + (z).ToString());
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
        /*
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
        */

        public List<string> Cerradura1(string prod)
        {
            string aux,aux2="";
            bool band = false;
            List<string> produ = new List<string>();
            produ.Add(prod);
            for (int i = 0; i < produ.Count; i++)
            {
                aux2 = "";
                band = false;
                for (int k = 0; k < produ[i].Length; k++)
                {
                    if (k > produ[i].IndexOf('.')&&!band)
                        aux2 += produ[i][k].ToString();
                    if (produ[i][k] == ' ')
                        band = true;
                }
                for (int j = 0; j < gramatica.Count; j++)
                {
                        if (gramatica[j].StartsWith(aux2))
                        {
                            aux = gramatica[j].Insert(gramatica[j].IndexOf('>')+1, ".");
                            if (!produ.Contains(aux))
                            {
                                produ.Add(aux);
                            }
                        }
                }
            }
            return produ;
        }

        /*   public List<string> Cerradura(List <string> prod)
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
           */
        public List<string> Cerradura(List<string> prod)
        {
            string aux="",aux2="";
            bool band = false;
            List<string> produ = new List<string>(prod);
            
            for (int i = 0; i < produ.Count; i++)
            {
                if (produ[i].IndexOf('.') != produ[i].Length - 1)
                {
                    aux2 = "";
                    band = false;
                    for (int k = 0; k < produ[i].Length; k++)
                    {
                        if ((produ[i][k] == ' ' || k == produ[i].Length) && k > produ[i].IndexOf('.'))
                        {
                            band = true;
                        }
                        if (k > produ[i].IndexOf('.') && k < produ[i].Length && !band)
                        {
                            aux2 += produ[i][k].ToString();
                        }
                    }
                    for (int j = 0; j < gramatica.Count; j++)
                    {
                        if (gramatica[j].StartsWith(aux2))
                        {
                            if (gramatica[j][3] != '.')
                                aux = gramatica[j].Insert(gramatica[j].IndexOf('>') + 1, ".");
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
        /* public List<string> Ir_A(List<string> I,string g)
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
         }*/

        public List<string> Ir_A(List<string> I, string g)
        {
            string res = "", res2 = "",auxi="";
            bool band = false;
            int auxj = 0;
            List<string> aux = new List<string>();
            for (int i = 0; i < I.Count; i++)
            {
                auxi = "";
                band = false;
                for (int j = 0; j < I[i].Length; j++)
                {
                    if (j>I[i].IndexOf('.') && j < I[i].Length&&!band&&I[i][j]!=' ')
                    {
                        auxi+=I[i][j].ToString();
                    }
                    if ((I[i][j] == ' '||j==I[i].Length-1) && j > I[i].IndexOf('.'))
                    {
                        band = true;
                        auxj = I[i].IndexOf('.')+auxi.Length;
                    }
                }

                if (auxi == g )
                {
                    res2 = I[i].Remove(I[i].IndexOf('.'), 1);
                    if(auxj<I[i].Length-1)
                    if (I[i][auxj + 1] == ' ')
                        auxj += 1;
                    res = res2.Insert(auxj, ".");
                    if (!aux.Contains(res))
                    {
                        aux.Add(res);
                    }
                }
            }
            return aux;
        }

        public void TablaanalisisSintactico()
        {
            string aux1="",aux2="",aux3="",edo="";
            for (int i =0;i<res.Count;i++)
            {
                for(int j=0;j<res[i].Count;j++)
                {
                    aux1 = "";aux2 = ""; aux3 = ""; edo = "";
                    for (int k=0;k<res[i][j].Length;k++)
                    {
                        if(res[i][j]=="x->programa.")
                        {
                            if(!tabla.Contains(i.ToString() + " $ ac"))
                            tabla.Add(i.ToString() + " $ ac");
                        }else
                        if(res[i][j].IndexOf('.')==res[i][j].Length-1)
                        { 
                            for(int z=0;z<res[i][j].Length;z++)
                            {
                                if (z < res[i][j].IndexOf('>'))
                                {
                                    aux1 += res[i][j][z];
                                }

                            }
                            aux1 = res[i][j].Remove((res[i][j].Length - 1), 1);
                            for (int z=1;z<=gramatica.Count-1;z++)
                            {
                                if(gramatica[z]==aux1)
                                {
                                    string[] sigs = Siguientes[z-1].Split(',');
                                    for (int x=0;x<sigs.Length;x++)
                                    {
                                        tabla.Add(i.ToString() + " " + sigs[x] + " " + "r" + z.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                        if(k<res[i][j].IndexOf('-'))
                        {
                            aux1 += res[i][j][k];
                        }
                        else
                        {
                            if(k <= res[i][j].IndexOf('.'))
                            {
                                aux2+= res[i][j][k];
                            }
                            else
                            {
                                if(res[i][j][k-1]=='.'&& res[i][j][k]==' ')
                                {
                                    k++;
                                }
                                if (res[i][j][k] != ' ')
                                {
                                    aux3 += res[i][j][k];
                                    if (terminales.Contains(aux3))
                                    {
                                        for (int z = 0; z < trans.Count; z++)
                                        {
                                            if (trans[z].StartsWith(i.ToString()))
                                            {
                                                edo = trans[z].Remove(0, i.ToString().Length);
                                                if(edo.StartsWith(aux3))
                                                edo = edo.Remove(0, aux3.Length);
                                                if(edo.Length==1||edo.Length==2&&!tabla.Contains(i.ToString() + " " + aux3 + " d" + edo))
                                                tabla.Add(i.ToString() + " " + aux3 + " d" + edo);
                                            }
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
            }
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

        public List<string> Tabla
        {
            get { return tabla; }
        }

        public List<string> Grmatica
        {
            get { return gramatica; }
        }

        public List<string> Terminales
        {
            get { return terminales; }
        }

        public List<string> NoTerminales
        {
            get { return no_terminales; }
        }
    }
}

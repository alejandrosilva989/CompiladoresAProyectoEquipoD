using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class Automata
    {
        Dictionary<Char, int> operandos = new Dictionary<char, int>();
        public int Cont = 0;
        List<Char> alfabeto = new List<Char>();


        public List<Estados> a = new List<Estados>();
        public List<Estados> b = new List<Estados>();
        public List<Estados> c = new List<Estados>();

        public Automata()
        { }
        
        string Cadena;
        public string generaAFN(string Pos)
        {
            Cont = 0;  
            this.a.Clear(); 
            this.b.Clear(); 
            this.c.Clear(); 
            for (int i = 0; i < Pos.Length; i++)
            {
                verifica(Pos[i]);
            }
            foreach (var it in this.a)
            {
                Cadena += it.id + " --> ";
                foreach (var i in it.transiciones)
                {
                    Cadena += " ( " + i.idDestino + " / " + i.etiqueta + " )";
                }
                Cadena += Environment.NewLine;
            }
            return this.Cadena;
        }

        public List<Estados> GeneraTablaAFN(string posfija)
        {
            Cont = 0; 
            this.a.Clear(); 
            this.b.Clear(); 
            this.c.Clear(); 
            List<Estados> tablaTransiciones = new List<Estados>();
            for (int i = 0; i < posfija.Length; i++)
            {
                this.verifica(posfija[i]);
            }
            foreach (var it in this.a)
            {
                Estados edo = new Estados();
                edo.id = it.id;
                foreach (var i in it.transiciones)
                {
                    edo.transiciones.Add(new Trancision(i.idDestino, i.etiqueta));
                }
                tablaTransiciones.Add(edo);
            }
            return tablaTransiciones;
        }

        int indEstadoIncial;
        int indEstadoAcep;
        Estados Estado;
        Estados Estado2;
        public void generaAutomata(char Pos)
        {

            this.Estado = new Estados(Cont, 0);
            Cont++;
            this.Estado2 = new Estados(Cont, 1);
            Cont++;
            this.Estado.transiciones.Add(new Trancision(this.Estado2.id, Pos));

            if (this.a.Count == 0)
            {
                this.a.Add(this.Estado);
                this.a.Add(this.Estado2);
                return;
            }
            if (this.b.Count == 0)
            {
                this.b.Add(this.Estado);
                this.b.Add(this.Estado2);
                return;
            }
            if (this.c.Count == 0)
            {
                this.c.Add(this.Estado);
                this.c.Add(this.Estado2);
                return;
            }
        }


        public void verifica(char caracter)
        {
            switch (caracter)
            {
                case '|':
                    if (this.c.Count > 0) this.Genera_Union(ref this.c, ref this.b);
                    else this.Genera_Union(ref this.b, ref this.a); break;
                case '*':
                    if (this.c.Count > 0) { this.Genera_CerraduradeKleene(ref this.c); break; }
                    if (this.b.Count > 0) { this.Genera_CerraduradeKleene(ref this.b); break; }
                    if (this.a.Count > 0) { this.Genera_CerraduradeKleene(ref this.a); break; }
                    break;

                case '&':
                    if (this.c.Count > 0) this.Genera_Concatenacion(ref this.c, ref this.b);
                    else this.Genera_Concatenacion(ref this.b, ref this.a);
                    break;
                case '?':
                    if (this.c.Count > 0) { this.GeneraAFN_0_o_1_instancia(ref this.c); break; }
                    if (this.b.Count > 0) { this.GeneraAFN_0_o_1_instancia(ref this.b); break; }
                    if (this.a.Count > 0) { this.GeneraAFN_0_o_1_instancia(ref this.a); break; }
                    break;
                case '+':
                    if (this.c.Count > 0) { this.GeneraAFN_positiva(ref this.c); break; }
                    if (this.b.Count > 0) { this.GeneraAFN_positiva(ref this.b); break; }
                    if (this.a.Count > 0) { this.GeneraAFN_positiva(ref this.a); break; }
                    break;
                default: this.generaAutomata(caracter); break;
            }
        }

        public void GeneraAFN_positiva(ref List<Estados> Exp_regular)
        {
            this.Genera_NuevosEstados(Exp_regular);

            this.Estado.transiciones.Add(new Trancision(Exp_regular[this.indEstadoIncial].id, 'Ɛ')); 
            Exp_regular[this.indEstadoIncial].tipoEstado = -1;
            Exp_regular[this.indEstadoAcep].transiciones.Add(new Trancision(this.Estado2.id, 'Ɛ')); 
            Exp_regular[this.indEstadoAcep].tipoEstado = -1;

            Exp_regular[this.indEstadoAcep].transiciones.Add(new Trancision(Exp_regular[this.indEstadoIncial].id, 'Ɛ')); 

            Exp_regular.Insert(0, this.Estado);  
            Exp_regular.Add(this.Estado2);
        }


        public void Genera_Union(ref List<Estados> ExpresionRegular2, ref List<Estados> ExpresionRegular1)
        {
            int indEstadoAcep = ExpresionRegular1.FindIndex(x => x.tipoEstado == 1);
            int indEstadoInicial = ExpresionRegular1.FindIndex(x => x.tipoEstado == 0);

            int indEstadoInicial2 = ExpresionRegular2.FindIndex(x => x.tipoEstado == 0);
            int indEstadoAcep2 = ExpresionRegular2.FindIndex(x => x.tipoEstado == 1);

            this.Genera_NuevosEstados(ExpresionRegular1);

            Estado.transiciones.Add(new Trancision(ExpresionRegular1[indEstadoInicial].id, 'Ɛ'));
            Estado.transiciones.Add(new Trancision(ExpresionRegular2[indEstadoInicial2].id, 'Ɛ'));

            ExpresionRegular1[indEstadoAcep].transiciones.Add(new Trancision(Estado2.id, 'Ɛ'));
            ExpresionRegular2[indEstadoAcep2].transiciones.Add(new Trancision(Estado2.id, 'Ɛ'));

            ExpresionRegular1[indEstadoInicial].tipoEstado = -1;
            ExpresionRegular1[indEstadoAcep].tipoEstado = -1;
            ExpresionRegular2[indEstadoInicial2].tipoEstado = -1;
            ExpresionRegular2[indEstadoAcep2].tipoEstado = -1;
            foreach (var it in ExpresionRegular2)
            {
                ExpresionRegular1.Add(it);
            }
            ExpresionRegular2.Clear();

            ExpresionRegular1.Insert(0, this.Estado);
            ExpresionRegular1.Add(this.Estado2);
        }
        

    
        public void Genera_CerraduradeKleene(ref List<Estados> Exp_regular)
        {
            this.GeneraAFN_positiva(ref Exp_regular);
            Exp_regular[0].transiciones.Add(new Trancision(Exp_regular[Exp_regular.Count - 1].id, 'Ɛ'));
        }
       

       
        public void GeneraAFN_0_o_1_instancia(ref List<Estados> Exp_regular)
        {
            this.Genera_NuevosEstados(Exp_regular);

            this.Estado.transiciones.Add(new Trancision(Exp_regular[this.indEstadoIncial].id, 'Ɛ')); 
            Exp_regular[this.indEstadoIncial].tipoEstado = -1;
            Exp_regular[this.indEstadoAcep].transiciones.Add(new Trancision(this.Estado2.id, 'Ɛ')); 
            Exp_regular[this.indEstadoAcep].tipoEstado = -1;
            this.Estado.transiciones.Add(new Trancision(Estado2.id, 'Ɛ'));
            Exp_regular.Insert(0, this.Estado);
            Exp_regular.Add(this.Estado2);
        }




        public void Genera_Concatenacion(ref List<Estados> ExpresionRegular2, ref List<Estados> ExpresionRegular1)
        {
            int indEstadoAcep = ExpresionRegular1.FindIndex(x => x.tipoEstado == 1);
            int indEstadoIncial2 = ExpresionRegular2.FindIndex(x => x.tipoEstado == 0);
            int indEstadoAcep2 = ExpresionRegular2.FindIndex(x => x.tipoEstado == 1);
            foreach (var it in ExpresionRegular2)
            {
                foreach (var it2 in it.transiciones)
                {
                    if (it2.idDestino == ExpresionRegular2[indEstadoAcep2].id)
                    {
                        it2.idDestino--;
                    }
                }
            } 
            foreach (var it in ExpresionRegular2[indEstadoIncial2].transiciones)
                ExpresionRegular1[indEstadoAcep].transiciones.Add(it);

            ExpresionRegular1[indEstadoAcep].tipoEstado = -1;

            ExpresionRegular2[indEstadoAcep2].id = ExpresionRegular2[indEstadoIncial2].id;
            Cont--;
            ExpresionRegular2.RemoveAt(indEstadoIncial2);
            foreach (var it in ExpresionRegular2)
            {
                ExpresionRegular1.Add(it);
            }
            ExpresionRegular2.Clear();
        }


        public int Cont1 = 0;
        private void Genera_NuevosEstados(List<Estados> Exp_regular)
        {
            this.indEstadoIncial = Exp_regular.FindIndex(x => x.tipoEstado == 0);
            this.indEstadoAcep = Exp_regular.FindIndex(x => x.tipoEstado == 1);

            this.Estado = new Estados(Cont, 0);
            Cont++;
            this.Estado2 = new Estados(Cont, 1);
            Cont++;
        }



    } 
}

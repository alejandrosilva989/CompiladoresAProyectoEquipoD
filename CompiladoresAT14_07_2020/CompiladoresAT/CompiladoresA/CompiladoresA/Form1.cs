using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CompiladoresA
{
    public partial class Form1 : Form
    {
        List<Char> alfabeto = new List<Char>();
        Dictionary<Char, int> operandos = new Dictionary<char, int>();
        List<string[]> op = new List<string[]>();
        private Automata automata;
        List<string> encabezados = new List<string>();
        List<Label> labels = new List<Label>();
        bool lenguaje;
        Regex id;
        Regex num;
        List<char> Edos = new List<char>();
        List<string> ErroresL = new List<string>();
        List<List<char>> EdosAcep = new List<List<char>>();
        AFD NUM;
        public Form1()
        {
            InitializeComponent();
            this.automata = new Automata();
            for (int i = 48; i < 123; i++)
            {
                if (i == 58)
                {
                    i = 96;
                }
                alfabeto.Add((Char)i);
            }
            alfabeto.Add((Char)46);
            operandos.Add('+', 3);
            operandos.Add('*', 3);
            operandos.Add('?', 3);
            operandos.Add('&', 2);
            operandos.Add('|', 1);
            operandos.Add('(', 0);
            operandos.Add(')', 0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eR = textBox1.Text;
            string post;
            Postfija pos = new Postfija(operandos, alfabeto);
            post = pos.Postfija2(eR);
            textBox2.Text = post;
        }




        private void button2_Click(object sender, EventArgs e)
        {


            this.txtAutomataTransicion.Clear();
            this.txtAutomataTransicion.Refresh();
            this.txtAutomataTransicion.Text = this.automata.generaAFN(this.textBox2.Text);
            int A = this.automata.a.Count;

            this.LlenaTabla();

        }

        private void LlenaTabla()
        {

            this.dgvTransiciones_AFN.Columns.Clear();
            this.dgvTransiciones_AFN.Rows.Clear();
            this.dgvTransiciones_AFN.Refresh();
            this.dgvTransiciones_AFN.ColumnHeadersVisible = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.White;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            this.dgvTransiciones_AFN.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            List<string> nombreColumnas = new List<string>();
            List<Estados> edosTransiciones = this.automata.GeneraTablaAFN(this.textBox2.Text);//new List<CEstado>();

            nombreColumnas.Add("Edos");
            for (int i = 0; i < edosTransiciones.Count; i++)
            {
                for (int j = (edosTransiciones[i].transiciones.Count - 1); j >= 0; j--)
                {
                    String nombreColumna = edosTransiciones[i].transiciones[j].etiqueta.ToString();
                    if (!nombreColumna.Contains("Ɛ") && !nombreColumnas.Contains(nombreColumna))
                        nombreColumnas.Add(nombreColumna);
                }
            }
            nombreColumnas.Add("Ɛ");
            this.dgvTransiciones_AFN.ColumnCount = nombreColumnas.Count;
            for (int i = 0; i < nombreColumnas.Count; i++)
            {
                this.dgvTransiciones_AFN.Columns[i].Name = nombreColumnas[i];
            }
            List<string[]> tablaTransiciones = new List<string[]>();
            for (int id = 0; id < edosTransiciones.Count; id++)
            {
                string[] renglonTransitivo = this.dimeEstadosTransitivos(edosTransiciones, id);
                tablaTransiciones.Add(renglonTransitivo);
            }
            op = tablaTransiciones;
            foreach (string[] renglonActual in tablaTransiciones)
            {
                this.dgvTransiciones_AFN.Rows.Add(renglonActual);
            }
            encabezados = nombreColumnas;
        }
        private string[] dimeEstadosTransitivos(List<Estados> edosTransiciones, int posicion)
        {
            List<string> lista_elementos = new List<string>();

            lista_elementos.Add(edosTransiciones[posicion].id.ToString());
            for (int i = 0; i < this.dgvTransiciones_AFN.ColumnCount - 1; i++)
            {
                for (int j = 1; j < this.dgvTransiciones_AFN.ColumnCount; j++)
                {
                    lista_elementos.Add(
                        this.dimeTransiciones(
                            edosTransiciones[posicion].transiciones,
                            this.dgvTransiciones_AFN.Columns[j].Name.ToString()));
                }
            }
            string[] elementos = lista_elementos.ToArray();
            return elementos;
        }

        private string dimeTransiciones(List<Trancision> transiciones, string nombreColumna)
        {
            string estados_destino = "ɸ";
            string idEstados = "";
            Boolean band = false;
            for (int i = 0; i < transiciones.Count; i++)
            {
                estados_destino = "";
                if (transiciones[i].etiqueta.ToString().Equals(nombreColumna))
                {
                    band = true;
                    idEstados += transiciones[i].idDestino.ToString() + ",";
                }
                else { estados_destino = "ɸ"; }
            }
            if (band) estados_destino = "{" + idEstados.Substring(0, (idEstados.Length - 1)) + "}";
            return estados_destino;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AFD();
        }

        public void AFD()
        {
            List<char> ligas = new List<char>();
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (alfabeto.Contains(textBox2.Text[i]))
                    ligas.Add(textBox2.Text[i]);
            AFD afd = new AFD(op, ligas, encabezados);
            afd.Genera_Estados();
            Dictionary<string, char> estados = new Dictionary<string, char>();
            List<string> transiciones = new List<string>(afd.DestadosT);
            estados = afd.Estados;
            this.dataGridView4.Columns.Clear();
            this.dataGridView4.Rows.Clear();
            this.dataGridView4.Refresh();
            this.dataGridView4.ColumnHeadersVisible = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.White;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            this.dataGridView4.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView4.Columns.Add("Edos", "Edos");
            for (int i = 0; i < ligas.Count; i++)
            {

                this.dataGridView4.Columns.Add(ligas[i].ToString(), ligas[i].ToString());
            }

            for (int i = 0; i < afd.DestadosF.Count; i++)
            {
                this.dataGridView4.Rows.Add(afd.DestadosF[i].ToString());
            }

            int index = 0;
            for (int i = 0; i < dataGridView4.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView4.ColumnCount; j++)
                {
                    for (int x = 0; x < transiciones.Count; x++)
                    {
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString() == transiciones[x][0].ToString())
                        {
                            index = ligas.IndexOf(transiciones[x][1]);
                            dataGridView4.Rows[i].Cells[index + 1].Value = transiciones[x][2].ToString();
                        }
                    }

                }
            }

            string[,] conjunto = new string[dataGridView4.Rows.Count, dataGridView4.Columns.Count];

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cel in row.Cells)
                    {
                        if (cel.Value != null)
                            conjunto[cel.RowIndex, cel.ColumnIndex] = cel.Value.ToString();
                        else
                            conjunto[cel.RowIndex, cel.ColumnIndex] = " ";
                    }
                }
            }
            lenguaje=afd.VerificaLenguaje(conjunto,textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.txtAutomataTransicion.Clear();
            this.txtAutomataTransicion.Refresh();
            this.txtAutomataTransicion.Text = this.automata.generaAFN(this.textBox2.Text);
            int A = this.automata.a.Count;

            this.LlenaTabla();

            AFD();
            
            dgvTransiciones_AFN.Rows.Clear();
            dgvTransiciones_AFN.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            if (lenguaje)
            {
                label11.Visible = true;
                label12.Visible = false;
            }
            else
            {
                label11.Visible = false;
                label12.Visible = true;
            }
        }
        
        public List<string> Lexico()
        {
            string[] lineas = txt_Lenguaje.Lines;
            string[,] id;
            string[,] num;
            List<char> edosid = new List<char>();
            List<char> edosnum = new List<char>();
            Postfija pos = new Postfija(operandos, alfabeto);
            textBox2.Text = pos.Postfija2(textBox4.Text);
            this.txtAutomataTransicion.Clear();
            this.txtAutomataTransicion.Refresh();
            this.txtAutomataTransicion.Text = this.automata.generaAFN(this.textBox2.Text);
            int A = this.automata.a.Count;
            this.LlenaTabla();
            id = AFD2();
            edosid = Edos;
            List<char> ligasid = new List<char>();
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (alfabeto.Contains(textBox2.Text[i]))
                    ligasid.Add(textBox2.Text[i]);
            dgvTransiciones_AFN.Rows.Clear();
            dgvTransiciones_AFN.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            textBox2.Text = pos.Postfija2(textBox5.Text);
            this.txtAutomataTransicion.Clear();
            this.txtAutomataTransicion.Refresh();
            this.txtAutomataTransicion.Text = this.automata.generaAFN(this.textBox2.Text);
            A = this.automata.a.Count;
            this.LlenaTabla();
            num = AFD2();
            edosnum = Edos;
            List<char> ligasnum = new List<char>();
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (alfabeto.Contains(textBox2.Text[i]))
                    ligasnum.Add(textBox2.Text[i]);
            dgvTransiciones_AFN.Rows.Clear();
            dgvTransiciones_AFN.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            textBox2.Text = "";
            lexicoB lexico = new lexicoB(lineas, id, num, ligasid, ligasnum, edosid, edosnum,EdosAcep[0],EdosAcep[1]);
            lexico.Clasifica();

            // oAnaLex.Inicia();
            //oAnaLex.Analiza(txt_Lenguaje.Text, textBox4.Text, textBox5.Text);
            dataGridView1.Rows.Clear();
            if (lexico.Lexema.Count > 0)
                dataGridView1.Rows.Add(lexico.Lexema.Count);
            for (int i = 0; i < lexico.Lexema.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = lexico.Lexema[i];
                dataGridView1.Rows[i].Cells[1].Value = lexico.Token[i];
            }
            return lexico.Error;
        }
        private void btn_Token_Click(object sender, EventArgs e)
        {
           Lexico();
            EdosAcep = new List<List<char>>();

        }

        public string[,] AFD2()
        {
            List<char> ligas = new List<char>();
            for (int i = 0; i < textBox2.Text.Length; i++)
                if (alfabeto.Contains(textBox2.Text[i]))
                    ligas.Add(textBox2.Text[i]);
            AFD afd = new AFD(op, ligas, encabezados);
            afd.Genera_Estados();
            Dictionary<string, char> estados = new Dictionary<string, char>();
            List<string> transiciones = new List<string>(afd.DestadosT);
            estados = afd.Estados;
            this.dataGridView4.Columns.Clear();
            this.dataGridView4.Rows.Clear();
            this.dataGridView4.Refresh();
            this.dataGridView4.ColumnHeadersVisible = true;
            // Establece el estilo del encabezado de columna.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.White;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            this.dataGridView4.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView4.Columns.Add("Edos", "Edos");
            for (int i = 0; i < ligas.Count; i++)
            {

                this.dataGridView4.Columns.Add(ligas[i].ToString(), ligas[i].ToString());
            }

            for (int i = 0; i < afd.DestadosF.Count; i++)
            {
                this.dataGridView4.Rows.Add(afd.DestadosF[i].ToString());
            }

            int index = 0;
            for (int i = 0; i < dataGridView4.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView4.ColumnCount; j++)
                {
                    for (int x = 0; x < transiciones.Count; x++)
                    {
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString() == transiciones[x][0].ToString())
                        {
                            index = ligas.IndexOf(transiciones[x][1]);
                            dataGridView4.Rows[i].Cells[index + 1].Value = transiciones[x][2].ToString();
                        }
                    }

                }
            }
             Edos= afd.DestadosF;
            string[,] conjunto = new string[dataGridView4.Rows.Count, dataGridView4.Columns.Count];

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cel in row.Cells)
                    {
                        if(cel.Value!=null)
                        conjunto[cel.RowIndex, cel.ColumnIndex] = cel.Value.ToString();
                        else
                        conjunto[cel.RowIndex, cel.ColumnIndex] = " ";

                    }
                }
            }
            EdosAcep.Add(afd.Aceptacion);
            return conjunto;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            LR_0_ au0 = new LR_0_();
            List<string> nume = new List<string>();
            nume.Add("1");
            nume.Add("2");
            nume.Add("3");
            nume.Add("4");
            nume.Add("5");
            nume.Add("6");
            nume.Add("7");
            nume.Add("8");
            nume.Add("9");
            nume.Add("0");
            au0.Genera_Edos();
            List<List<string>> edos = new List<List<string>>(au0.Estados);

           // for (int i = 0; i < edos.Count; i++)
             //   for(int j=0;j<edos[i].Count;j++)
                //MessageBox.Show(edos.Count.ToString());
             List<string> trans = new List<string >(au0.Transiciones);
             List<string> g = new List<string>(au0.Grama);
            g.Add("$");
            au0.TablaanalisisSintactico();
            List<string> tabla = new List<string>(au0.Tabla);
            string ini = "", gra = "", fin = "";
             for (int i = 0; i < g.Count; i++)
             {
                 if (i == 0)
                 {
                     dataGridView2.Columns.Add("Edos", "Edos");
                     dataGridView3.Columns.Add("Edos", "Edos");
                 }

                 dataGridView2.Columns.Add(g[i], g[i]);
             }
             for (int i=0;i<edos.Count;i++)
             {
                 dataGridView2.Rows.Add(i.ToString());
                 dataGridView3.Rows.Add(i.ToString());
             }
            string aux = "",auxin="";
             for (int i = 0; i < g.Count; i++)
             {
                 dataGridView3.Columns.Add(g[i].ToString(), g[i].ToString());
             }

            for (int i = 0; i < g.Count; i++)
            {
                for (int j = 0; j < edos.Count; j++)
                {
                    for (int x = 0; x < trans.Count; x++)
                    {
                        ini = ""; gra = ""; fin = "";
                        for (int z=0;z<trans[x].Length;z++)
                        {
                            if(nume.Contains(trans[x][z].ToString()))
                            {
                                if (z == 0 || z == 1)
                                    ini += trans[x][z];
                                else
                                if(z==trans[x].Length-1||z== trans[x].Length - 2)
                                    fin+= trans[x][z];
                            }
                            else
                            {
                                gra += trans[x][z];

                            }
                        }
                        if (dataGridView2.Rows[j].Cells[0].Value.ToString() == ini)
                        {
                            if (g[i] == gra)
                            {
                                dataGridView2.Rows[j].Cells[i + 1].Value = fin;
                            }
                        }
                        else
                        {
                            //dataGridView2.Rows[j].Cells[i + 1].Value = "ɸ";

                        }
                    }
                }
            }
            int cont = 0;
            for (int i = 0; i < g.Count; i++)
            {
                for (int j = 0; j < edos.Count; j++)
                {
                    for (int x = 0; x < tabla.Count; x++)
                    {
                        ini = ""; gra = ""; fin = "";
                        cont = 0;
                        for(int z=0;z<tabla[x].Length;z++)
                        {
                            if (tabla[x][z] != ' ' && cont == 0)
                            {
                                ini += tabla[x][z];
                            }
                            else
                            {
                                if (cont == 0 && tabla[x][z] == ' ')
                                    cont++;
                                else
                                {
                                    if (tabla[x][z] != ' ' && cont == 1)
                                    {
                                        gra += tabla[x][z];
                                    }
                                    else
                                    {
                                        if (cont == 1 && tabla[x][z] == ' ')
                                            cont++;
                                        else
                                        {
                                            if (cont == 2)
                                            {
                                                fin += tabla[x][z];
                                            }
                                        }
                                    }
                                }
                            }
                            
                           
                        }
                        if (dataGridView3.Rows[j].Cells[0].Value.ToString() == ini)
                        {
                            if (g[i] == gra)
                            {
                                dataGridView3.Rows[j].Cells[i + 1].Value = fin;
                            }
                        }
                    }
                }
            }
                        Form2 f2 = new Form2();
             f2.ShowDialog();

         
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            treeView1.Nodes.Clear();
            LR_0_ lr = new LR_0_();
            lr.Genera_Edos();
            List<string> termi = new List<string>(lr.Terminales);
            List<string> no_term = new List<string>(lr.NoTerminales);
            List<string> g = new List<string>(lr.Grama);
            g.Add("$");
            ErroresL=Lexico();
            string[] lineas = txt_Lenguaje.Lines;
            id = new Regex(textBox4.Text);
             num = new Regex(textBox5.Text);
            int h = 650;
            foreach (Label a in labels)
                this.Controls.Remove(a);
            lineas[lineas.Length - 1] += " $";
            List<string> errores = new List<string>();
           
            errores.Clear();
            
            Point p = new Point(80, h);
            string err = "";
            List<int> lineaError = new List<int>();
            for (int i = 0; i < lineas.Length; i++)
            {
                foreach (string a in ErroresL)
                {
                    if (lineas[i].Contains(a))
                    {
                        if (!errores.Contains("Error en la linea " + i.ToString() + " no se reconoce " + a))
                        {
                            errores.Add("Error en la linea " + (i+1).ToString() + " no se reconoce " + a);
                            lineaError.Add(i);
                        }

                    }
                }
            }

            List<string> gramat = new List<string>(lr.Grmatica);


            foreach (string a in errores)
            {
                Label label = new Label();
                label.Location = new Point(80, h);
                label.ForeColor = Color.Red;
                label.Width = 300;
                label.Height = 15;
                label.Text = a;
                this.Controls.Add(label);
                labels.Add(label);
                h += 15;
            }
            Error.Visible = false;
            string[] linea;
            List<List<string>> arbol = new List<List<string>>();
            List<string> arbolaux = new List<string>();
            int index = 0;
            string auxres = "";
            string elemento;
            bool band = false;
            int s = 0;
            string res = "",auxnodo="";
            int t = 0;
            List<string> w = new List<string>();
            List<string> nodos = new List<string>();
            Stack<int> pila = new Stack<int>();
            List<TreeNode> nodosA = new List<TreeNode>();
            List<TreeNode> auxA = new List<TreeNode>();
            TreeNode padre=new TreeNode();
            pila.Push(0);
            for(int i=0;i<lineas.Length; i++)
            {
                
                    
                    if (lineas[i].Contains(";"))
                    {
                        index = lineas[i].IndexOf(';');
                        if (lineas[i][index - 1] != ' ')
                        {
                            lineas[i] = lineas[i].Insert(index, " ");
                        }
                    }
                    if(lineas[i].Length > 1)
                    while(lineas[i][0]==' ')
                    {
                        if (lineas[i].Length == 1)
                            break;
                        lineas[i] = lineas[i].Remove(0,1);
                    }
                   linea = lineas[i].Split(' ');
                for(int j=0;j<linea.Length;j++)
                {
                    w.Add(linea[j]);
                }
               
            }
            if(ErroresL.Count==0)
            for(int j=0; j<w.Count;j++)
                    {
                        elemento = w[j];
                        if(!g.Contains(elemento))
                        {
                            if(id.IsMatch(elemento)&&!ErroresL.Contains(elemento))
                            {
                                elemento="identificador";
                            }
                            else
                            {
                                if(num.IsMatch(elemento)&&!ErroresL.Contains(elemento))
                                {
                                    elemento = "numero";
                        }
                        else
                        {
                            Error.Visible = true;
                            break;
                        }
                            }
                        }

                        if (dataGridView3.Rows[s].Cells[(g.IndexOf(elemento) + 1)].Value != null)
                            res = dataGridView3.Rows[s].Cells[(g.IndexOf(elemento) + 1)].Value.ToString();
                        else
                {
                    Error.Visible = true;
                    Invalidate();

                    break;
                }
                if (res.Contains("d"))
                                {
                                    string pilita = res.Remove(0, 1);
                                    pila.Push(Int32.Parse(pilita));
                                     s = Int32.Parse(pilita);
                        nodosA.Add(new TreeNode(elemento));
                    nodos.Add(elemento);
                                }else
                                {
                                    if(res.Contains("r"))
                                    {
                                  while (res.Contains("r"))
                                  {
                                    res = res.Remove(0, 1);
                                    string gra = gramat[Int32.Parse(res)];
                                    string aux = "";
                                    string auxini = "";
                                    for (int c = 0; c < gramat[Int32.Parse(res)].Length; c++)
                                    {
                                        if (c < gramat[Int32.Parse(res)].IndexOf('>'))
                                            auxini += gramat[Int32.Parse(res) ][c];
                                        if (c > gramat[Int32.Parse(res)].IndexOf('>') - 1)
                                        {
                                            aux += gramat[Int32.Parse(res)][c];
                                        }
                                    }
                                    auxini = auxini.Remove((auxini.Length - 1), 1);
                                 padre = new TreeNode(auxini, nodosA.ToArray());
                                if(auxini== "secuencia-sent")
                                {
                                    if(!auxA.Contains(padre))
                                    auxA.Add(padre);
                                }
                                nodosA.Clear();
                                nodosA.Add(padre);
                           
                            string[] nums = aux.Split(' ');
                                    if (pila.Count >= nums.Length)
                                        for (int c = 0; c < nums.Length; c++)
                                            pila.Pop();
                                    if (pila.Count > 0)
                                    {
                                         t = pila.Pop();
                                    pila.Push(t);
                                    }
                                    if (dataGridView2.Rows[t].Cells[(g.IndexOf(auxini)+1)].Value != null)
                                    {
                                        res = dataGridView2.Rows[t].Cells[(g.IndexOf(auxini) + 1)].Value.ToString();
                                        pila.Push(Int32.Parse(res));
                                    }
                                    else
                                        {
                         
                                Invalidate();
                                Error.Visible = true;
                                break;
                                         }


                            if (dataGridView3.Rows[Int32.Parse(res)].Cells[(g.IndexOf(elemento) + 1)].Value != null)
                            {
                                res = dataGridView3.Rows[Int32.Parse(res)].Cells[(g.IndexOf(elemento) + 1)].Value.ToString();


                            }
                            else
                            {
                                Error.Visible = true;
                                break;
                            }

                        }
                                  if(res.Length>1&&!res.Contains("ac"))
                                {
                                res = res.Remove(0, 1);
                                s = Int32.Parse(res);
                            pila.Push(s);
                            }
                            else
                            {
                                if (res.Contains("ac"))
                                {
                                    MessageBox.Show("Cadena aceptada");
                                    nodosA[0].Nodes.Clear();
                                    padre = nodosA[0];
                                    TreeNode hijoP = new TreeNode("secuencia-sent", auxA.ToArray());
                                    treeView1.Nodes.Add(padre);
                                    treeView1.Nodes[0].Nodes.Add(hijoP);
                                }
                            }
                                      
                           
                                    }
                                 }
                         
                  }

            treeView1.ExpandAll();
        }

       public void Genera_Arbol()
        { 
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

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

        public Form1()
        {
            InitializeComponent();
            this.automata = new Automata();
            for (int i=48;i<123;i++)
            {
                if(i==58)
                {
                    i = 96;
                }
                alfabeto.Add((Char)i);
            }
            alfabeto.Add((Char)46);
            operandos.Add('+',3);
            operandos.Add('*',3);
            operandos.Add('?',3);
            operandos.Add('&',2);
            operandos.Add('|',1);
            operandos.Add('(', 0);
            operandos.Add(')', 0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eR = textBox1.Text;
            string post;
            Postfija pos = new Postfija(operandos,alfabeto);
            post = pos.Postfija2(eR);
            textBox2.Text = post;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

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
            #region Formato DataGridView
            // Limpia el DataGridView
            this.dgvTransiciones_AFN.Columns.Clear();
            this.dgvTransiciones_AFN.Rows.Clear();
            this.dgvTransiciones_AFN.Refresh();
            this.dgvTransiciones_AFN.ColumnHeadersVisible = true;
            // Establece el estilo del encabezado de columna.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.White;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            this.dgvTransiciones_AFN.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            #endregion

            #region Llenado de Tabla en DataGridView
            // Agrega Nombres a las columnas
            List<string> nombreColumnas = new List<string>();
            List<Estados> edosTransiciones = this.automata.GeneraTablaAFN(this.textBox2.Text);//new List<CEstado>();
            #region Asignando nombres a DataGridView
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
            #endregion

            // Establece número de columnas.
            this.dgvTransiciones_AFN.ColumnCount = nombreColumnas.Count;
            //Inserta Nombre en La Tabla
            for (int i = 0; i < nombreColumnas.Count; i++)
            {
                this.dgvTransiciones_AFN.Columns[i].Name = nombreColumnas[i];
            }

            // Llena Tabla con los estados de transicion.
            List<string[]> tablaTransiciones = new List<string[]>();
            for (int id = 0; id < edosTransiciones.Count; id++)
            {
                string[] renglonTransitivo = this.dimeEstadosTransitivos(edosTransiciones, id);
                tablaTransiciones.Add(renglonTransitivo);
            }
            op = tablaTransiciones;
            // Agrega los renglones al DataGridView
            foreach (string[] renglonActual in tablaTransiciones)
            {
                this.dgvTransiciones_AFN.Rows.Add(renglonActual);
            }
            #endregion
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
                    idEstados += transiciones[i].idDestino.ToString() + ", ";
                }
                else { estados_destino = "ɸ"; }
            }
            if (band) estados_destino = "{" + idEstados.Substring(0, (idEstados.Length - 2)) + "}";
            return estados_destino;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<char>ligas=new List<char>();
            for (int i = 0; i < textBox2.Text.Length; i++)
                if(alfabeto.Contains(textBox2.Text[i]))
                ligas.Add(textBox2.Text[i]);
            AFD afd = new AFD(op,ligas,encabezados);
            afd.Genera_Estados();
            Dictionary<string, char> estados = new Dictionary<string, char>();
            estados = afd.Estados;
            foreach (KeyValuePair<string, char> kvp in estados)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                MessageBox.Show( string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }

            foreach (char a in afd.DestadosF)
                MessageBox.Show(a.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Regex coinc = new Regex(textBox1.Text);
            if(coinc.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Pertenece al lenguaje");
            }
        }
    }
}

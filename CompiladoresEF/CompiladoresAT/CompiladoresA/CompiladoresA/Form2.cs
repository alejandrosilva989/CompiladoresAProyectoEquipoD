using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiladoresA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LR_0_ au0 = new LR_0_();
            int h = 5;
            Point p = new Point(80, h);
            au0.Genera_Edos();
            List<List<string>> edos = new List<List<string>>(au0.Estados);
            List<string> Edos = new List<string>(au0.Transiciones);
            string edoAct = "";
            MessageBox.Show(edos.Count.ToString());
            for (int i = 0; i < edos.Count; i++)
            {
                for (int j = 0; j < edos[i].Count; j++)
                {
                    edoAct += edos[i][j] + " ";

                }
                Label label = new Label();
                label.Location = p;
                label.Width = 300;
                label.Height = 15;
                label.Text = edoAct;
                this.Controls.Add(label);
                edoAct = "";
                p.Y = h += 13;
            }
        }
    }
}

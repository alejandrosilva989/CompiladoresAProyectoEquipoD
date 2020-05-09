using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class Trancision
    {
        public int idDestino { set; get; }
        public char etiqueta { set; get; }
        public Boolean estado { set; get; }

        public Trancision() { }

        public Trancision(int Id_destino, char etiqueta)
        {
            this.idDestino = Id_destino;
            this.etiqueta = etiqueta;
        }

        public string estadoTransicion()
        {
            //string a = "ɸ Φ Ɛ";
            string etiquetaEstado = this.estado == true ?
                etiquetaEstado = this.idDestino.ToString() : etiquetaEstado = "0";
            return etiquetaEstado;
        }
    }
}

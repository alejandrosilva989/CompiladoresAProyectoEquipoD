using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class Estados
    {
        public List<Trancision> transiciones { set; get; }
        public int id { set; get; }
        public int tipoEstado { set; get; }     


        public Estados() { this.transiciones = new List<Trancision>(); }

        public Estados(int Id, int Tipo_Estado)
        {
            this.id = Id;
            this.tipoEstado = Tipo_Estado;
            this.transiciones = new List<Trancision>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doctor.Entities
{
    public class Medicamento
    {
        public int MedicamentoId { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Receta> Receta { get; set; }
    }
}

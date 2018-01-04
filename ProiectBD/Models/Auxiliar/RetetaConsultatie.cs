using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models.Auxiliar
{
    public class RetetaConsultatie
    {
        public int ID { get; set; }

        public string Numar { get; set; }

        public string Descriere { get; set; }

        public int ConsultatieID { get; set; }

        public string DescriereConsultatie { get; set; }

        public DateTime DataConsultatie { get; set; }
    }
}

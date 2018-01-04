using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models.Auxiliar
{
    public class InterventieConsultatie
    {
        public int ID { get; set; }

        public string Nume { get; set; }

        public string Descriere { get; set; }

        public DateTime Data { get; set; }

        public int Pret { get; set; }

        public int ConsultatieID { get; set; }

        public string DescriereConsultatie { get; set; }

        public DateTime DataConsultatie { get; set; }
    }
}

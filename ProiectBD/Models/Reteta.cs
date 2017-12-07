using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Reteta
    {
        public Reteta(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            Numar = reader.GetString(1);
            Descriere = reader.IsDBNull(2) ? null : reader.GetString(2);
            ConsultatieID = reader.GetInt32(3);
        }

        public Reteta()
        {
            Descriere = "";
            Numar = "";
        }
        public Reteta(Interventie interventie)
        {
            Numar = interventie.Nume;
            Descriere = interventie.Descriere;
            ConsultatieID = interventie.ConsultatieID;
        }

        public int ID { get; set; }

        public string Numar { get; set; }

        public string Descriere { get; set; }

        public int ConsultatieID { get; set; }
    }
}

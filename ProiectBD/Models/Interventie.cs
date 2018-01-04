using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Interventie
    {
        public Interventie(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            Nume = reader.GetString(1);
            Descriere = reader.IsDBNull(2) ? null : reader.GetString(2);
            Data = reader.GetDateTime(3);
            Pret = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
            ConsultatieID = reader.GetInt32(5);
        }

        public Interventie()
        {
            Descriere = "";
            Data = DateTime.Now;
            Nume = "";
        }
        public Interventie(Interventie interventie)
        {
            Nume = interventie.Nume;
            Descriere = interventie.Descriere;
            Data = interventie.Data;
            Pret = interventie.Pret;
            ConsultatieID = interventie.ConsultatieID;
        }

        public int ID { get; set; }

        public string Nume { get; set; }

        public string Descriere { get; set; }

        public DateTime Data { get; set; }

        public int Pret { get; set; }

        public int ConsultatieID { get; set; }
    }
}

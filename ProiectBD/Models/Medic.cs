using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Medic
    {
        public Medic(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            Nume = reader.IsDBNull(1) ? null : reader.GetString(1);
            Prenume = reader.IsDBNull(2) ? null : reader.GetString(2);
            Telefon = reader.IsDBNull(3) ? null : reader.GetString(3);
            Email = reader.IsDBNull(4) ? null : reader.GetString(4);
            Adresa = reader.IsDBNull(5) ? null : reader.GetString(5);
        }

        public Medic()
        {
            Nume = "";
            Prenume = "";
            Adresa = "";
            Telefon = "";
            Email = "";
        }
        public Medic(Medic medic)
        {
            Nume = medic.Nume;
            Prenume = medic.Prenume;
            Adresa = medic.Adresa;
            Telefon = medic.Telefon;
            Email = medic.Email;
        }

        public int ID { get; set; }

        public string Nume { get; set; }

        public string Prenume { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Adresa { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Proprietar
    {
        public Proprietar(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            Nume = reader.IsDBNull(1) ? null : reader.GetString(1);
            Prenume = reader.IsDBNull(2) ? null : reader.GetString(2);
            CNP = reader.IsDBNull(3) ? null : reader.GetString(3);
            SerieCI = reader.IsDBNull(4) ? null : reader.GetString(4);
            NumarCI = reader.IsDBNull(5) ? null : reader.GetString(5);
            Judet = reader.IsDBNull(6) ? null : reader.GetString(6);
            Localitate = reader.IsDBNull(7) ? null : reader.GetString(7);
            Adresa = reader.IsDBNull(8) ? null : reader.GetString(8);
            Telefon = reader.IsDBNull(9) ? null : reader.GetString(9);
            Email = reader.IsDBNull(10) ? null : reader.GetString(10);

        }

        public Proprietar()
        {
            Nume = "";
            Prenume = "";
            CNP = "";
            SerieCI = "";
            NumarCI = "";
            Judet = "";
            Localitate = "";
            Adresa = "";
            Telefon = "";
            Email = "";
        }
        public Proprietar(Proprietar prop)
        {
            Nume = prop.Nume;
            Prenume = prop.Prenume;
            CNP = prop.CNP;
            SerieCI = prop.SerieCI;
            NumarCI = prop.NumarCI;
            Judet = prop.Judet;
            Localitate = prop.Localitate;
            Adresa = prop.Adresa;
            Telefon = prop.Telefon;
            Email = prop.Email;
        }

        public int ID { get; set; }

        public string Nume { get; set; }

        public string Prenume { get; set; }

        public string CNP { get; set; }

        public string SerieCI { get; set; }

        public string NumarCI { get; set; }

        public string Judet { get; set; }

        public string Localitate { get; set; }

        public string Adresa { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }


    }
}

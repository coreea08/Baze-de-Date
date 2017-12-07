using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Animal
    {
        public Animal(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            CodMicrocip = reader.IsDBNull(1) ? null : reader.GetString(1);
            DataNasterii = reader.GetDateTime(2);
            SerieCarnet = reader.IsDBNull(3) ? null : reader.GetString(3);
            NumarCarnet = reader.IsDBNull(4) ? null : reader.GetString(4);
            CodPasaport = reader.IsDBNull(5) ? null : reader.GetString(5);
            Nume = reader.GetString(6);
            Culoare = reader.GetString(7);
            Rasa = reader.IsDBNull(8) ? null : reader.GetString(8);
            Categorie = reader.GetString(9);
            Sex = reader.GetString(10);
            ProprietarID = reader.GetInt32(11);
        }

        public Animal()
        {
            CodMicrocip = "";
            DataNasterii = DateTime.Now;
            SerieCarnet = "";
            NumarCarnet = "";
            CodPasaport = "";
            Nume = "";
            Culoare = "";
            Rasa = "";
            Categorie = "";
            Sex = "";
        }
        public Animal(Animal animal)
        {
            CodMicrocip = animal.CodMicrocip;
            DataNasterii = animal.DataNasterii;
            SerieCarnet = animal.SerieCarnet;
            NumarCarnet = animal.NumarCarnet;
            CodPasaport = animal.CodPasaport;
            Nume = animal.Nume;
            Culoare = animal.Culoare;
            Rasa = animal.Rasa;
            Categorie = animal.Categorie;
            Sex = animal.Sex;
        }

        public int ID { get; set; }

        public string CodMicrocip { get; set; }

        public DateTime DataNasterii { get; set; }

        public string SerieCarnet { get; set; }

        public string NumarCarnet { get; set; }

        public string CodPasaport { get; set; }

        public string Nume { get; set; }

        public string Culoare { get; set; }

        public string Rasa { get; set; }

        public string Categorie { get; set; }

        public string Sex { get; set; }

        public int ProprietarID { get; set; }
    }
}

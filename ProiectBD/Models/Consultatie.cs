using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectBD.Models
{
    public class Consultatie
    {
        public Consultatie(IDataReader reader, SqlConnection conn)
        {
            ID = reader.GetInt32(0);
            Descriere = reader.IsDBNull(1) ? null : reader.GetString(1);
            Data = reader.GetDateTime(2).Date;
            Observatii = reader.IsDBNull(3) ? null : reader.GetString(3);
            AnimalID = reader.GetInt32(4);
            MedicID = reader.GetInt32(5);
        }

        public Consultatie()
        {
            Descriere = "";
            Data = DateTime.Now;
            Observatii = "";
        }
        public Consultatie(Consultatie consultatie)
        {
            Descriere = consultatie.Descriere;
            Data = consultatie.Data;
            Observatii = consultatie.Observatii;
            AnimalID = consultatie.AnimalID;
            MedicID = consultatie.MedicID;
        }

        public int ID { get; set; }

        public string Descriere { get; set; }

        public DateTime Data { get; set; }

        public string Observatii { get; set; }

        public int AnimalID { get; set; }

        public int MedicID { get; set; }
    }
}

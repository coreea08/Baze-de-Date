using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProiectBD.Models.Auxiliar;

namespace ProiectBD.Controllers
{
    public class AUXController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public AUXController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/animalProprietar")]
        public List<AnimalProprietar> GetNumeAnimaleWithCNPProprietar()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT P.Nume + ' ' + P.Prenume as NumeProprietar, P.CNP, P.Telefon, A.Nume as NumeAnimal, A.CodMicrocip, A.Categorie, A.Rasa " +
                                            "FROM Animale A, Proprietari P " +
                                            "WHERE A.ProprietarID = P.ID", conn);
            //cmd.Parameters.AddWithValue("@cnp", cnp);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var consultatii = new List<AnimalProprietar>();

                while (reader.Read())
                {

                    var p = new AnimalProprietar
                    {
                        Nume = reader.GetString(0),
                        CNP = reader.GetString(1),
                        Telefon =reader.GetString(2),
                        NumeA = reader.GetString(3),
                        CodMicrocip = reader.GetString(4),
                        Categorie =reader.GetString(5),
                        Rasa = reader.GetString(6)
                    };
                    consultatii.Add(p);
                }

                return consultatii;
            }
        }

        [HttpGet("/api/medicNumarConsultatii")]
        public List<Medic_NrConsultatii> GetMediciWithNumarConsultatii()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Nume, Prenume, Telefon, (SELECT COUNT(*)  FROM Consultatii WHERE MedicID = M.ID) AS NumarConsultatii FROM Medici M ", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var medici = new List<Medic_NrConsultatii>();

                while (reader.Read())
                {

                    var p = new Medic_NrConsultatii
                    {
                        Nume = reader.GetString(0),
                        Prenume = reader.GetString(1),
                        Telefon = reader.GetString(2),
                        NumarConsultatii = reader.GetInt32(3)
                    };
                    medici.Add(p);
                }

                return medici;
            }
        }

        [HttpGet("/api/castigConsultatiiMedic")]
        public List<CastigTotalConsultatii> GetCastigConsultatiiMedic()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT M.Nume, M.Prenume, A.CastigTotal " +
                                            "FROM Medici M, (SELECT MedicID, Sum(C.Pret) AS CastigTotal " +
                                                            "FROM Consultatii C  " +
                                                            "GROUP BY MedicID  " +
                                                            "HAVING SUM(C.Pret) > 100) AS A " +
                                           "WHERE M.id = A.MedicID  ", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var medici = new List<CastigTotalConsultatii>();

                while (reader.Read())
                {

                    var p = new CastigTotalConsultatii
                    {
                        Nume = reader.GetString(0),
                        Prenume = reader.GetString(1),
                        CastigTotal = reader.GetInt32(2)
                    };
                    medici.Add(p);
                }

                return medici;
            }

        }

        [HttpGet("/api/NrReteteMedic")]
        public List<CastigTotalConsultatii> GetNrReteteMedic()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Nume, Prenume " +
                                            "FROM Medici WHERE ID IN (SELECT MedicID FROM Consultatii WHERE ID IN (SELECT ConsultatieID FROM Retete) GROUP BY MedicID HAVING COUNT(*) > 1  )", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var medici = new List<CastigTotalConsultatii>();

                while (reader.Read())
                {

                    var p = new CastigTotalConsultatii
                    {
                        Nume = reader.GetString(0),
                        Prenume = reader.GetString(1),
                    };
                    medici.Add(p);
                }

                return medici;
            }
        }


        [HttpGet("/api/medicAverageInterventii")]
        public List<StatisticiInterventii> GetMediciAverageInterventii()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT M.Nume, M.Prenume, SUM(I.Pret), COUNT(I.Pret), AVG(I.Pret) FROM Medici M, Consultatii C, Interventii I " +
                "WHERE M.ID = C.MedicID AND C.ID = I.ConsultatieID AND I.Data BETWEEN @data1 AND @data2 " +
                "GROUP BY M.Nume, M.Prenume ", conn);

            DateTime data1, data2;
            if (DateTime.Now.Month == 1)
            {
                data1 = new DateTime(DateTime.Now.Year-1,12, 1);
                data2 = new DateTime(DateTime.Now.Year-1, 12, DateTime.DaysInMonth(DateTime.Now.Year-1, 12));
            }
            else
            {
                data1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
                data2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1));
            }

            cmd.Parameters.AddWithValue("@data1", data1);
            cmd.Parameters.AddWithValue("@data2", data2);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var medici = new List<StatisticiInterventii>();

                while (reader.Read())
                {

                    var p = new StatisticiInterventii
                    {
                        Nume = reader.GetString(0),
                        Prenume = reader.GetString(1),
                        CastigTotal = reader.GetInt32(2),
                        NumarInterventii = reader.GetInt32(3),
                        MedieCastig = reader.GetInt32(4),
                    };
                    medici.Add(p);
                }

                return medici;
            }
        }

    }
}
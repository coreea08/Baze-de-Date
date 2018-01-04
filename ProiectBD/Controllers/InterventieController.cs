using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ProiectBD.Models;
using ProiectBD.Models.Auxiliar;

namespace ProiectBD.Controllers
{
    public class InterventieController : Controller
    {
        public static string connectionString = "Server=OVIDIU-PC\\SQLEXPRESS;Database=Proiect_BD;Trusted_Connection=True;MultipleActiveResultSets=true";
        public SqlConnection conn;

        public InterventieController()
        {
            this.conn = new SqlConnection(connectionString);
        }

        [HttpGet("/api/Interventie/{id}")]
        public Interventie GetInterventie(int id)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Interventii WHERE ID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                var interventie = new Interventie(reader, conn);

                return interventie;
            }

        }


        [HttpGet("/api/Interventie")]
        public List<Interventie> GetInterventii()
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Interventii", conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                List<Interventie> interventii = new List<Interventie>();

                while (reader.Read())
                {
                    Interventie p = new Interventie(reader, conn);
                    interventii.Add(p);
                }

                return interventii;
            }
        }

        [HttpGet("/api/InterventieMedicNoua/{medicId}")]
        public List<InterventieConsultatie> GetInterventiiNoiMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT I.ID, I.Nume, I.Descriere, I.Data, I.Pret , I.ConsultatieID, C.Descriere, C.Data " +
                "FROM Medici M, Consultatii C, Interventii I WHERE M.ID = C.MedicID AND C.ID = I.ConsultatieID AND M.ID = @medicId AND I.Data >= @data ORDER BY I.Data", conn);

            cmd.Parameters.AddWithValue("@medicId", medicId);
            cmd.Parameters.AddWithValue("@data", DateTime.Now);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var interventii = new List<InterventieConsultatie>();

                while (reader.Read())
                {

                    var p = new InterventieConsultatie
                    {
                        ID = reader.GetInt32(0),
                        Nume = reader.GetString(1),
                        Descriere = reader.GetString(2),
                        Data = reader.GetDateTime(3).Date,
                        Pret = reader.GetInt32(4),
                        ConsultatieID = reader.GetInt32(5),
                        DescriereConsultatie = reader.GetString(6),
                        DataConsultatie = reader.GetDateTime(7).Date
                    };
                    interventii.Add(p);
                }

                return interventii;
            }

        }

        [HttpGet("/api/InterventieMedicVeche/{medicId}")]
        public List<InterventieConsultatie> GetInterventiiVechiMedic(int medicId)
        {
            this.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT *, (SELECT C2.Descriere FROM Consultatii C2 WHERE I.ConsultatieID = C2.ID ), (SELECT C1.Data FROM Consultatii C1 WHERE I.ConsultatieID = C1.ID )" +
                           "FROM Interventii I WHERE I.ConsultatieID IN (SELECT C.ID FROM Consultatii C WHERE C.MedicID = @medicId) AND I.Data < @data ORDER BY I.Data DESC", conn);

            cmd.Parameters.AddWithValue("@medicId", medicId);
            cmd.Parameters.AddWithValue("@data", DateTime.Now);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                var interventii = new List<InterventieConsultatie>();

                while (reader.Read())
                {

                    var p = new InterventieConsultatie
                    {
                        ID = reader.GetInt32(0),
                        Nume = reader.GetString(1),
                        Descriere = reader.GetString(2),
                        Data = reader.GetDateTime(3).Date,
                        Pret = reader.IsDBNull(4)? 0 :reader.GetInt32(4),
                        ConsultatieID = reader.GetInt32(5),
                        DescriereConsultatie = reader.GetString(6),
                        DataConsultatie = reader.GetDateTime(7).Date
                    };
                    interventii.Add(p);
                }

                return interventii;
            }

        }


        [HttpPost("/api/Interventie")]
        public IActionResult CreateInterventie([FromBody] Interventie interventie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SqlCommand cmd = new SqlCommand("INSERT INTO Interventii VALUES (@nume, @descriere, @data, @pret, @consultatieID)", conn);

            cmd.Parameters.AddWithValue("@nume", interventie.Nume);
            cmd.Parameters.AddWithValue("@descriere", interventie.Descriere);
            cmd.Parameters.AddWithValue("@data", interventie.Data);
            cmd.Parameters.AddWithValue("@pret", interventie.Pret);
            cmd.Parameters.AddWithValue("@consultatieID", interventie.ConsultatieID);


            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }

        [HttpPut("/api/Interventie/{id}")]
        public IActionResult UpdateInterventie(int id, [FromBody] Interventie interventie)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Interventii SET Descriere = @descriere, Data = @data, Nume=@nume, Pret = @pret, ConsultatieID = @consultatieID  WHERE ID = @id ", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nume", interventie.Nume);
            cmd.Parameters.AddWithValue("@descriere", interventie.Descriere);
            cmd.Parameters.AddWithValue("@data", interventie.Data);
            cmd.Parameters.AddWithValue("@pret", interventie.Pret);
            cmd.Parameters.AddWithValue("@consultatieID", interventie.ConsultatieID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok();
        }

        [HttpDelete("/api/Interventie/{id}")]
        public IActionResult DeleteInterventie(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Interventii WHERE ID=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            this.conn.Open();
            cmd.ExecuteNonQuery();
            this.conn.Close();

            return Ok();
        }
    }
}